using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public Button[] saveSlots; // 10개의 슬롯 버튼 배열
    public static int savePoint; // 세이브 포인트 수치 (게임플레이에서 증가)
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

    // 세이브 슬롯 활성화/비활성화 상태 업데이트
    protected void UpdateSaveSlots()
    {
        for (int i = 0; i < saveSlots.Length; i++)
        {
            if (i < savePoint)
            {
                saveSlots[i].interactable = true; // 활성화
            }
            else
            {
                saveSlots[i].interactable = false; // 비활성화
            }
        }
    }
    void GenerateSaveSlots(int slotCount)
    {
        saveSlots = new Button[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            Vector3 uiPosition = new Vector3(uiPos[i,0], uiPos[i, 1], 0f);
            GameObject newSlot = Instantiate(slotPrefab, uiPosition, transform.rotation); // 슬롯 생성
            saveSlots[i] = newSlot.GetComponent<Button>();

            // 슬롯의 버튼 클릭 이벤트 설정
            int slotIndex = i; // 클로저 문제 방지
            saveSlots[i].onClick.AddListener(() => OnSlotClicked(slotIndex));
        }
    }

    // 슬롯 선택 시 호출 (로드 기능 연결 가능)
    public void OnSlotClicked(int slotIndex)
    {
        if (slotIndex < savePoint)
        {
            Debug.Log($"Slot {slotIndex + 1} 선택됨. 세이브 데이터를 로드합니다.");
            // 여기에 로드 로직 추가
        }
        else
        {
            Debug.Log("이 슬롯은 아직 잠겨 있습니다.");
        }
    }
}
