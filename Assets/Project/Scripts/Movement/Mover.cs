using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private float MaxSpeed = 3.0f;

        private NavMeshAgent Agent;
        private Animator Animator;
        private Health HealthComponent;

        void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            HealthComponent = GetComponent<Health>();
        }

        void Update()
        {
            Agent.enabled = !HealthComponent.IsDead;
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionSchaduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            Agent.isStopped = false;
            Agent.destination = destination;
            Agent.speed = MaxSpeed * Mathf.Clamp01(speedFraction);
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = Agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            speed = Mathf.InverseLerp(0, Agent.speed, speed);
            Animator.SetFloat("ForwardSpeed", speed);
        }

        public void Cancel()
        {
            Agent.isStopped = true;
        }
    }
}


