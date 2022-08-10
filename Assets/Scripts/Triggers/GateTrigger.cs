using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Feather")
        {
            other.GetComponent<Collision>()?.Gate(); // Eðer Deðen objenin içeresinde collision scripti var ise çalýþýyor.
        }
    }
}
