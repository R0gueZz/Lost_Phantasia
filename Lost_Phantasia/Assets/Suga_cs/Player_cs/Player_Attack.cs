using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    [SerializeField]
    ParticleSystem[] slash_Effects;

    //�U������
    private bool nowAttack = false;

    //�A�^�b�N���̈ړ�
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
        Gun_Attack();
    }

    //���U��
    void Sword_Attack()
    {
        if (Player_Move.grounded && Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    //�e�U��
    void Gun_Attack()
    {
        if(Input.GetMouseButtonDown(1))
        {
            //�E�N���b�N�ōU��
        }
    }

    void Attack()
    {
        //�U��
        Debug.Log("Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boss")&& nowAttack)
        {
            Attack();
        }
    }

    //�A�^�b�N���̈ړ���
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
        Vector3 force = gameObject.transform.rotation * new Vector3(0, 0, Attack_Speed[2]);
        rb.AddForce(force, ForceMode.Impulse);
    }
    #endregion

    //�A�^�b�N�I��
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
    #endregion

    //�G�t�F�N�g����
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
        slash_Effects[2].Play();
    }
    #endregion
}
