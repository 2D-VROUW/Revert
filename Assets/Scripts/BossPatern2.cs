using System.Collections;
using UnityEngine;

public class BossPatern2 : MonoBehaviour
{
    public GameObject leg;
    private GameObject legAttack;

    [SerializeField] private float speed = 100f;
    [SerializeField] private float attackTurm = 3f;

    private int legWhere;
    private int currentLegWhere = 0;

    private float randomX;
    private float randomY;
    private int direction = 1;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMovingForward = true;

    float[,] legPositions = {
        { -22f,  3f, 1 },
        { -22f, -2f, 1 },
        { -22f, -7f, 1 },
        {  22f,  3f, -1 },
        {  22f, -2f, -1 },
        {  22f, -7f, -1 }
    };

    void Update()
    {
        if (legAttack != null)
        {
            // �պ� �̵� ����
            if (isMovingForward)
            {
                legAttack.transform.position = Vector3.MoveTowards(legAttack.transform.position, targetPos, speed * Time.deltaTime);
                if (Vector3.Distance(legAttack.transform.position, targetPos) < 0.1f)
                {
                    isMovingForward = false;  // ��ǥ�� �����ϸ� �ݴ�� �̵�
                }
            }
            else
            {
                legAttack.transform.position = Vector3.MoveTowards(legAttack.transform.position, startPos, speed * Time.deltaTime);
                if (Vector3.Distance(legAttack.transform.position, startPos) < 0.1f)
                {
                    isMovingForward = true;  // �ٽ� ���� ��ġ�� ���ư��� ������ �̵�
                }
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("����");
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("����2");

        // ���� �ε��� ����
        legWhere = Random.Range(0, legPositions.GetLength(0));

        // �� �Ҵ�
        randomX = legPositions[legWhere, 0];
        randomY = legPositions[legWhere, 1];
        direction = (int)legPositions[legWhere, 2];

        currentLegWhere = legWhere;

        // ���� ����
        Vector3 legDirection = leg.transform.localScale;
        if (direction == 1)
        {
            legDirection.x = Mathf.Abs(legDirection.x);
        }
        else
        {
            legDirection.x = -Mathf.Abs(legDirection.x);
        }

        // �ٸ� ���� ����
        Vector2 SpawnPos = new Vector2(randomX, randomY);
        legAttack = Instantiate(leg, SpawnPos, transform.rotation);
        legAttack.transform.localScale = legDirection;

        // �պ��� ��ǥ ��ġ ����
        startPos = legAttack.transform.position;
        targetPos = new Vector3(randomX + 20f * direction, randomY, startPos.z);  // 5 ������ ��ǥ ���� ����

        Destroy(legAttack, 3f);  // ���� �ð� �Ŀ� �ٸ� ���� ����
    }
}
