using UnityEngine;

namespace Triplano
{
    [CreateAssetMenu(fileName = nameof(RandomModule), menuName = "Levels/SpawnModuleFilter")]
    public class RandomModuleFilter : SpawnModuleFilter
    {
        [SerializeField] private Module[] modules;

        private Module RandomModule { get => modules[RandomIndex]; }
        private int RandomIndex { get => (int)Random.RandomRange(0f, modules.Length); }

        public override Module GetModule()
        {
            return RandomModule;
        }
    }
}
