using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triplano
{
    public class ModuleSpawnTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvent moduleSpawnEvent;
        [SerializeField] private TagData tagData;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out TagContainer tagContainer))
                return;
            if (tagContainer.TagData != tagData)
                return;

            moduleSpawnEvent.TriggerEvent();
        }
    }
}
