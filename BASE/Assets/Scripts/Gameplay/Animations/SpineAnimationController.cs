using System;
using UnityEngine;
using Spine.Unity;
using Spine;

namespace EnglishKids.RobotGame
{
    public class SpineAnimationController : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private SkeletonGraphic _animation;

        protected string _currentAnimation;
        private Action<string> OnAnimationCompleteHandler;

        //==================================================
        // Properties
        //==================================================

        public bool IsPlaying { get { return !string.IsNullOrEmpty(_currentAnimation) && _animation.AnimationState.GetCurrent(0).IsComplete; } }

        //==================================================
        // Methods
        //==================================================

        protected void PlayAnimation(AnimationReferenceAsset targetAnimation, bool loop, float animationSpeed)
        {
            if (!targetAnimation.name.Equals(_currentAnimation))
            {                
                _animation.AnimationState.ClearTrack(0);
                _animation.AnimationState.SetAnimation(0, targetAnimation, loop).TimeScale = animationSpeed;

                if (!loop)
                    _animation.AnimationState.Complete += OnAnimationComplete;
                            
                _currentAnimation = targetAnimation.name;
            }            
        }
                
        protected virtual void OnAnimationComplete(TrackEntry trackEntry)
        {
            _animation.AnimationState.Complete -= OnAnimationComplete;
            _animation.AnimationState.ClearTrack(0);
            _currentAnimation = string.Empty;
        }        
    }
}