using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected int Hp = 0;

    //�����邩�ǂ����̐ݒ� true�Ȃ瓮����
    public bool IsMove = true;


    //������damege�͗^����_���[�W
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
