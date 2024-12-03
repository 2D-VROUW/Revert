using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float curHp;
    public float maxHp=3f;
    public float damage=1f;

    protected bool isDie=false;
    protected bool isDamage=false;
    virtual protected void Awake()
    {
        maxHp = 3f;
        curHp = maxHp;
        damage = 1f;
    }
    virtual public void TakeDamage(float damage,Transform player)
    {
        if (isDamage||isDie) return;
        ChangeState(true);
        if (this.gameObject.transform.rotation.y != 0)//���� ������ ���� ���� ��
        {
            if (player.transform.position.x < gameObject.transform.position.x)//�÷��̾ ���� ���ʿ� ���� ��� 
            {
                curHp -= damage;
            }
            else
            {
                curHp -= damage + 1;
            }
        }
        else //���� �������� �������� �� 
        {
            if (player.transform.position.x < gameObject.transform.position.x)//�÷��̾ ���� ���ʿ� ���� ��� 
            {
                curHp -= damage + 1;//�����
            }
            else
            {
                curHp -= damage ;
            }
        }

        Invoke("ChangeState(false)", 0.05f);
        CheckHp();
    }
    protected void ChangeState(bool state)
    {
        isDamage = state;
    }
    protected void CheckHp()
    {
        if (curHp <= 0)
        {
            isDie = true;
        }
        Destroy(this.gameObject,1f);
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckHp();
        }
    }
    protected bool IsDie()//use different script
    {
        return isDie;
    }
}
