using System;
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

        public int ReturnLaneBoundary(int indexOfCurrentLane)
        {
            if (indexOfCurrentLane >= numberOfLanes - 1)
                return 1;
            else if (indexOfCurrentLane <= 0)
                return -1;

            return 0;
        }
    }
}
