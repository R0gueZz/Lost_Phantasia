using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_test : MonoBehaviour
{
    Rigidbody rb;

    float maxHp;
    float currentHp;

    float dmg = 25f;

    [Header("プレイヤー")]
    [SerializeField]
    GameObject player;

    [Header("ノックバックの強さ")]
    [SerializeField]
    float[] knockForce;

    [Header("HPバー")]
    [SerializeField]
    Slider hpBar;

    [Header("ダメージ")]
    [SerializeField]
    Text dmgText;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100f;
        currentHp = maxHp;
        hpBar.value = 1;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Blade") && Player_SwordAttack.nowAttack)
        {
            currentHp = currentHp - dmg;

            if(player.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                rb.AddForce(-transform.forward * knockForce[0], ForceMode.VelocityChange);
            }

            else if(player.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                rb.AddForce(transform.forward * knockForce[0], ForceMode.VelocityChange);
            }

            rb.AddForce(transform.up * knockForce[1], ForceMode.VelocityChange);

            hpBar.value = (float)currentHp / (float)maxHp;
        }
    }
}
