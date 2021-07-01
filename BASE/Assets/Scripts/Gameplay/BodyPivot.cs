using UnityEngine;

namespace EnglishKids.RobotGame
{
    public class BodyPivot : MonoBehaviour
    {
        //==================================================
        // Fields
        //==================================================

        [SerializeField] private BodyParts _kind;
        [SerializeField] private RectTransform _cachedTransform;

        //==================================================
        // Properties
        //==================================================

        public BodyParts Kind { get { return _kind; } }
        public RectTransform CachedTransform { get { return _cachedTransform; } }
    }
}