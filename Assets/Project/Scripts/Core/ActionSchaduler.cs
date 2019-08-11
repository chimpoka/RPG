using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionSchaduler : MonoBehaviour
    {
        MonoBehaviour CurrentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (CurrentAction == action) return;
            if (CurrentAction != null)
            {
                print("Cancelling" + CurrentAction);
            }
            CurrentAction = action;
        }
    }
}


