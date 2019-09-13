using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float WeaponRange = 2f;
        [SerializeField] float TimeBetweenAttacks = 1f;
        [SerializeField] float Damage = 5f;

        Health Target;
        float TimeSinceLastAttack = Mathf.Infinity;



        private void Update()
        {
            TimeSinceLastAttack += Time.deltaTime;
            

            if (Target == null) return;
            if (Target.IsDead) return;

            bool isInRange = Vector3.Distance(transform.position, Target.transform.position) < WeaponRange;
            if (!isInRange)
            {
                GetComponent<Mover>().MoveTo(Target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(Target.transform);
            if (TimeSinceLastAttack >= TimeBetweenAttacks)
            {
                TriggerAttack();
                TimeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            // This will trigger the Hit() event
            GetComponent<Animator>().SetTrigger("Attack");
        }

        // Animation Event
        public void Hit()
        {
            if (Target == null) return;
            print("Hit");
            Target.TakeDamage(Damage);
        }

        // Can 'this' attack 'target'
        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            Health targetToTest = target.GetComponent<Health>();
            return (targetToTest != null && !targetToTest.IsDead);
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionSchaduler>().StartAction(this);
            Target = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            GetComponent<Mover>().Cancel();
            Target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }
    }
}