using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour
{
    float maxHp;
    float hp;
    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Damage()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossAttack"))
        {
            Damage();
        }
    }
}
