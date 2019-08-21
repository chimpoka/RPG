using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float HealthPoints = 100f;

        private bool isDead = false;
        public bool IsDead { get => isDead; set => isDead = value; }

        public void TakeDamage(float damage)
        {
            HealthPoints = Mathf.Max(HealthPoints - damage, 0);   
            if (HealthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (IsDead) return;

            GetComponent<Animator>().SetTrigger("Die");
            IsDead = true;
            GetComponent<ActionSchaduler>().CancelCurrentAction();
        }
    }
}