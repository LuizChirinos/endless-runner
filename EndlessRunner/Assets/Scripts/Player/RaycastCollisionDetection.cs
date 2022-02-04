using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triplano
{
    public class RaycastCollisionDetection : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField] private float minDistanceOfDetection = 0.2f;
        [SerializeField] private float maxDistanceOfDetection = 0.2f;
        [SerializeField] private bool raycastTriggers = false;

        private RaycastHit hitInfo;
        private float distanceToCollider;

        [SerializeField] private Color debugColor = Color.red;

        public bool IsColliding 
        {
            get
            {
                Ray ray = new Ray(transform.position, transform.forward);
                bool rayCollision = Physics.Raycast(ray, out hitInfo, maxDistanceOfDetection, collisionLayer);

                if (hitInfo.collider)
                    distanceToCollider = hitInfo.distance;

                return rayCollision;
            }
        }

        public float DistanceToCollider { get => distanceToCollider; }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * minDistanceOfDetection, debugColor);
        }
    }
}
