using UnityEngine.EventSystems;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class RestartRobotButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] RobotIntaractiveElement _target;
                
        //==================================================
        // Methods
        //==================================================	

        public void OnPointerDown(PointerEventData eventData) { }

        public void OnPointerUp(PointerEventData eventData)
        {
            _target.Play();
        }
    }
}