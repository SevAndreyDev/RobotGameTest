using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace EnglishKids.RobotGame
{
    public class DragElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        public static event Action<ColorKinds> OnElementWasUsed;

        //==================================================
        // Fields
        //==================================================

        [Space]
        [SerializeField] private Image _elementImage;
        [SerializeField] private RectTransform _cachedTransform;
        [SerializeField] private RectTransform _factoryPanel;
        [SerializeField] private float _returnMovementDuration = 2f;
        [SerializeField] private float _rotationDuration = 1.5f;
        [SerializeField] private float _putInSlotDuration = 0.3f;
              
        private GameManager _manager;
        private AudioManager _audio;
        private ColorKinds _colorKind;
        private BodyPivot _target;
        private float _possibleTargetOffset;
        private Vector3 _startPosition;
        private Vector3 _startRotation;
        private Audio _speachKind;
        
        //==================================================
        // Methods
        //==================================================

        public static void ClearEvents()
        {
            OnElementWasUsed = null;
        }

        public void Activate(ColorBlock.RobotPartConfig configItem)
        {
            _manager = GameManager.Instance;
            _audio = AudioManager.Instance;

            _elementImage.raycastTarget = true;

            _colorKind = configItem.ColorBlockSettings.Kind;
            _startPosition = _cachedTransform.localPosition;
            _startRotation = _cachedTransform.rotation.eulerAngles;

            Robot targetRobot = _manager.LeftRobot.ColorKind == configItem.ColorBlockSettings.Kind ? _manager.LeftRobot : _manager.RightRobot;
            _target = targetRobot.FindBodyPart(configItem.robotPart);
            _possibleTargetOffset = configItem.maxDragOffset;

            _speachKind = configItem.ColorBlockSettings.SpeachKind;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _audio.PlaySound(Audio.Pick);

            var sequance = DOTween.Sequence();
            sequance.Append(_cachedTransform.DORotate(Vector3.zero, _rotationDuration));
        }

        public void OnPointerUp(PointerEventData eventData)
        {                        
            var sequance = DOTween.Sequence();
            
            if (Vector3.Distance(_cachedTransform.position, _target.CachedTransform.position) <= _possibleTargetOffset)
            {
                _elementImage.raycastTarget = false;
                OnElementWasUsed?.Invoke(_colorKind);

                StarController star = _manager.Effests.GetFreeStarEffect();
                star.CachedTransform.position = _cachedTransform.position;
                star.Play();

                _audio.PlaySound(Audio.CorrectSlot);
                _audio.PlaySpeach(_speachKind);

                _cachedTransform.SetParent(_target.CachedTransform);

                sequance.Append(_cachedTransform.DOMove(_target.CachedTransform.position, _putInSlotDuration));                
            }
            else
            {
                _cachedTransform.SetParent(_factoryPanel);

                sequance.Append(_cachedTransform.DORotate(_startRotation, _rotationDuration));

                if (_cachedTransform.localPosition != _startPosition)
                {
                    _audio.PlaySound(Audio.WrongSlot);
                    sequance.Insert(0f, _cachedTransform.DOLocalMove(_startPosition, _returnMovementDuration));

                    _elementImage.raycastTarget = false;

                    sequance.OnComplete(() =>
                    {
                        _elementImage.raycastTarget = true;
                    });
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _cachedTransform.SetParent(_manager.DragField);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _cachedTransform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f);
        }        
    }
}