using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triplano
{
    public abstract class SpawnModuleFilter : ScriptableObject
    {
        public abstract Module GetModule();
    }
}
