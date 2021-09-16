using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status : MonoBehaviour
{
    float maxHp;
    float hp;

    [Header("HPÉoÅ[")]
    [SerializeField]
    Slider[] hpBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100;
        hp = maxHp;
        hpBar[0].value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Damage();
    }

    void Damage()
    {   
        if(Input.GetKeyDown(KeyCode.P))
        {
            hp = hp - 10f;

            hpBar[0].value = (float)hp / (float)maxHp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossAttack"))
        {
            Damage();
        }
    }
}
