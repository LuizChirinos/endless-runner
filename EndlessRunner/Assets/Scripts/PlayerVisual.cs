using Damages;
using System;
using Triplano.Lanes;
using Triplano.Movement;
using UnityEngine;

namespace Triplano
{
    [RequireComponent(typeof(LaneMovement), typeof(Health))]
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Transform visual;
        private JumpMovement jumpMovement;
        private RailMovement railMovement;
        private SlideMovement slideMovement;
        private Health health;
        private Animator anim;

        private int runningHash = Animator.StringToHash("isRunning");
        private int dieHash = Animator.StringToHash("Die");
        private int jumpingHash = Animator.StringToHash("isJumping");
        private int slideHash = Animator.StringToHash("isSliding");

        private void Start()
        {
            anim = visual.GetComponentInChildren<Animator>();
            railMovement = GetComponentInParent<RailMovement>();
            health = GetComponent<Health>();
            jumpMovement = GetComponent<JumpMovement>();
            slideMovement = GetComponent<SlideMovement>();

            //slideMovement.OnStartSliding += StartSlide;
            //slideMovement.OnStopSliding += StopSlide;
            //jumpMovement.OnStartJumping += StartJumping;
            //jumpMovement.OnStopJumping += StopJumping;
            railMovement.OnStartMoving += StartMoving;
            railMovement.OnStopMoving += StopMoving;
            health.OnDeath += Die;
        }

        private void StopSlide()
        {
            anim.SetBool(slideHash, false);

            StartMoving();
            StopJumping();
        }

        private void StartSlide()
        {
            anim.SetBool(slideHash, true);

            StopJumping();
            StopMoving();
        }

        private void OnDestroy()
        {
            jumpMovement.OnStartJumping -= StartJumping;
            jumpMovement.OnStopJumping -= StopJumping;
            railMovement.OnStartMoving -= StartMoving;
            railMovement.OnStopMoving -= StopMoving;
            health.OnDeath -= Die;
        }

        private void Die()
        {
            anim.SetBool(dieHash, true);
        }

        private void StopMoving()
        {
            anim.SetBool(runningHash, false);
        }

        private void StartMoving()
        {
            anim.SetBool(runningHash, true);
        }

        private void StartJumping()
        {
            anim.SetBool(jumpingHash, true);
        }
        private void StopJumping()
        {
            anim.SetBool(jumpingHash, false);
        }
    }
}
