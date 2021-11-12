using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameC : MonoBehaviour
{
    public static bool isClear = false;

    private void Start()
    {
        isClear = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isClear = true;
        }
    }
}
