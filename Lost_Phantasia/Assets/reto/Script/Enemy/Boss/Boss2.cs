using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
public class Boss2 : MonoBehaviour
{

    [Serializable]
    struct MoveState
    {
        //移動時間の最大
        public int frame;
        //移動スピード
        public float speed;
    }

    [Serializable]
    struct BodyBlowState
    {
        //突進のスピード
        public float speed;
        //突進の攻撃力
        public float attackPower;
    }
    [Serializable]
    struct JumpAttackState
    {
        //ジャンプ攻撃のスピード
        public float jumpPower;
        //ジャンプ攻撃の攻撃力
        public float attackPower;
        //衝撃破の残る時間
        public int shockWaveDisplayTime;
        //ジャンプ攻撃後の隙
        public int gap;
        //衝撃波
        public GameObject shockWave;
    }

    enum BossActionPattern
    {
        Move,
        BodyBlow,
        JumpAttack,
        Stay,
        Dead
    }

    
    Rigidbody rb;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    string groundMaskName;

    int groundMask;

    bool isGround;


    //現在のステータス
    BossActionPattern bossState;

    //移動のステータス
    [SerializeField]
    MoveState moveStatus;

    //突進のステータス
    [SerializeField]
    BodyBlowState bodyBlowStatus;

    //ジャンプ攻撃のステータス
    [SerializeField]
    JumpAttackState jumpAttackStatus;

    
    private void Start()
    {
        //配列にコルーチンを格納

        groundMask = LayerMask.NameToLayer(groundMaskName);
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ActionSelect());
    }

    int frameCount = 0;
    float prevTime = 0;
    float Fps;
    private void Update()
    {
        IsGround();
    }

    IEnumerator ActionSelect ()
    {
        while(true)
        {
            float rand = UnityEngine.Random.Range(0f, 100f);
            if (RandomDecision(rand, 0f, 10f))
            {
                bossState = BossActionPattern.Stay;
                yield return StartCoroutine(WaitFrame(1000));   
            }
            else if (RandomDecision(rand, 10f, 40f))
            {
                bossState = BossActionPattern.Move;
                yield return StartCoroutine(Move());
            }
            else if (RandomDecision(rand, 40f, 70f))
            {
                bossState = BossActionPattern.BodyBlow;
                yield return StartCoroutine(BodyBlow());
            }
            else if (RandomDecision(rand, 70f, 100f))
            {
                bossState = BossActionPattern.JumpAttack;
                yield return StartCoroutine(JumpAttack());
            }
            yield return null;
        }
        


    }

    bool RandomDecision (float value, float min,float max)
    {
        if (min <= value && value < max)
        {
            return true;
        }
        return false;
    }

    IEnumerator Move()
    {
        for (int i = 0; i < moveStatus.frame && Vector3.Distance(transform.position, Player.transform.position) > 3; i++)
        {
            rb.velocity = new Vector3(Mathf.Clamp(-(transform.position.x - Player.transform.position.x), -1, 1) * moveStatus.speed, rb.velocity.y, 0);

            Debug.Log((Mathf.Clamp(-(transform.position.x - Player.transform.position.x), -1, 1)));

            yield return null;
        }

        Vector3 lerpStarVel = rb.velocity; 
        for(float i = 1; i < 100 ; i++)
        { 
            rb.velocity = Vector3.Lerp(lerpStarVel, Vector3.zero, i / 100);
            yield return null;
        }
        rb.velocity = Vector3.zero;
        yield break;
    }
    IEnumerator BodyBlow()
    {
        rb.velocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezeAll - (int)RigidbodyConstraints.FreezePositionX;
        rb.AddForce(transform.forward * bodyBlowStatus.speed, ForceMode.Impulse);
        yield return null;
        yield return new WaitUntil(() => rb.velocity == Vector3.zero);
        rb.constraints -= (int)RigidbodyConstraints.FreezePositionY;
        yield break;
    }
    IEnumerator JumpAttack()
    {
        //Y軸のみ移動可能にする
        rb.constraints = RigidbodyConstraints.FreezeAll - (int)RigidbodyConstraints.FreezePositionY;

        rb.velocity = Vector3.zero;
        rb.AddForce(0, jumpAttackStatus.jumpPower, 0, ForceMode.Impulse);

        yield return null;
        //着地するまで待機
        yield return new WaitUntil(() => isGround);
        //着地すると衝撃波を出す
        jumpAttackStatus.shockWave.SetActive(true);
        //待機
        yield return StartCoroutine(WaitFrame(jumpAttackStatus.shockWaveDisplayTime));

        //衝撃波を消す
        jumpAttackStatus.shockWave.SetActive(false);

        //後隙
        yield return StartCoroutine(WaitFrame(jumpAttackStatus.gap));

        rb.constraints -= (int)RigidbodyConstraints.FreezePositionX;
        yield break;
    }

    IEnumerator WaitFrame(int waitFrame)
    {
        for (int i = 0; i < waitFrame; i++)
        {
            yield return null;
        }
    }

    void IsGround()
    {
        if (Physics.BoxCast(transform.position, new Vector3(transform.localScale.x / 2, 0.1f, 1), -transform.up,Quaternion.identity,1))
        {
            isGround = true;
            return;
        }
        isGround = false;
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
