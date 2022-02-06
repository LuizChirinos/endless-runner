using UnityEngine;

namespace Triplano
{
    public class Module : MonoBehaviour
    {
        [SerializeField] private Transform endPivot;
        [SerializeField] private ModuleSpawnTrigger moduleSpawnTrigger;
        [SerializeField] private ModuleSpawnTrigger moduleDestroyTrigger;
        public Vector3 EndPosition { get => endPivot.position; }

        public void SetSpawnTrigger(bool value) => moduleSpawnTrigger.gameObject.SetActive(value);

        public void SetDestroyTrigger(bool value) => moduleDestroyTrigger.gameObject.SetActive(value);

    }
}
