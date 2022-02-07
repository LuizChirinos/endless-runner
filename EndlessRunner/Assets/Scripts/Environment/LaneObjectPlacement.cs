using System.Collections;
using System.Collections.Generic;
using Triplano.Lanes;
using UnityEditor;
using UnityEngine;

namespace Triplano
{
    public class LaneObjectPlacement : MonoBehaviour
    {
        [SerializeField] private LaneMovementData laneMovementData;
        [SerializeField] private int laneIndex;
        [SerializeField] private float incrementAmount = 1f;

        private void OnValidate()
        {
            transform.localPosition = new Vector3(laneIndex * laneMovementData.Spacing,
                                                  transform.localPosition.y,
                                                  transform.localPosition.z);

            incrementAmount = Mathf.Clamp(incrementAmount, 0f, incrementAmount);
        }

        public void IncrementOnZ()
        {
            transform.localPosition += transform.forward * incrementAmount;
        }
        public void DecrementOnZ()
        {
            transform.localPosition -= transform.forward * incrementAmount;
        }
        public void IncrementOnY()
        {
            transform.localPosition += transform.up * incrementAmount;
        }
        public void DecrementOnY()
        {
            transform.localPosition -= transform.up * incrementAmount;
        }
    }
}
