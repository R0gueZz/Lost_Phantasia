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
    float dashSpeed;

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
    static public bool rolling = false;
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
        Avoid_Search();
        Attack_Search();
        Move();
    }

    //移動
    void Move()
    {
        //移動拒否
        if (!do_move|!avoid)
        {
            move_SlideTimer = 0f;
            return;
        }
        //移動の補間
        Vector3 target = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        target.x = Mathf.Lerp(0, target.x, move_SlideTimer);
        move_SlideTimer += Time.deltaTime / slideDuration;
　　　　//ダッシュ
        if(target.sqrMagnitude >0 && Input.GetButton("LB")||target.sqrMagnitude >0 && Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector3(target.x * dashSpeed,rb.velocity.y,0);
            anim.SetFloat("Sword_Blend", 2);
            transform.rotation = Quaternion.LookRotation(target);
            rolling = true;
        }
        //通常
        else if (target.sqrMagnitude > 0)
        {
            rb.velocity = new Vector3(target.x * speed, rb.velocity.y, 0);
            anim.SetFloat("Sword_Blend", target.magnitude);
            rolling = false;
            transform.rotation = Quaternion.LookRotation(target);
        }
        else
        {
            anim.SetFloat("Sword_Blend", target.magnitude * 0.95f * Time.deltaTime);
            rolling = false;
        }
    }

    //ジャンプ
    void Jump()
    {
        if(!do_move)
        {
            return;
        }
        else if (grounded && Input.GetKeyDown(KeyCode.Space)
            ||Input.GetButtonDown("Jump")&& grounded)
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
        else if (avoid && grounded && Input.GetKeyDown(KeyCode.W) 
            ||avoid && grounded && Input.GetButtonDown("RB"))
        {
            rolling = true;
            anim.SetFloat("Sword_Blend", 0);
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

    //回避中か確認
    void Avoid_Search()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Avoidance"))
        {
            avoid = false;
        }
    }

    void Avoid_Start()
    {
        rb.velocity = Vector3.zero;
        Vector3 avoidance = gameObject.transform.rotation * new Vector3(0, 0, avoid_force);
        rb.AddForce(avoidance, ForceMode.Impulse);
    }

    void invin_end()
    {
        rolling = false;
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

