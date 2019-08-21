﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float ChaseDistance = 5f;
        [SerializeField] private float SuspicionTime = 3f;

        private GameObject Player;
        private Fighter FighterComponent;
        private Health HealthComponent;
        private Mover Movement;
        private Vector3 GuardPosition;
        private float TimeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            Player = GameObject.FindWithTag("Player");
            FighterComponent = GetComponent<Fighter>();
            HealthComponent = GetComponent<Health>();
            Movement = GetComponent<Mover>();

            GuardPosition = transform.position;
        }

        private void Update()
        {
            if (HealthComponent.IsDead) return;

            if (InAttackRangeToPlayer() && FighterComponent.CanAttack(Player))
            {
                TimeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (TimeSinceLastSawPlayer < SuspicionTime)
            {
                SuspicionBehaviour();
                //Debug
                if (name == "Enemy (1)")
                {
                    print(TimeSinceLastSawPlayer);
                }
            }
            else
            {
                GuardBehaviour();
            }

            TimeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            FighterComponent.Attack(Player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionSchaduler>().CancelCurrentAction();
        }

        private void GuardBehaviour()
        {
            Movement.StartMoveAction(GuardPosition);
        }



        private bool InAttackRangeToPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            return distanceToPlayer  < ChaseDistance;
        }

        // Called by Unity in editor (not in game)
        private void OnDrawGizmos()
        {

        }

        // Called by Unity in editor (not in game)
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, ChaseDistance);
        }
    }
}