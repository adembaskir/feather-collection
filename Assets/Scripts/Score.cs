using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    [SerializeField]public float score;
    private void Awake()
    {
        instance = this;
    }

}
