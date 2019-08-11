using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent Agent;
        private Animator Animator;

        void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void Stop()
        {
            Agent.isStopped = true;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionSchaduler>().StartAction(this);
            GetComponent<Fighter>().CancelAttack();
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
    }
}


