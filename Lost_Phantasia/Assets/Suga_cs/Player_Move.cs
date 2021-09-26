using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    //走るスピード
    [SerializeField]
    float speed;
    //ジャンプ力
    [SerializeField]
    float jumpForce;

    static public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    //移動
    void Move()
    {
        Vector3 target = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (target.sqrMagnitude > 0)
        {
            transform.position += transform.forward * target.magnitude * speed * Time.deltaTime;
            anim.SetFloat("Normal_Blend", target.magnitude);
            transform.rotation = Quaternion.LookRotation(target);
        }
    }

    //ジャンプ
    void Jump()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            this.rb.AddForce(transform.up * this.jumpForce);
            anim.SetBool("Jump", true);
            grounded = false;
        }
    }

    #region
    //接地判定

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Jump", false);
            grounded = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
    #endregion
}

