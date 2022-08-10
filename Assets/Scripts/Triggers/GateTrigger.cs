using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Feather")
        {
            other.GetComponent<Collision>()?.Gate(); // E�er De�en objenin i�eresinde collision scripti var ise �al���yor.
        }
    }
}
