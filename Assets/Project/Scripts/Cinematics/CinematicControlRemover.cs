﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject Player;

        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            Player = GameObject.FindWithTag("Player");
        }

        private void DisableControl(PlayableDirector pd)
        {
            Player.GetComponent<ActionSchaduler>().CancelCurrentAction();
            Player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControl(PlayableDirector pd)
        {
            Player.GetComponent<PlayerController>().enabled = true;
        }
    }
}