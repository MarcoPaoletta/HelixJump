using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassScorePoint : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        GameManager.singleton.AddScore(1);
    }
}
