using UnityEngine;

namespace Triplano
{
    public class Module : MonoBehaviour
    {
        [SerializeField] private Transform endPivot;

        public Vector3 EndPosition { get => endPivot.position; }
    }
}
