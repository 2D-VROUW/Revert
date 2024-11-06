using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHp : MonoBehaviour
{
    //���� ü��
    [SerializeField] private float curHealth;
    //�ִ� ü��
    [SerializeField] public float maxHealth;
    //HP ����
    public Slider HpBarSlider;
    Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;
    public void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    public void SetUp(float amount)
    {
        maxHealth = amount;
        curHealth = maxHealth;
    }

    
    public void CheckHp() //hp ��� ������Ʈ
    {
        if (HpBarSlider != null)
            HpBarSlider.value = curHealth / maxHealth;
    }

    public void Damage(float damage) //������ ����
    {
        if (maxHealth == 0 || curHealth <= 0) //ü�� 0���� �н�
            return;
        curHealth -= damage;
        CheckHp();
        if (curHealth <= 0)
        {
            //ü�� 0 �÷��̾� ���
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            OnDamaged(collision.transform.position);
    }
    void OnDamaged(Vector2 targetPos)
    {
        //PlayerDamaged 11���̾�
        gameObject.layer = 11;
        //�÷��̾� ��������
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        //�ǰ� �� �ڷ� ������
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);

        Invoke("OffDamaged", 0.5f);
    }

    void OffDamaged()
    {
        //�������� Ǯ��
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
