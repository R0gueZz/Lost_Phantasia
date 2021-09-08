using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : Player_Status
{
    Rigidbody rb;
    Animator anim;

    [Header("通常時のスピード")]
    [SerializeField]
    float speed;

    [Header("ダッシュ時のスピード")]
    [SerializeField]
    float dashSpeed;

    [Header("ジャンプの力")]
    [SerializeField]
    float jumpForce;

    [Header("回避の力")]
    [SerializeField]
    float avoid_force;

    //Durarionの時間
    [Header("攻撃終了から走り出す時の補間")]
    [SerializeField]
    [Min(0)]
    float slideDuration;

    float move_SlideTimer = 0f;

    [Header("ダッシュアニメーションの補間")]
    [SerializeField]
    float lerpDuration;

    float lerpProb = 0f;

    //条件処理用変数
    private bool do_move;
    static public bool avoid = true;
    static public bool grounded;
    static public bool rolling = false;
    static public bool dash = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Effecter();
        Jump();
        Avoidance();
    }
    private void FixedUpdate()
    {
        Avoid_Search();
        Attack_Search();
        Move();
    }

    //エフェクト管理
    void Effecter()
    {
        if(!do_move)
        {
            dash = false;
            rolling = false;
        }
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
        
        Vector3 target = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        //補間
        target.x = Mathf.Lerp(0, target.x, move_SlideTimer);

        move_SlideTimer += Time.deltaTime / move_SlideTimer;

        float value = Mathf.Lerp(target.magnitude, 2, lerpProb);

        //ダッシュ
        if (target.sqrMagnitude >0 && Input.GetButton("LB")||target.sqrMagnitude >0 && Input.GetKey(KeyCode.LeftShift))
        {
            lerpProb += Time.deltaTime / lerpDuration;
            lerpProb = Mathf.Clamp(lerpProb, 0, 1);

            rb.velocity = new Vector3(target.x * dashSpeed,rb.velocity.y,0);
            anim.SetFloat("Sword_Blend",value);
            transform.rotation = Quaternion.LookRotation(target);
            dash = true;
            rolling = false;
        }
        //通常
        else if (target.sqrMagnitude > 0)
        {
            lerpProb -= Time.deltaTime / lerpDuration;
            lerpProb = Mathf.Clamp(lerpProb, 0, 1);

            rb.velocity = new Vector3(target.x * speed, rb.velocity.y, 0);
            anim.SetFloat("Sword_Blend", value);
            dash = false;
            rolling = false;
            transform.rotation = Quaternion.LookRotation(target);
        }
        else
        {
            anim.SetFloat("Sword_Blend", target.magnitude);
            dash = false;
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
        else if (grounded && Input.GetKeyDown(KeyCode.Space) && !rolling
            ||Input.GetButtonDown("Jump") && grounded && !rolling)
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
            dash = false;
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
           anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")||
           anim.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))       
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

