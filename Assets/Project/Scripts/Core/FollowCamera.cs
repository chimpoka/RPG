using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform Target;

        void LateUpdate()
        {
            transform.position = Target.position;
        }

        //private Vector3 StartRotation;

        //private void Start()
        //{
        //    StartRotation = transform.eulerAngles;
        //}

        //private void Update()
        //{
        //    transform.eulerAngles = StartRotation;
        //}
    }
}