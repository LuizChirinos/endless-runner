using System;
using UnityEngine;

namespace Triplano.Lanes
{
    [CreateAssetMenu(fileName = nameof(LaneMovementData), menuName = "Triplano/Lanes")]
    public class LaneMovementData : ScriptableObject
    {
        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private float durationOfMovement;

        [SerializeField] private float spacing;
        [SerializeField] private int numberOfLanes;
        [SerializeField] private float durationOfSlide;

        public float Spacing { get => spacing; }
        public int NumberOfLanes { get => numberOfLanes; }
        public AnimationCurve MovementCurve { get => movementCurve; }
        public float DurationOfMovement { get => durationOfMovement; }
        public float DurationOfSlide { get => durationOfSlide; }

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
