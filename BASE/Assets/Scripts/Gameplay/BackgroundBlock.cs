using UnityEngine.UI;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class BackgroundBlock : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private Orientations _orientation;
        [SerializeField] private Image _background;
        [SerializeField] private RectTransform _center;
        [SerializeField] private SpeachButton _speachButton;

        //==================================================
        // Properties
        //==================================================

        public Orientations Orientation { get { return _orientation; } }
        public Image Background { get { return _background; } }
        public RectTransform Center { get { return _center; } }
        public SpeachButton CachedSpeachButton { get { return _speachButton; } }

        private ColorKinds _blockColor;

        //==================================================
        // Methods
        //==================================================

        public void Initialize(ColorKinds color)
        {
            _blockColor = color;
            DragElement.OnElementWasUsed += OnDragElementWasUsed;
        }

        #region Events
        private void OnDragElementWasUsed(ColorKinds color)
        {
            if (_blockColor == color)
            {
                _speachButton.gameObject.SetActive(true);
                _speachButton.PlaySpeach();

                DragElement.OnElementWasUsed -= OnDragElementWasUsed;
            }
        }
        #endregion
    }
}