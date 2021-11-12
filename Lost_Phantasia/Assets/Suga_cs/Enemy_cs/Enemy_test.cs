using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_test : MonoBehaviour
{
    Rigidbody rb;

    float maxHp;
    float currentHp;

    [SerializeField]
    GameObject sounds;

    [Header("�_���[�W")]
    [SerializeField]
    float dmg = 12.5f;

    [Header("�v���C���[")]
    [SerializeField]
    GameObject player;

    [Header("�m�b�N�o�b�N�̋���")]
    [SerializeField]
    float[] knockForce;

    [Header("HP�o�[")]
    [SerializeField]
    Slider hpBar;

    [SerializeField]
    GameObject destroyEffect;

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
            EffectPlay();
            gameObject.SetActive(false);
        }
    }

    void EffectPlay()
    {
        GameObject effect = Instantiate(destroyEffect);
        GameObject sound = Instantiate(sounds);

        sound.transform.position = gameObject.transform.position;
        effect.transform.position = gameObject.transform.position;
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
