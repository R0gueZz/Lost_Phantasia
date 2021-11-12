using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDes : MonoBehaviour
{
    void Start()
    {
        Invoke("BreakEffect", 1f);
    }

    void BreakEffect()
    {
        Destroy(gameObject);
    }
}
