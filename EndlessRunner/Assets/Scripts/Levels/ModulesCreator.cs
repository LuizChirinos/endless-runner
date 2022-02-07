using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Triplano.Lanes;
using System;

namespace Triplano
{
    public class ModulesCreator : MonoBehaviour
    {
        [SerializeField] private List<Module> modules;
        [SerializeField] private Transform modulesParent;
        [SerializeField] private GameEvent spawnModuleGameEvent;
        [SerializeField] private GameEvent destroyModuleGameEvent;
        [SerializeField] private CameraOriginData cameraOriginData;
        [SerializeField] private int maxNumberOfModules = 2;

        private RailMovement railMovement;

        public int NumberOfActiveModules { get => modules.Count; }
        public Vector3 LastModulesPosition { get => transform.TransformPoint(LastModule.EndPosition); }
        public Module FirstModule { get => modules[0]; }
        public Module LastModule { get => modules[NumberOfActiveModules - 1]; }

        private void Start()
        {
            railMovement = GetComponentInChildren<RailMovement>();
            modules = GetComponentsInChildren<Module>().ToList();

            foreach (Module item in modules)
            {
                item.SetDestroyTrigger(false);
            }

            spawnModuleGameEvent.SubscribeToEvent(CreateModule);
            destroyModuleGameEvent.SubscribeToEvent(DestroyModule);
        }

        private void OnDestroy()
        {
            spawnModuleGameEvent.UnsubscribeToEvent(CreateModule);
            destroyModuleGameEvent.UnsubscribeToEvent(DestroyModule);
        }

        private void CreateModule()
        {
            Module modulePrefab = LastModule.SpawnModuleFilter.GetModule();
            Module spawnedModule = Instantiate(modulePrefab, LastModulesPosition, Quaternion.identity, modulesParent);
            AddModule(spawnedModule);
        }

        private void DestroyModule()
        {
            Module removedModule = RemoveModule(FirstModule);
            Destroy(removedModule.gameObject);
        }

        private Module AddModule(Module module)
        {
            //spawnedModules++;
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
