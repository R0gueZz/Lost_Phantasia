using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SwordAttack : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    [Header("攻撃エフェクト配列")]
    [SerializeField]
    ParticleSystem[] slash_Effects;

    //攻撃判定
    public static bool nowAttack = false;

    //アタック時の移動
    [Header("攻撃時の移動力")]
    [SerializeField]
    float[] Attack_Speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Sword_Attack();
    }

    //剣攻撃
    void Sword_Attack()
    {
        if (Player_Move.grounded && Input.GetMouseButtonDown(0) && !Player_Move.rolling || 
            Player_Move.grounded && Input.GetButtonDown("Fire1") && !Player_Move.rolling)
        {
            anim.SetTrigger("Attack");
        }
    }

    void Attack()
    {
        //攻撃
        Debug.Log("Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boss")&& nowAttack)
        {
            Attack();
        }
    }

    //アタック時の移動量
    #region
    void Attack_Start1()
    {
        Vector3 power_a = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        nowAttack = true;
        if (power_a.sqrMagnitude >= 0.01f)
        {
            return;
        }
        else
        {
            rb.velocity = Vector3.zero;
            Vector3 force = gameObject.transform.rotation * new Vector3(0, 0, Attack_Speed[0]);
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
    void Attack_Start2()
    {
        nowAttack = true;
        Vector3 force = gameObject.transform.rotation * new Vector3(0, 0, Attack_Speed[1]);
        rb.AddForce(force, ForceMode.Impulse);
    }

    void Attack_Start3()
    {
        nowAttack = true;
        Vector3 force = gameObject.transform.rotation * new Vector3(0, 0, Attack_Speed[3]);
        rb.AddForce(force, ForceMode.Impulse);
    }

    void Attack_Start4()
    {
        nowAttack = true;
        Vector3 force = gameObject.transform.rotation * new Vector3(0, 0, Attack_Speed[2]);
        rb.AddForce(force, ForceMode.Impulse);
    }
    #endregion

    //アタック終了
    #region
    void Attack_End1()
    {
        nowAttack = false;
    }

    void Attack_End2()
    {
        nowAttack = false;
    }

    void Attack_End3()
    {
        nowAttack = false;
    }

    void Attack_End4()
    {
        nowAttack = false;
    }
    #endregion

    //エフェクト発生
    #region
    void Effect_Start1()
    {
        slash_Effects[0].Play();
    }
    
    void Effect_Start2()
    {
        slash_Effects[1].Play();
    }

    void Effect_Start3()
    {
        slash_Effects[3].Play();
    }

    void Effect_Start4()
    {
        slash_Effects[2].Play();
    }
    #endregion
}
