using UnityEngine;
using Spine.Unity;

namespace EnglishKids.RobotGame
{
    public class StarController : SpineAnimationController
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private RectTransform _cachedTransform;
        [SerializeField] private GameObject _animationObject;

        [SerializeField] private AnimationReferenceAsset _effect;
        [SerializeField] private float _animationSpeed;

        //==================================================
        // Properties
        //==================================================

        public RectTransform CachedTransform { get { return _cachedTransform; } }

        //==================================================
        // Methods
        //==================================================

        public void Play()
        {
            _animationObject.SetActive(true);
            PlayAnimation(_effect, false, _animationSpeed);
        }
    }
}