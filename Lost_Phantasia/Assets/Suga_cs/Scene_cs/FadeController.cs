using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        if (!FadeManager.isFadeInstance)
        {
            Instantiate(fade);
        }
    }
}
