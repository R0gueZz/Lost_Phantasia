using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    //çUåÇ
    void Attack()
    {
        if (Player_Move.grounded && Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }
}
