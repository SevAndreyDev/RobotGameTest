using UnityEngine;
using Spine.Unity;
using Spine;

namespace EnglishKids.RobotGame
{
    public class RobotIntaractiveElement : SpineAnimationController
    {
        //==================================================
        // Fields
        //==================================================
                
        [SerializeField] private GameObject _animationObject;

        [SerializeField] private AnimationReferenceAsset _idle;
        [SerializeField] private AnimationReferenceAsset _action;
        [SerializeField] private float _animationSpeed;
                
        //==================================================
        // Methods
        //==================================================

        public void Play()
        {
            if (!_animationObject.activeSelf)
                _animationObject.SetActive(true);
                        
            PlayAnimation(_action, false, _animationSpeed);
        }

        #region Events
        protected override void OnAnimationComplete(TrackEntry trackEntry)
        {
            base.OnAnimationComplete(trackEntry);                    
            PlayAnimation(_idle, true, _animationSpeed);
        }
        #endregion
    }
}