using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// USED TUTORIAL:
// https://www.youtube.com/watch?v=xppompv1DBg&ab_channel=Brackeys
public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    void Awake() {
        instance = this;
    }
    #endregion

    public GameObject player;
}
