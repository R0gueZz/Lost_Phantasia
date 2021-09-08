using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_test : MonoBehaviour
{
    Rigidbody rb;

    float maxHp;
    float currentHp;

    [Header("ノックバックの強さ")]
    [SerializeField]
    float[] knockForce;

    [Header("HPバー")]
    [SerializeField]
    Slider hpBar;

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
            currentHp = currentHp - 10f;

            rb.AddForce(-transform.forward * knockForce[0],ForceMode.Impulse);
            rb.AddForce(transform.up * knockForce[1], ForceMode.Impulse);

            hpBar.value = (float)currentHp / (float)maxHp;
        }
    }
}
