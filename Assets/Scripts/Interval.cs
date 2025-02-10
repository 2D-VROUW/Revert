using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interval : MonoBehaviour
{
    [SerializeField] private GameObject Size;

    public int numOfEnemies = 0;
    public bool isPlayerInterval = false;
    public bool stageClear = false;
    public bool doorOpen = false;
    public GameObject[] door;
    private float doorMoveTime = 1f; // ���� �ö󰡴� �ð�
    private float fadeDuration = 1f; // ������� �ð�

    void Start()
    {
        DetectEnemies();

        SpriteRenderer spr = Size.GetComponent<SpriteRenderer>();
        Color color = spr.color;
        color.a = 0f;
        spr.color = color;

        foreach (GameObject d in door)
        {
            if (d != null)
            {
                d.transform.position -= new Vector3(0, 20f, 0); // ó���� ���� �Ʒ��� �̵�
                d.SetActive(false);
            }
        }
    }

    void Update()
    {
        DetectEnemies();
        if (isPlayerInterval && !doorOpen)
        {
            doorOpen = true;
            foreach (GameObject d in door)
            {
                if (d != null)
                {
                    d.SetActive(true);
                    MoveDoorSmoothly(d, 20f); // �� ������ �ö󰡱�
                }
            }
        }

        if (isPlayerInterval && numOfEnemies <= 0)
            stageClear = true;

        if (stageClear && isPlayerInterval)
        {
            foreach (GameObject d in door)
            {
                if (d != null)
                {
                    StartCoroutine(FadeOutAndDestroy(d)); // ������ ���̸� ������ ����
                }
            }
        }
    }

    void DetectEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Size.transform.position, Size.transform.localScale, 0f);
        int currentEnemy = 0;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                currentEnemy++;
            }
        }
        numOfEnemies = currentEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInterval = true;
        }
    }

    // ���� ������ �ø��� �Լ�
    private void MoveDoorSmoothly(GameObject door, float moveAmount)
    {
        StartCoroutine(MoveDoorCoroutine(door, moveAmount));
    }

    IEnumerator MoveDoorCoroutine(GameObject door, float moveAmount)
    {
        Vector3 startPosition = door.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, moveAmount, 0);
        float elapsedTime = 0f;

        while (elapsedTime < doorMoveTime)
        {
            door.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / doorMoveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.position = targetPosition;
    }

    // ���� ������ ������� ���� �� �����ϴ� �Լ�
    IEnumerator FadeOutAndDestroy(GameObject door)
    {
        SpriteRenderer sr = door.GetComponent<SpriteRenderer>(); // ������ SpriteRenderer ��������
        if (sr == null) yield break; // SpriteRenderer ������ �ߴ�

        Color color = sr.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            sr.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        sr.color = color;
        Destroy(door); // ���������� �� ����
    }
}
