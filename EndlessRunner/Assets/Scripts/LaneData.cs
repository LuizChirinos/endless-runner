using UnityEngine;

namespace Triplano.Lanes
{
    [CreateAssetMenu(fileName = nameof(LaneData), menuName = "Triplano/Lanes")]
    public class LaneData : ScriptableObject
    {
        [SerializeField] private float spacing;
        [SerializeField] private int numberOfLanes;

        public float Spacing { get => spacing; }
        public int NumberOfLanes { get => numberOfLanes; }
    }
}
