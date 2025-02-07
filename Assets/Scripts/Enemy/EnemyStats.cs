using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float curHp;
    public float maxHp=3f;
    public float damage=1f;

    protected float fadeDuration = 0.5f;
    protected bool isDie=false;
    virtual protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHp = 3f;
        curHp = maxHp;
        damage = 1f;
    }
    virtual public void TakeDamage(float damage,Transform player)
    {
        if (isDie) return;
        
        if (this.gameObject.transform.rotation.y == 0)//���� ������ ���� ���� ��
        {
            if (player.transform.position.x < gameObject.transform.position.x)//�÷��̾ ���� ���ʿ� ���� ��� 
            {
                Debug.Log("Enemy hit" + damage);
                curHp -= damage;
            }
            else
            {
                Debug.Log("BackAttack!! Enemy hit" + (damage+1));
                curHp -= damage + 1;
            }
        }
        else //���� �������� �������� �� 
        {
            if (player.transform.position.x < gameObject.transform.position.x)//�÷��̾ ���� ���ʿ� ���� ��� 
            {
                curHp -= damage + 1;//�����
                Debug.Log("BackAttack!! Enemy hit" + (damage + 1));
            }
            else
            {
                Debug.Log("Enemy hit" + damage);
                curHp -= damage ;
            }
        }
        CheckHp();
    }
    
    virtual protected void CheckHp()
    {
        if (curHp <= 0)
        {
            isDie = true;
            StartCoroutine("FadeOutAndDestroy");
        }
    }
    // 사망시 투명도 줄이고 삭제
    IEnumerator FadeOutAndDestroy()
    {
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
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
