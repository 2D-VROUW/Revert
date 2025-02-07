using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public Button[] saveSlots; // 10���� ���� ��ư �迭
    public static int savePointIndex = 0; // ���̺� ����Ʈ ��ġ (�����÷��̿��� ����)
    public static int currentSavePoint;
    public static int currentSelectedSlot; 
    public GameObject slotPrefab;
    public GameObject currentSlot;
    public Transform SaveSlotCanvas;


    float[,] uiPos = {
        { -400f,  100f },
        { -200f, 100f },
        { 0f, 100f },
        { 200f,  100f },
        { 400f, 100f },
        { -400f,  -100f },
        { -200f, -100f },
        { 0f, -100f },
        { 200f,  -100f },
        { 400f, -100f }
    };
    void Start()
    {
        Debug.Log($"�ֱ� ���� : {currentSavePoint}");
        GenerateSaveSlots(savePointIndex);
    }

    void GenerateSaveSlots(int slotCount)
    {
        saveSlots = new Button[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            Vector3 uiPosition = new Vector3(uiPos[i,0], uiPos[i, 1], 0f);
            GameObject newSlot = Instantiate(slotPrefab, uiPosition, transform.rotation); // ���� ����
            saveSlots[i] = newSlot.GetComponent<Button>();
            newSlot.transform.SetParent(SaveSlotCanvas.transform, false); // ĵ������ Transform ����
            newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(uiPos[i, 0], uiPos[i, 1]);
            // ������ ��ư Ŭ�� �̺�Ʈ ����
            int slotIndex = i; // Ŭ���� ���� ����
            saveSlots[i].onClick.AddListener(() => OnSlotClicked(slotIndex));

            if (currentSelectedSlot == i)
            {
                Instantiate(currentSlot, saveSlots[i].transform.position, Quaternion.identity);
            }
        }
    }

    // ���� ���� �� ȣ�� (�ε� ��� ���� ����)
    public void OnSlotClicked(int slotIndex)
    {
        if (slotIndex < savePointIndex)
        {
            currentSelectedSlot = slotIndex;
            Debug.Log($"Slot {slotIndex} ���õ�. ���̺� �����͸� �ε��մϴ�.");
            SceneManager.LoadScene("LevelDesign");
        }
        else
        {
            Debug.Log("�� ������ ���� ��� �ֽ��ϴ�.");
        }
    }
}
