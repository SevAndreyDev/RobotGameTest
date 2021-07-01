using UnityEngine.UI;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class FactoryItem : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private RectTransform _panel;
        [SerializeField] private DragElement _dragElement;
        [SerializeField] private RectTransform _iconTransform;        
        [SerializeField] private Image _icon;
        [SerializeField] private float _offsetWidth;
        [SerializeField] private float _offsetRotation;

        //==================================================
        // Properties
        //==================================================
                
        public RectTransform Panel { get { return _panel; } }
        public RectTransform IconTransform { get { return _iconTransform; } }
        public Image Icon { get { return _icon; } }

        public float Height { get; private set; }

        //==================================================
        // Methods
        //==================================================

        public void ActivateImage(ColorBlock.RobotPartConfig configItem)
        {
            Sprite sprite = configItem.image;
            _icon.sprite = sprite;
            _icon.SetNativeSize();

            float scaleFactor = configItem.ColorBlockSettings.ScaleFactor;
                        
            _icon.rectTransform.localScale = Vector3.one * scaleFactor;
            const float HALF_PART = 0.5f;
            float possibleHorizontalShift = _offsetWidth - sprite.rect.width * scaleFactor * HALF_PART;

            _icon.rectTransform.localPosition = Vector3.right * Random.Range(-possibleHorizontalShift, possibleHorizontalShift);
            _icon.rectTransform.Rotate(Vector3.forward * Random.Range(-_offsetRotation, _offsetRotation));

            this.Height = sprite.rect.height * scaleFactor;

            _panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.Height);
            _panel.gameObject.SetActive(true);

            _dragElement.Activate(configItem);
        }
    }
}