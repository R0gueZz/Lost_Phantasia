using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    enum ActionPattern
    {
        stay,
        bodyBlow,
        jumpAttack,
    }

    int state = 0;

    int tmp = 0;

    Animator animator;

    GameObject player;

    //攻撃をわかりやすくするための色変更用
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
 
       StartCoroutine(ActionSet());
    }

    // Update is called once per frame
    void Update()
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
        yield return new WaitForSeconds(0.5f);

        //ランダムで行動を決める
        tmp =  Random.Range(1, 100);

        Debug.Log(state);
        //行動を行う
        if (0 <= tmp && tmp < 20)
        {
            StartCoroutine(Stay());
            state = (int)ActionPattern.stay;
            renderer.material.color = Color.black;
        }
        else if(20 <= tmp && tmp < 60 )
        {
            StartCoroutine(BodyBlow());
            state = (int)ActionPattern.bodyBlow;

            renderer.material.color = Color.blue;
        }
        else if(60 <= tmp && tmp <= 100)
        {
            StartCoroutine(JumpAtteck());
            state = (int)ActionPattern.jumpAttack;
            renderer.material.color = Color.red;
        }
        //アニメーターに状態を渡す
        animator.SetInteger("State", state);
        yield break;

    }

    public override void Dead()
    {
        renderer.material.color = Color.black;
        animator.SetInteger("State", 0);
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
        Debug.Log("Stay");
        yield return new WaitForSeconds(0.5f);
        PlayerLook();
        StartCoroutine(ActionSet());
        yield break;
    }
    IEnumerator BodyBlow ()
    {
        Debug.Log("BodyBlow");
        yield return new WaitForSeconds(1.7f);
        PlayerLook();
        StartCoroutine(ActionSet());
        yield break;
    }
    IEnumerator JumpAtteck ()
    {
        Debug.Log("JumpAtteck");
        yield return new WaitForSeconds(1.7f);
        PlayerLook();
        StartCoroutine(ActionSet());
        yield break;
    }
    #endregion 

    //ボスの体力確認用
    private void OnGUI()
    {
        GUILayout.Label($"BossHP : {Hp}");
        GUILayout.Label($"State : {(ActionPattern)state}");
    }

}
