using Damages;
using System.Collections;
using System.Collections.Generic;
using Triplano.Inputs;
using Triplano.Lanes;
using Triplano.Movement;
using UnityEngine;

namespace Triplano
{
    public class PlayerBridgeEvents : MonoBehaviour
    {
        private Health health;
        private RailMovement railMovement;
        private VerticalMovement verticalMovement;
        private SlideMovement slideMovement;
        private LaneMovement laneMovement;
        private InputMovement inputMovement;

        private void Start()
        {
            health = GetComponent<Health>();
            railMovement = GetComponentInParent<RailMovement>();
            slideMovement = GetComponent<SlideMovement>();
            verticalMovement = GetComponent<VerticalMovement>();
            laneMovement = GetComponent<LaneMovement>();
            inputMovement = GetComponent<InputMovement>();

            verticalMovement.OnStartJumping += slideMovement.LockMovement;
            verticalMovement.OnStopJumping += slideMovement.UnlockMovement;
            health.OnDeath += railMovement.LockMovement;
            health.OnDeath += laneMovement.StopMove;
            health.OnDeath += inputMovement.Disable;
        }
        private void OnDestroy()
        {
            verticalMovement.OnStartJumping -= slideMovement.LockMovement;
            verticalMovement.OnStopJumping -= slideMovement.UnlockMovement;
            health.OnDeath -= railMovement.LockMovement;
            health.OnDeath -= laneMovement.StopMove;
            health.OnDeath -= inputMovement.Disable;
        }
    }
}
