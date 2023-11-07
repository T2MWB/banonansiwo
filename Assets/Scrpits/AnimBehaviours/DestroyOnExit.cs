/*
Erstellung: 3.6.2023
Authoren: vElumi, Tammo Wiebe
Nutzen: Zerstört das Objekt welches den animator besitzt nach der Animation an der dieses Programm als Behaviour hinzugefügt ist.
Änderungen:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.gameObject, stateInfo.length - 0.2f);
    }
}
