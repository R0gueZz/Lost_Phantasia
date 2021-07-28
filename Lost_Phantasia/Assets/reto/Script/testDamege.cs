using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDamege : MonoBehaviour
{
    [SerializeField]
    EnemyBase enemyBase;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && enemyBase != null)  
        {
            enemyBase.Damege(1);
        }
        if (Input.GetKeyDown(KeyCode.Space) && enemyBase != null)
        {
           // enemyBase.Dead();
        }
        
    }

    void aaa()
    {
        rb.AddForce(-10, 0, 0,ForceMode.Impulse);
    }

    public void Damege ()
    {
        Debug.Log("PlayerDamege");
    }
    
}
