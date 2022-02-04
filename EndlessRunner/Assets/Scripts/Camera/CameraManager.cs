using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triplano
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraOriginData cameraOriginData;

        private void Start()
        {
            cameraOriginData.OnUpdatedOrigin += DispatchPosition;
        }

        private Vector3 DispatchPosition() => transform.position;
    }
}
