using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent onEnter;
    private void OnTriggerEnter(Collider other)
    {
      onEnter?.Invoke();//(?) e�er null ise di�er tarafa ge�miyor
    }
}
