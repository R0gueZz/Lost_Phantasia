using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected float Hp = 0;

    protected bool ismove = true;

    virtual public void Damege (float damege )
    {
        Hp -= damege;
        if(Hp <= 0)
        {
            Dead();
        }
        return;
    }
    virtual public void Dead()
    {
        ismove = true;
        gameObject.SetActive(false);


    }
}
