using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float avoid_force;

    //Durarionの時間
    [SerializeField]
    [Min(0)]
    float slideDuration;

    float move_SlideTimer = 0f;

    private bool do_move;
    static public bool avoid = true;
    static public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
        Avoidance();
    }
    private void FixedUpdate()
    {
        Attack_Search();
        Move();
    }

    //移動
    void Move()
    {
        if (!do_move|!avoid)
        {
            move_SlideTimer = 0f;
            return;
        }
        Vector3 target = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        target.x = Mathf.Lerp(0, target.x, move_SlideTimer);
        move_SlideTimer += Time.deltaTime / slideDuration;
        if (target.sqrMagnitude > 0)
        {
            rb.velocity = new Vector3(target.x * speed, rb.velocity.y, 0);
            anim.SetFloat("Sword_Blend", target.magnitude);

            transform.rotation = Quaternion.LookRotation(target);
        }
        else
        {
            anim.SetFloat("Sword_Blend", 0);
        }
    }

    //ジャンプ
    void Jump()
    {
        if(!do_move)
        {
            return;
        }
        else if (grounded && Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Jump")&& grounded)
        {
            this.rb.AddForce(transform.up * this.jumpForce,ForceMode.Impulse);
            anim.SetBool("Jump", true);
            grounded = false;
        }
    }

    //回避
    void Avoidance()
    {
        if(!do_move)
        {
            return;
        }
        else if (avoid && grounded && Input.GetKeyDown(KeyCode.LeftShift) 
            ||avoid && grounded && Input.GetButtonDown("button1"))
        {
            rb.velocity = Vector3.zero;
            Vector3 avoidance = gameObject.transform.rotation * new Vector3(0, 0, avoid_force);
            rb.AddForce(avoidance, ForceMode.Impulse);
            anim.SetBool("Avoidance",true);
            avoid = false;
        }
    }

    //攻撃中か確認
    void Attack_Search()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
           anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
           anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            anim.SetFloat("Sword_Blend", 0);
            do_move = false;
        }
        else
        {
            do_move = true;
        }
    }

    void Avoid_end()
    {
        anim.SetBool("Avoidance", false);
        avoid = true;
    }
    #region 接地判定
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

