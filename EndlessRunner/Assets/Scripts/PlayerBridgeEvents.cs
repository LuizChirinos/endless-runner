using Damages;
using System.Collections;
using System.Collections.Generic;
using Triplano.Inputs;
using Triplano.Lanes;
using UnityEngine;

namespace Triplano
{
    public class PlayerBridgeEvents : MonoBehaviour
    {
        private Health health;
        private RailMovement railMovement;
        private LaneMovement laneMovement;
        private InputMovement inputMovement;

        private void Start()
        {
            health = GetComponent<Health>();
            railMovement = GetComponentInParent<RailMovement>();
            laneMovement = GetComponent<LaneMovement>();
            inputMovement = GetComponent<InputMovement>();

            health.OnDeath += railMovement.LockMovement;
            health.OnDeath += laneMovement.StopMove;
            health.OnDeath += inputMovement.Disable;
        }
        private void OnDestroy()
        {
            health.OnDeath -= railMovement.LockMovement;
            health.OnDeath -= laneMovement.StopMove;
            health.OnDeath -= inputMovement.Disable;
        }
    }
}
