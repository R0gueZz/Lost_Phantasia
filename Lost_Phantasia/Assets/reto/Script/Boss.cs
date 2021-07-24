//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Boss : EnemyBase
//{
//    enum ActionPattern
//    {
//        stay,
//        Move,
//        bodyBlow,
//        jumpAttack,
//        Dead
//    }
//    Rigidbody rb;
  


//    WaitForSeconds actionSetWait = new WaitForSeconds(1f);
//    WaitForSeconds attackWait = new WaitForSeconds(1.7f);

//    int state = 0;

//    int stateRandom = 0;

//    Animator animator;

//    GameObject player;
//    testDamege testDamege;

//    //�U�����킩��₷�����邽�߂̐F�ύX�p
//    Renderer renderer;
//    bool isBodyBlow = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        renderer = GetComponent<Renderer>();
//        animator = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody>();
//        player = GameObject.FindGameObjectWithTag("Player");
//        testDamege = player.GetComponent<testDamege>();

//        //StartCoroutine(ActionSet());
//        StartCoroutine(Move());
//    }

//    private void Update()
//    {
        
//    }



//    //�s�������߂�
//    IEnumerator ActionSet ()
//    {
//        //IsMove��false�̎������Ȃ�
//        yield return new WaitUntil(()=> IsMove);
//        //��U�ҋ@��Ԃɂ���
//        state = 0;
//        animator.SetInteger("State", 0);
//        renderer.material.color = Color.black;
//        //�҂�
//        yield return actionSetWait;
//        if (transform.position.x - player.transform.position.x >= 5 || transform.position.x - player.transform.position.x <= -5)
//        {
//            StartCoroutine(Move());
//            yield break;
//        }

//        //�����_���ōs�������߂�
//        stateRandom =  Random.Range(1, 100);
//        //�s�����s��
//        if (0 <= stateRandom && stateRandom < 10)
//        {
//            StartCoroutine(Stay());
            
//        }
//        if(10 <= stateRandom && stateRandom < 20)
//        {
//            StartCoroutine(Move());
//        }
//        else if(20 <= stateRandom && stateRandom < 60 )
//        {
//            StartCoroutine(BodyBlow());
            
//        }
//        else if(60 <= stateRandom && stateRandom <= 100)
//        {
//            StartCoroutine(JumpAtteck());
            
//        }
//        //�A�j���[�^�[�ɏ�Ԃ�n��
//        animator.SetInteger("State", state);
//        yield break;

//    }

//    public override void Dead()
//    {
//        renderer.material.color = Color.black;
//        state = (int)ActionPattern.Dead;
//        animator.SetInteger("State", state);
//        Invoke("InvokeBaseDead", 1);
//    }

//    void InvokeBaseDead ()
//    {
//        base.Dead();
//    }


//    void PlayerLook()
//    {
//        if (transform.position.x > player.transform.position.x)
//        {
//            transform.rotation = Quaternion.Euler(0, 0, 0);
//        }
//        else
//        {
//            transform.rotation = Quaternion.Euler(0, 180, 0);
//        }
//    }




//    #region BossAction
//    IEnumerator Stay ()
//    {
//        state = (int)ActionPattern.stay;
//        renderer.material.color = Color.black;
//        yield return new WaitForSeconds(0.5f);
//        PlayerLook();
//        StartCoroutine(ActionSet());
//        yield break;
//    }
//    IEnumerator Move ()
//    {
//        PlayerLook();
//        state = (int)ActionPattern.Move;
//        renderer.material.color = Color.green;
//        for(int i = 0; i <= 1500 && (transform.position.x - player.transform.position.x >= 2 || transform.position.x - player.transform.position.x <= -2); i++)
//        {

//            rb.constraints = (RigidbodyConstraints)124;
//            rb.velocity =  new Vector3(Mathf.Clamp(rb.velocity.x +transform.position.x - player.transform.position.x,-1,1), 0, 0).normalized ;
//            yield return null;
            
//            if(i % 20 == 0)
//            {
//                PlayerLook();
//            }
//        }
//        PlayerLook();
//        StartCoroutine(ActionSet());
//        rb.constraints = RigidbodyConstraints.FreezeAll;
//        yield break;
//    }

//    IEnumerator BodyBlow ()
//    {
//        state = (int)ActionPattern.bodyBlow;
//        rb.constraints = (RigidbodyConstraints)124;
//        renderer.material.color = Color.blue;
//        isBodyBlow = true;
//        Invoke("BodyBlowStop",0.15f);
//        yield return attackWait;
//        PlayerLook();
//        StartCoroutine(ActionSet());
//        yield break;
//    }
//    IEnumerator JumpAtteck ()
//    {
//        state = (int)ActionPattern.jumpAttack;
//        renderer.material.color = Color.red;
//        rb.constraints = (RigidbodyConstraints)122;
//        StartCoroutine(JumpAttackDamege());
//        yield return attackWait;
//        PlayerLook();
//        StartCoroutine(ActionSet());
//        rb.constraints = (RigidbodyConstraints)126;

//        yield break;
//    }


//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube(transform.position - new Vector3(0f, 0.5f, 0f), new Vector3(6, 1, 2));

//    }


//    bool ishit = false;
//    RaycastHit[] hit;
//    int attackframe = 0;
//    IEnumerator JumpAttackDamege()
//    {
//        ishit = false;   
//        yield return new WaitForSeconds(1.5f);
//        for(attackframe = 0; attackframe < 200;attackframe++)
//        {
           
//            Debug.Log("aa");
//            hit = Physics.BoxCastAll(transform.position - new Vector3(0f, 0.5f, 0f), new Vector3(3, 1, 2), -transform.up, Quaternion.identity, LayerMask.GetMask("Player"));
//            foreach(var i in hit)
//            {
//                if(i.collider.gameObject.CompareTag("Player"))
//                {
//                    testDamege.Damege();
//                    yield break;
//                }
//            }
            
//            attackframe++;
//            yield return null;
//        }
        
//        yield break;
        
//    }
//    void BodyBlowStop ()
//    {
//        isBodyBlow = false;
//    }
//    #endregion
//    private void OnCollisionEnter(Collision collision)
//    {
//          //�^�b�N���������ς���
//        if(isBodyBlow && collision.gameObject.CompareTag("Player"))
//        {
//            Debug.Log("BodyBlow");
//            testDamege.Damege();
//        }
//        //�^�b�N�����ł͂Ȃ��Ƃ��̐ڐG�_���[�W
//        if(collision.gameObject.CompareTag("Player"))
//        {
//            testDamege.Damege();
//        }
//    }


//    //�{�X�̗̑͊m�F�p
//    private void OnGUI()
//    {
//        GUILayout.Label($"BossHP : {Hp}");
//        GUILayout.Label($"State : {(ActionPattern)state}");
//    }

//}
