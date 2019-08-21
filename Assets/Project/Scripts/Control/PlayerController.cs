using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Camera MainCamera;
        private Mover PlayerMovement;
        private Health HealthComponent;

        void Start()
        {
            MainCamera = Camera.main;
            PlayerMovement = GetComponent<Mover>();
            HealthComponent = GetComponent<Health>();
        }

        void Update()
        {
            if (HealthComponent.IsDead) return;

            // Mouse cursor located on enemy
            if (InteractWithCombat()) return;
            // Mouse cursor located on landscape
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    PlayerMovement.StartMoveAction(hit.point);  
                }
                return true;
            }
            return false;
        }

        private void MoveToCursor()
        {
           
        }

        private Ray GetMouseRay()
        {
            return MainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}


