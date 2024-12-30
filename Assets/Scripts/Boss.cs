using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject patern2;

    [SerializeField] protected int hp = 100;
    [SerializeField] protected int attackSpeed = 10;

    protected int page = 1;
    private int attackPatern = 0;
    private int currentAttackPatern = 0;
    private bool canAttack = true;

    //���� ��ũ��Ʈ �ޱ�
    BossPatern2 B2;

    void Start()
    {
        B2 = patern2.GetComponent<BossPatern2>();
    }
    //���� ���
    private void Update()
    {
        if (canAttack)
        {
            Patern();
            StartCoroutine("CanAttack");
        }
        if (hp <= 60)
        {
            page = 2;
        }
    }
    //���� ����
    private void Patern()
    {
        attackPatern = 2;//Random.Range(1, 5);
        switch (attackPatern)
        {
            case 1:
                break;
            case 2:
                B2.Attack();
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }
    //�ǰ�
    public virtual void TakeDamage(int damage)
    {
        if (page == 2)
        {
            hp -= damage;
            if (hp <= 0)
                Destroy(this.gameObject);
        }
    }
    //���� ��
    private IEnumerator CanAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
