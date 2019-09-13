using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool AlreadyTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (!AlreadyTriggered && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                AlreadyTriggered = true;
            }
        }
    }
}