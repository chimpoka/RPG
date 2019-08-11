using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField]
        float WeaponRange = 2.0f;

        Transform Target;

        private void Update()
        {
            if (Target == null) return;
            
            bool isInRange = Vector3.Distance(transform.position, Target.position) < WeaponRange;
            if (!isInRange)
            {
                GetComponent<Mover>().MoveTo(Target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
            
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionSchaduler>().StartAction(this);
            Target = target.transform;
            
            Debug.Log("Attack");
        }

        public void CancelAttack()
        {
            Target = null;
        }
    }
}