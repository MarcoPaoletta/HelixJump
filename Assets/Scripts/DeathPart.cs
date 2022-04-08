using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    void OnEnable() // called when the object becomes enabled and active
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}
