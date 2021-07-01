using System;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class FactoryPartsBlock : MonoBehaviour
    {
        public static event Action OnBlockIsEmpty;

        //==================================================
        // Fields
        //==================================================

        [SerializeField] RectTransform _cachedTransform;

        private bool _isActive;

        //==================================================
        // Properties
        //==================================================

        public RectTransform CachedTransform { get { return _cachedTransform; } }
        public int PartsCount { get; set; }

        //==================================================
        // Methods
        //==================================================

        public static void ClearEvents()
        {
            OnBlockIsEmpty = null;
        }

        public void Activate()
        {
            DragElement.OnElementWasUsed += OnElementWasUsed;
            _isActive = true;
        }

        public void Deactivate()
        {
            DragElement.OnElementWasUsed -= OnElementWasUsed;
            _isActive = false;
        }

        #region Events
        private void OnElementWasUsed(ColorKinds color)
        {
            if (_isActive)
            {                
                this.PartsCount--;

                if (this.PartsCount == 0)
                {
                    Deactivate();
                    OnBlockIsEmpty?.Invoke();
                }
            }
        }
        #endregion
    }
}