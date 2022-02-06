using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triplano
{
    [CreateAssetMenu(fileName = nameof(CameraOriginData), menuName = "Cameras/CameraData")]
    public class CameraOriginData : ScriptableObject
    {
        public delegate Vector3 CameraOriginEventHandler();
        public CameraOriginEventHandler OnUpdatedOrigin;
    }
}
