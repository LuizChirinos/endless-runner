using UnityEngine;

namespace Triplano.Lanes
{
    public class RailMovement : MonoBehaviour
    {
        [SerializeField] private MovementData movementData;
        private void FixedUpdate()
        {
            transform.localPosition += Vector3.forward * Time.deltaTime * movementData.Speed;
        }
    }

}