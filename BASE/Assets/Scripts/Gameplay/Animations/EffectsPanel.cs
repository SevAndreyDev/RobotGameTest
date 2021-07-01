using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class EffectsPanel : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private StarController _starPrefab;

        private List<StarController> _starList;

        //==================================================
        // Methods
        //==================================================	

        private void Awake()
        {
            _starList = new List<StarController>();
        }

        public StarController GetFreeStarEffect()
        {
            foreach (StarController item in _starList)
            {
                if (!item.IsPlaying)
                    return item;
            }

            StarController star = Instantiate(_starPrefab, _rectTransform);
            _starList.Add(star);

            return star;
        }
    }
}