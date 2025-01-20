using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPManager : MonoBehaviour
{

    public static int hp = 5;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject life4;
    public GameObject life5;

    // Use this for initialization
    void Awake()
    {
        life1.GetComponent<Image>().enabled = true;
        life2.GetComponent<Image>().enabled = true;
        life3.GetComponent<Image>().enabled = true;
        life4.GetComponent<Image>().enabled = true;
        life5.GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (hp)
        {
            case 4:
                life5.GetComponent<Image>().enabled = false;
                break;
            case 3:
                life4.GetComponent<Image>().enabled = false;
                break;
            case 2:
                life3.GetComponent<Image>().enabled = false;
                break;
            case 1:
                life2.GetComponent<Image>().enabled = false;
                break;
            case 0:
                life1.GetComponent<Image>().enabled = false;
                //game over
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("hit");

            hp -= 1;
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;  // ��������ŭ ü�� ����

        if (hp <= 0)
        {
            Die();  // ü���� 0 ���Ϸ� �������� ���
        }

        UpdateLifeUI();  // ü�� UI ������Ʈ
    }

    private void UpdateLifeUI()
    {
        // HP�� ���� UI ���� ������ ��Ȱ��ȭ
        life1.GetComponent<Image>().enabled = hp >= 1;
        life2.GetComponent<Image>().enabled = hp >= 2;
        life3.GetComponent<Image>().enabled = hp >= 3;
        life4.GetComponent<Image>().enabled = hp >= 4;
        life5.GetComponent<Image>().enabled = hp >= 5;
    }


    private void Die()
    {
        Debug.Log("�÷��̾� ���");
        // �÷��̾� ��� ó�� (���� ���� ó�� ��).
        Destroy(gameObject);
    }
}