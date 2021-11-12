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
        //�ړ����Ԃ̍ő�
        public int frame;
        //�ړ��X�s�[�h
        public float speed;
    }

    [Serializable]
    struct BodyBlowState
    {
        //�ːi�̃X�s�[�h
        public float speed;
        //�ːi�̍U����
        public float attackPower;
    }
    [Serializable]
    struct JumpAttackState
    {
        //�W�����v�U���̃X�s�[�h
        public float jumpPower;
        //�W�����v�U���̍U����
        public float attackPower;
        //�Ռ��j�̎c�鎞��
        public int shockWaveDisplayTime;
        //�W�����v�U����̌�
        public int gap;
        //�Ռ��g
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


    //���݂̃X�e�[�^�X
    BossActionPattern bossState;

    //�ړ��̃X�e�[�^�X
    [SerializeField]
    MoveState moveStatus;

    //�ːi�̃X�e�[�^�X
    [SerializeField]
    BodyBlowState bodyBlowStatus;

    //�W�����v�U���̃X�e�[�^�X
    [SerializeField]
    JumpAttackState jumpAttackStatus;

    
    private void Start()
    {
        //�z��ɃR���[�`�����i�[

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
        //Y���݈̂ړ��\�ɂ���
        rb.constraints = RigidbodyConstraints.FreezeAll - (int)RigidbodyConstraints.FreezePositionY;

        rb.velocity = Vector3.zero;
        rb.AddForce(0, jumpAttackStatus.jumpPower, 0, ForceMode.Impulse);

        yield return null;
        //���n����܂őҋ@
        yield return new WaitUntil(() => isGround);
        //���n����ƏՌ��g���o��
        jumpAttackStatus.shockWave.SetActive(true);
        //�ҋ@
        yield return StartCoroutine(WaitFrame(jumpAttackStatus.shockWaveDisplayTime));

        //�Ռ��g������
        jumpAttackStatus.shockWave.SetActive(false);

        //�㌄
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
