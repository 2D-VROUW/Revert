using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;  // �÷��̾�
    private Vector3 offset = new Vector3(0, 2.5f, -10);
    private float smoothSpeed = 0.125f;  // �ε巯�� �̵� �ӵ�
    private float lastInputTime;  // ������ �Է� �ð�
    private float resetTime = 2f; // 3�� �� ���� ũ��� ���
    private Camera cam;
    private float defaultZoom = 10f; // �⺻ ī�޶� ũ��
    private float zoomedSize = 8f; // Ȯ��� ī�޶� ũ��
    private float zoomSpeed = 2f; // �� ��ȯ �ӵ�

    // ��鸲 ���� �߰�
    public float shakeDuration = 0.5f; // ��鸮�� �ð�
    public float shakeMagnitude = 1f; // ��鸮�� ����
    private bool isShaking = false; // ��鸲 ���� Ȯ��

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = defaultZoom;  // �ʱ� �� ũ��
    }

    void LateUpdate()
    {
        if (isShaking) return; // ��鸮�� ���ȿ��� �⺻ �̵��� ����

        bool isMoving = false;

        if (Input.GetKey(KeySetting.Keys[KeyAction.LEFT])) // ���� �̵�
        {
            lastInputTime = Time.time;
            isMoving = true;
        }
        else if (Input.GetKey(KeySetting.Keys[KeyAction.RIGHT])) // ���� �̵�
        {
            lastInputTime = Time.time;
            isMoving = true;
        }

        // �Է� �� �� Ȯ��
        if (isMoving)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomedSize, Time.deltaTime * zoomSpeed);
        }

        // 3�� ���� �Է��� ������ ���� ũ��� �ε巴�� ���
        if (Time.time - lastInputTime > resetTime)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, defaultZoom, Time.deltaTime * zoomSpeed);
        }

        if (target != null)
        {
            // ���� ������ �����ϸ鼭 �̵�
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    // ��ų ��� �� ȭ�� ���� �Լ� (���� ������ ����)
    public void StartShake()
    {
        if (!isShaking)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        isShaking = true;
        Vector3 basePosition = target.position + offset; // ��鸲 ���� ��ġ�� ������ ������ ������ ����
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = basePosition + new Vector3(x, y, 0f); // ���� ��ġ������ ��鸮�� ����

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = basePosition; // ��鸲 ���� �� ���� ��ġ(������ ����)�� ����
        isShaking = false;
    }
}
