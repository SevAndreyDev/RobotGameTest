using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class LevelBuilder : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [Space]
        [SerializeField] private BackgroundBlock[] _backgrounds;

        //==================================================
        // Methods
        //==================================================
        
        public void BuildLevel(ColorBlock block, Orientations orientation)
        {
            foreach (BackgroundBlock item in _backgrounds)
            {
                if (item.Orientation == orientation)
                {
                    item.Background.sprite = orientation == Orientations.Left ? block.LeftBackground : block.RightBackground;

                    Robot robot = Instantiate(block.RobotPrefab);

                    robot.CachedTransform.SetParent(item.Center);
                    robot.CachedTransform.localPosition = Vector3.zero;
                    robot.CachedTransform.localScale = Vector3.one * block.ScaleFactor;

                    if (orientation == Orientations.Left)
                        GameManager.Instance.LeftRobot = robot;
                    else
                        GameManager.Instance.RightRobot = robot;

                    item.CachedSpeachButton.Label.text = block.ColorText;
                    item.CachedSpeachButton.SpeachSound = block.SpeachKind;

                    item.Initialize(block.Kind);
                }
            }

        }
    }
}