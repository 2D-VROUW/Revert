using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public GameObject heartPrefab;  // ��Ʈ ������
    public GameObject diePanel;
    public int maxHP = 5;  // �ִ� ü��
    public int currentHP = 5;  // ���� ü��
    public List<GameObject> heartObjects = new List<GameObject>();  // ��Ʈ GameObject ����Ʈ

    private bool isInvincible = false;  // ���� ���� ����
    public float invincibilityDuration = 1f;  // ���� ���� ���� �ð�
    private DiePanel dP;

    void Start()
    {
        dP = diePanel.GetComponent<DiePanel>();
        CreateHearts();  // ���� ���� �� ��Ʈ ��ü ����
        UpdateHearts();  // �ʱ� ��Ʈ �̹��� ������Ʈ
    }

    // ��Ʈ �������� �̿��� ��Ʈ ��ü ����
    void CreateHearts()
    {
        for (int i = 0; i < maxHP; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform); // �������� �ν��Ͻ�ȭ�Ͽ� �θ�� ����
            heartObjects.Add(heart);  // ��Ʈ ��ü ����Ʈ�� �߰�
        }
    }

    // ü�� ���� ó��
    public void TakeDamage(int damage, Vector2 targetpos)
    {
        // ���� ������ ��� ������ ��ȿȭ
        if (isInvincible) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            currentHP = 0;
            dP.Bravo6();
            Invoke("Die", 1f);
        }

        // ��Ʈ ������Ʈ
        UpdateHearts();

        // ���� ���� ����
        StartCoroutine(InvincibilityCoroutine());
    }

    // ��Ʈ ���� ������Ʈ
    void UpdateHearts()
    {
        for (int i = 0; i < heartObjects.Count; i++)
        {
            // ���� ü�¿� �´� ��Ʈ�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
            heartObjects[i].SetActive(i < currentHP);
        }
    }

    // �÷��̾� ��� ó��
    public void Die()
    {
        Debug.Log("�÷��̾� ���");
        SceneManager.LoadScene("Gameover");
    }

    // ���� ���� �ڷ�ƾ
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;  // ���� ���� ����
        yield return new WaitForSeconds(invincibilityDuration);  // ������ �ð� ���� ���
        isInvincible = false;  // ���� ���� ����
    }
}
