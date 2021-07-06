using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected float Hp = 0;

    //動けるかどうかの設定 trueなら動ける
    public bool IsMove = true;


    //引数のdamegeは与えるダメージ
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
