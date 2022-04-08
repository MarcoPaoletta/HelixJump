using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    void OnCollisionExit(Collision collision)
    {
        GameManager.singleton.NextLevel();
    }
}
