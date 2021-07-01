using TMPro;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class SpeachButton : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] TMP_Text _label;

        //==================================================
        // Properties
        //==================================================

        public TMP_Text Label { get { return _label; } }
        public Audio SpeachSound { get; set; }

        //==================================================
        // Methods
        //==================================================

        public void PlaySpeach()
        {
            AudioManager.Instance.PlaySpeach(SpeachSound);
        }
    }
}