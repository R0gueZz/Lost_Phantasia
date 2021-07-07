using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDamege : MonoBehaviour
{
    [SerializeField]
    EnemyBase enemyBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && enemyBase != null)  
        {
            Debug.Log("Damege");
            enemyBase.Damege(1);
        }
        if (Input.GetKeyDown(KeyCode.Space) && enemyBase != null)
        {
            enemyBase.Dead();
        }

    }

    public void Damege ()
    {
        Debug.Log("Damege");
    }
}
