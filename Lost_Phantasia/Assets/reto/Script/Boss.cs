using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    enum BossActionPattern
    {
        Stay,
        Move,
        BodyBlow,
        JumpAttack,
        Dead
    }
    float Fps = 400;

    Rigidbody rb;
    Animator animator;


    GameObject Player;
    [SerializeField]
    GameObject shockWave;
    [SerializeField]
    GameObject BodyBlowDecision;

    [SerializeField]
    float movetime;

    int state = 0;

    Ray Groundray;

    int layer;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        layer = LayerMask.NameToLayer("Ground");
        StartCoroutine(ActionSet());
    }

    float tmp;
    int frameCount = 0;
    float prevTime = 0;
    void Update()
    {
       
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            Debug.LogFormat("{0}fps", frameCount / time);
            Fps = frameCount / time;

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
    private void OnGUI()
    {
        GUILayout.Label($"HP : {Hp}");
        GUILayout.Label($"{(BossActionPattern)state}");
    }

    #region BossAction
    IEnumerator ActionSet()
    {
        if(!ismove)
        {
            yield break;
        }
        if (Vector3.Distance(transform.position,Player.transform.position) > 8)
        {
            StartCoroutine(Move());
            yield break;
        }
        tmp = Random.RandomRange(0, 10001) % 100;
        if (0 <= tmp && tmp < 20)
        {   
            StartCoroutine(Move());
        }
        else if (20 <= tmp && tmp < 60)
        {
            state = (int)BossActionPattern.Stay;
            animator.SetInteger("State", state);
            for(int i = 0; i < Fps * 0.5f; i++)
            {
                yield return null;
            }

            StartCoroutine(JumpAttack());
        }
        else if (60 <= tmp && tmp < 100)
        {
            state = (int)BossActionPattern.Stay;
            animator.SetInteger("State", state);
            for (int i = 0; i < Fps * 0.5f; i++)
            {
                yield return null;
            }
            StartCoroutine(BodyBlow());
        }
        yield break;
    }

    float moveStopDistance = 1;
    float motionTime = 0;
    IEnumerator Move()
    {
        if (!ismove)
        {
            yield break;
        }
        motionTime = 0;
       
        state = (int)BossActionPattern.Move;
        animator.SetInteger("State", state);

        while (motionTime < movetime && Vector3.Distance(transform.position, Player.transform.position) > 1.5f)
        {
            
            if (!ismove)
            {
                yield break;
            }

            rb.AddForce(-(transform.position.x - Player.transform.position.x), 0, 0);
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -2, 2), rb.velocity.y, rb.velocity.z);
            motionTime += Time.deltaTime;
            LookPlayer();
            yield return null;

        }

        state = (int)BossActionPattern.Stay;
        animator.SetInteger("State", state);
        for (int i = 0; i < Fps * 0.5f; i++)
        {
            yield return null;
        }
        StartCoroutine(ActionSet());
        yield break;
    }


    RaycastHit raycastHit;
    IEnumerator JumpAttack()
    {
        if (!ismove)
        {
            yield break;
        }
       
        state = (int)BossActionPattern.JumpAttack;
        animator.SetInteger("State", state);
        rb.AddForce(0, 10, 0, ForceMode.Impulse);
        for (int i = 0; i < Fps * 2f; i++)
        {
            yield return null;
        }
        LookPlayer();
        if (!ismove)
        {
            yield break;
        }

        yield return new WaitUntil(() => Physics.Raycast(transform.position, -transform.up, out raycastHit, 1.1f) && !raycastHit.collider.CompareTag("Ground"));

        if (!ismove)
        {
            yield break;
        }
        shockWave.SetActive(true);
        for (int i = 0; i < Fps * 1f; i++)
        {
            yield return null;
        }

        if (!ismove)
        {
            yield break;
        }
        shockWave.SetActive(false);
        state = (int)BossActionPattern.Stay;
        animator.SetInteger("State", state);
        for (int i = 0; i < Fps * 1; i++)
        {
            yield return null;
        }
        LookPlayer();
        if (!ismove)
        {
            yield break;
        }
        
        StartCoroutine(ActionSet());
        yield break;
    }

    IEnumerator BodyBlow()
    {
        LookPlayer();
        if (!ismove)
        {
            yield break;
        }
        state = (int)BossActionPattern.BodyBlow;
        animator.SetInteger("State", state);
        for (int i = 0; i < Fps * 2; i++)
        {
            yield return null;
        }

        if (!ismove)
        {
            yield break;
        }
        rb.AddForce(transform.right * 7, ForceMode.Impulse);
        BodyBlowDecision.SetActive(true);
        for (int i = 0; i < Fps * 1; i++)
        {
            yield return null;
        }

        if (!ismove)
        {
            yield break;
        }
        BodyBlowDecision.SetActive(false);
        state = (int)BossActionPattern.Stay;
        animator.SetInteger("State", state);
        for (int i = 0; i < Fps * 1; i++)
        {
            yield return null;
        }

        if (!ismove)
        {
            yield break;
        }
        
        StartCoroutine(ActionSet());
        LookPlayer();
        yield break;

    }

    override public void Dead ()
    {
        state = (int)BossActionPattern.Dead;
        animator.SetInteger("State", state);
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        shockWave.SetActive(false);
        BodyBlowDecision.SetActive(false);
        Invoke("InvokeBaseDead", 1f);
    }

    void InvokeBaseDead()
    {
        base.Dead();
    }


    void LookPlayer()
    {
        transform.LookAt(Player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y - 90, 0);
        
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(state == (int)BossActionPattern.BodyBlow)
            {
                
            }
            if(state == (int)BossActionPattern.JumpAttack)
            {
                
            }
            
        }
    }

}
