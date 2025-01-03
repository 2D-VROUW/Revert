using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public Button[] saveSlots; // 10���� ���� ��ư �迭
    public static int savePoint; // ���̺� ����Ʈ ��ġ (�����÷��̿��� ����)
    public static int currentSavePoint;
    public GameObject slotPrefab;

    float[,] uiPos = {
        { -7f,  1.6f },
        { -3.5f, 1.6f },
        { 0f, 1.6f },
        { 3.5f,  1.6f },
        { 7f, 1.6f },
        { -7f,  -1.6f },
        { -3.5f, -1.6f },
        { 0f, -1.6f },
        { 3.5f,  -1.6f },
        { 7f, -1.6f }
    };
    void Start()
    {
        UpdateSaveSlots();
    }

    // ���̺� ���� Ȱ��ȭ/��Ȱ��ȭ ���� ������Ʈ
    protected void UpdateSaveSlots()
    {
        for (int i = 0; i < saveSlots.Length; i++)
        {
            if (i < savePoint)
            {
                saveSlots[i].interactable = true; // Ȱ��ȭ
            }
            else
            {
                saveSlots[i].interactable = false; // ��Ȱ��ȭ
            }
        }
    }
    void GenerateSaveSlots(int slotCount)
    {
        saveSlots = new Button[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            Vector3 uiPosition = new Vector3(uiPos[i,0], uiPos[i, 1], 0f);
            GameObject newSlot = Instantiate(slotPrefab, uiPosition, transform.rotation); // ���� ����
            saveSlots[i] = newSlot.GetComponent<Button>();

            // ������ ��ư Ŭ�� �̺�Ʈ ����
            int slotIndex = i; // Ŭ���� ���� ����
            saveSlots[i].onClick.AddListener(() => OnSlotClicked(slotIndex));
        }
    }

    // ���� ���� �� ȣ�� (�ε� ��� ���� ����)
    public void OnSlotClicked(int slotIndex)
    {
        if (slotIndex < savePoint)
        {
            Debug.Log($"Slot {slotIndex + 1} ���õ�. ���̺� �����͸� �ε��մϴ�.");
            // ���⿡ �ε� ���� �߰�
        }
        else
        {
            Debug.Log("�� ������ ���� ��� �ֽ��ϴ�.");
        }
    }
}
