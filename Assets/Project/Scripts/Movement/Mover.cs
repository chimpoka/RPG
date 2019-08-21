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

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionSchaduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            Agent.isStopped = false;
            Agent.destination = destination;
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


