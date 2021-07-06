using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    enum ActionPattern
    {
        stay,
        Move,
        bodyBlow,
        jumpAttack,
        Dead
    }

  


    WaitForSeconds actionSetWait = new WaitForSeconds(0.5f);
    WaitForSeconds attackWait = new WaitForSeconds(1.7f);

    int state = 0;

    int stateRandom = 0;

    Animator animator;

    GameObject player;
    testDamege testDamege;

    //攻撃をわかりやすくするための色変更用
    Renderer renderer;

    //衝撃波の見た目（仮）
    [SerializeField]
    GameObject syougekiha;

    bool isBodyBlow = false;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        testDamege = player.GetComponent<testDamege>();
 
       StartCoroutine(ActionSet());
    }

    private void Update()
    {
    }



    //行動を決める
    IEnumerator ActionSet ()
    {

        //IsMoveがfalseの時動かない
        yield return new WaitUntil(()=> IsMove);
        //一旦待機状態にする
        state = 0;
        animator.SetInteger("State", 0);
        renderer.material.color = Color.black;
        //待つ
        yield return actionSetWait;

        //ランダムで行動を決める
        stateRandom =  Random.Range(1, 100);
        //行動を行う
        if (0 <= stateRandom && stateRandom < 20)
        {
            StartCoroutine(Stay());
            
        }
        else if(20 <= stateRandom && stateRandom < 60 )
        {
            StartCoroutine(BodyBlow());
            
        }
        else if(60 <= stateRandom && stateRandom <= 100)
        {
            StartCoroutine(JumpAtteck());
            
        }
        //アニメーターに状態を渡す
        animator.SetInteger("State", state);
        yield break;

    }

    public override void Dead()
    {
        renderer.material.color = Color.black;
        state = (int)ActionPattern.Dead;
        animator.SetInteger("State", state);
        Invoke("InvokeBaseDead", 1);
    }

    void InvokeBaseDead ()
    {
        base.Dead();
    }


    void PlayerLook()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }




    #region BossAction
    IEnumerator Stay ()
    {
        state = (int)ActionPattern.stay;
        renderer.material.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        PlayerLook();
        StartCoroutine(ActionSet());
        yield break;
    }
    IEnumerator BodyBlow ()
    {
        state = (int)ActionPattern.bodyBlow;

        renderer.material.color = Color.blue;
        isBodyBlow = true;
        Invoke("BodyBlowStop",0.15f);
        yield return attackWait;
        PlayerLook();
        StartCoroutine(ActionSet());
        yield break;
    }
    IEnumerator JumpAtteck ()
    {
        state = (int)ActionPattern.jumpAttack;
        renderer.material.color = Color.red;
        StartCoroutine(JumpAttackDamege());
        yield return attackWait;
        PlayerLook();
        StartCoroutine(ActionSet());
            
        yield break;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - new Vector3(0f, 0.5f, 0f), new Vector3(6, 1, 2));

    }
        
    IEnumerator JumpAttackDamege()
    {
        yield return new WaitForSeconds(0.75f);
        bool ishit = false;
        RaycastHit[] hit;
        int attackframe = 0;
        syougekiha.SetActive(true);
        while(attackframe < 250)
        {
            hit = Physics.BoxCastAll(transform.position - new Vector3(0f, 0.5f, 0f), new Vector3(3, 1, 2), -transform.up, Quaternion.identity,LayerMask.GetMask("Player"));
           if (!ishit)
           {
                testDamege.Damege();
                ishit = true;
                break;
           }
            
            attackframe++;
            yield return null;
        }
        syougekiha.SetActive(false);
        yield break;
        
    }
    void BodyBlowStop ()
    {
        isBodyBlow = false;
    }
    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        if(isBodyBlow && collision.gameObject.CompareTag("Player"))
        {
            testDamege.Damege();
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            testDamege.Damege();
        }
    }


    //ボスの体力確認用
    private void OnGUI()
    {
        GUILayout.Label($"BossHP : {Hp}");
        GUILayout.Label($"State : {(ActionPattern)state}");
    }

}
