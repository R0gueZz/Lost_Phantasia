using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected int Hp = 0;

    //動けるかどうかの設定 trueなら動ける
    public bool IsMove = true;


    //引数のdamegeは与えるダメージ
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
