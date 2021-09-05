using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockon : MonoBehaviour
{
    [Header("“ª‚ÌˆÊ’u")]
    [SerializeField]
    Transform head;

    [SerializeField]
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        head.LookAt(target.transform);
    }
}
