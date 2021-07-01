using UnityEngine.UI;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class Robot : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private ColorKinds _kind;
        [SerializeField] RectTransform _cachedTransform;
        [SerializeField] private Image _shadow;
        [SerializeField] private RobotIntaractiveElement _animatedRobot;
        [SerializeField] BodyPivot[] _parts;

        private int _leftParts;

        //==================================================
        // Properties
        //==================================================

        public ColorKinds ColorKind { get { return _kind; } }
        public RectTransform CachedTransform { get { return _cachedTransform; } }

        //==================================================
        // Methods
        //==================================================

        public void Initialize()
        {
            DragElement.OnElementWasUsed += OnElementWasUsed;
            _leftParts = _parts.Length;
        }

        public BodyPivot FindBodyPart(BodyParts bodyKind)
        {
            foreach (BodyPivot item in _parts)
            {
                if (item.Kind == bodyKind)
                    return item;
            }

            return null;
        }

        #region Events
        private void OnElementWasUsed(ColorKinds color)
        {
            if (color == _kind)
            {
                _leftParts--;

                if (_leftParts <= 0)
                {
                    DragElement.OnElementWasUsed -= OnElementWasUsed;

                    foreach (BodyPivot item in _parts)
                    {
                        item.gameObject.SetActive(false);
                    }

                    _shadow.enabled = false;

                    _animatedRobot.Play();
                }
            }
        }
        #endregion
    }
}