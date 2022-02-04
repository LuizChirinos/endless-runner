using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Triplano
{
    public class ModulesCreator : MonoBehaviour
    {
        [SerializeField] private List<Module> modules;
        [SerializeField] private GameEvent moduleGameEvent;
        [SerializeField] private Module modulePrefab;
        [SerializeField] private int maxNumberOfModules = 2;

        public int NumberOfActiveModules { get => modules.Count; }
        public Vector3 LastModulesPosition { get => transform.TransformPoint(LastModule.EndPosition); }
        public  Module FirstModule { get => modules[0]; }
        public  Module LastModule { get => modules[NumberOfActiveModules-1]; }

        private void Start()
        {
            modules = GetComponentsInChildren<Module>().ToList();
            moduleGameEvent.SubscribeToEvent(CreateModule);
        }
        private void OnDestroy()
        {
            moduleGameEvent.UnsubscribeToEvent(CreateModule);
        }

        public void CreateModule()
        {
            Module spawnedModule = Instantiate(modulePrefab, LastModulesPosition, Quaternion.identity, transform);
            AddModule(spawnedModule);

            if (NumberOfActiveModules > maxNumberOfModules)
            {
                Module moduleinstance = FirstModule;

                RemoveModule(moduleinstance);
                Destroy(moduleinstance.gameObject);
            }
        }

        private Module AddModule(Module module)
        {
            modules.Add(module);
            return module;
        }
        private Module RemoveModule(Module module)
        {
            modules.Remove(module);
            return module;
        }
    }
}
