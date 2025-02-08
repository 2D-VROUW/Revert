using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private CameraMove cameraMove; // ī�޶� ����� ���� �߰�

    private void Start()
    {
        // ī�޶� ã��
        cameraMove = FindObjectOfType<CameraMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyMove enemyMove = collision.GetComponent<EnemyMove>();
                EnemyBehavior enemyBehavior = collision.GetComponentInParent<EnemyBehavior>();

                if (enemyMove != null)
                {
                    enemyMove.Stun(3f);
                }
                else if (enemyBehavior != null)
                {
                    enemyBehavior.Stun(3f);
                }
                else
                {
                    Debug.Log("����ù�");
                }

                // ���� ������ �� ȭ�� ���� ����
                if (cameraMove != null)
                {
                    cameraMove.StartShake();
                }
                else
                {
                    Debug.LogWarning("ī�޶� �� ã��");
                }
            }
        }
    }
}
