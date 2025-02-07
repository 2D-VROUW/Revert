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

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = defaultZoom;  // �ʱ� �� ũ��
    }

    void LateUpdate()
    {
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
            // ����� ��ġ�� ������ ����
            Vector3 desiredPosition = target.position + offset;
            // �ε巴�� �̵�
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
