using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !Shield.Instance.shieldIsActiv)
        {
            Debug.Log("Player");
            GameManager.Instance.ResetPlayer();
        }
    }
}
