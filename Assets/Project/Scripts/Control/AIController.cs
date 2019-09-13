using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float ChaseDistance = 5f;
        [SerializeField] private float SuspicionTime = 3f;
        [SerializeField] private PatrolPath Path;
        [SerializeField] private float WaypointTolerance = 1f;
        [SerializeField] private float WaypointDwellTime = 1f;
        [Range(0,1)]
        [SerializeField] private float PatrolSpeedFraction = 0.2f;

        private GameObject Player;
        private Fighter FighterComponent;
        private Health HealthComponent;
        private Mover Movement;
        private Vector3 GuardPosition;
        private float TimeSinceLastSawPlayer = Mathf.Infinity;
        private float TimeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int CurrentWaypointIndex = 0;

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
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            TimeSinceArrivedAtWaypoint += Time.deltaTime;
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

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = GuardPosition;

            if (Path != null)
            {
                if (AtWaypoint())
                {
                    TimeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (TimeSinceArrivedAtWaypoint > WaypointDwellTime)
            {
                Movement.StartMoveAction(nextPosition, PatrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < WaypointTolerance;
        }

        private void CycleWaypoint()
        {
            CurrentWaypointIndex = Path.GetNextIndex(CurrentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return Path.GetWaypoint(CurrentWaypointIndex);
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