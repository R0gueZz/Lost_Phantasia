using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected int Hp = 0;

    //“®‚¯‚é‚©‚Ç‚¤‚©‚Ìİ’è true‚È‚ç“®‚¯‚é
    public bool IsMove = true;


    //ˆø”‚Ìdamege‚Í—^‚¦‚éƒ_ƒ[ƒW
    virtual public void Damege(int damege)
    {
        Hp -= damege;
        if (Hp <= 0)
        {
            IsMove = false;
            Invoke("Dead",1);
        }
    }

    virtual public void Dead()
    {
        Animator animator;
        if (animator = GetComponent<Animator>())
        {
            animator.SetBool("IsDead", true);
        }
        Debug.Log("Dead");
        Destroy(gameObject);
    }
}
