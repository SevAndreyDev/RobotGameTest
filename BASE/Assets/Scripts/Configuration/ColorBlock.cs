using System;
using UnityEngine;

namespace EnglishKids.RobotGame
{
    [CreateAssetMenu(fileName = "Color Block", menuName = "Robot Game/Color Block", order = 50)]
    public class ColorBlock : ScriptableObject
    {
        [Serializable]
        public class RobotPartConfig
        {
            public BodyParts robotPart;
            public Sprite image;
            public float maxDragOffset;

            public ColorBlock ColorBlockSettings { get; set; }
        }
        
        //==================================================
        // Fields
        //==================================================

        [Space]
        [SerializeField] private ColorKinds _kind;

        [Header("Base View Settings")]
        [SerializeField] private Sprite _leftBackground;
        [SerializeField] private Sprite _rightBackground;
        [SerializeField] private Audio _speachKind;
        [SerializeField] private string _colorText;

        [Header("Robot Body")]
        [SerializeField] private Robot _robotPrefab;
        [SerializeField] private float _scaleFactor;        
        [SerializeField] private RobotPartConfig[] _robotConfigurations;

        //==================================================
        // Properties
        //==================================================

        public ColorKinds Kind { get { return _kind; } }
        public Sprite LeftBackground { get { return _leftBackground; } }
        public Sprite RightBackground { get { return _rightBackground; } }
        public Audio SpeachKind { get { return _speachKind; } }
        public string ColorText { get { return _colorText; } }
        public Robot RobotPrefab { get { return _robotPrefab; } }
        public float ScaleFactor { get { return _scaleFactor; } }
        public RobotPartConfig[] RobotConfigurations { get { return _robotConfigurations; } }
    }
}
