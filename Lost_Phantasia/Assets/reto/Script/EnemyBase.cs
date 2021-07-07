using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected float Hp = 0;

    //“®‚¯‚é‚©‚Ç‚¤‚©‚ÌÝ’è true‚È‚ç“®‚¯‚é
    public bool IsMove = true;


    //ˆø”‚Ìdamege‚Í—^‚¦‚éƒ_ƒ[ƒW
    virtual public void Damege(int damege)
    {
       
        if(Hp > 0)
        {
            Hp -= damege;
        }
        if(0 >= Hp)
        {
            IsMove = false;
            Dead();
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
