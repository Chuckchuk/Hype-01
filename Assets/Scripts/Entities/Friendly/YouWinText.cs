using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinText : MonoBehaviour
{
    public Canvas WinText;

    void OnTriggerEnter(Collider collider){
        Debug.Log("TRIGGERED");
        if(collider.transform.tag == "Player" && WinText != null){
            WinText.gameObject.SetActive(true);
        }
    }
}
