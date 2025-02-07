using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public Button[] saveSlots; // 10개의 슬롯 버튼 배열
    public static int savePointIndex = 0; // 세이브 포인트 수치 (게임플레이에서 증가)
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
        Debug.Log($"최근 지점 : {currentSavePoint}");
        GenerateSaveSlots(savePointIndex);
    }

    void GenerateSaveSlots(int slotCount)
    {
        saveSlots = new Button[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            Vector3 uiPosition = new Vector3(uiPos[i,0], uiPos[i, 1], 0f);
            GameObject newSlot = Instantiate(slotPrefab, uiPosition, transform.rotation); // 슬롯 생성
            saveSlots[i] = newSlot.GetComponent<Button>();
            newSlot.transform.SetParent(SaveSlotCanvas.transform, false); // 캔버스의 Transform 지정
            newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(uiPos[i, 0], uiPos[i, 1]);
            // 슬롯의 버튼 클릭 이벤트 설정
            int slotIndex = i; // 클로저 문제 방지
            saveSlots[i].onClick.AddListener(() => OnSlotClicked(slotIndex));

            if (currentSelectedSlot == i)
            {
                Instantiate(currentSlot, saveSlots[i].transform.position, Quaternion.identity);
            }
        }
    }

    // 슬롯 선택 시 호출 (로드 기능 연결 가능)
    public void OnSlotClicked(int slotIndex)
    {
        if (slotIndex < savePointIndex)
        {
            currentSelectedSlot = slotIndex;
            Debug.Log($"Slot {slotIndex} 선택됨. 세이브 데이터를 로드합니다.");
            SceneManager.LoadScene("LevelDesign");
        }
        else
        {
            Debug.Log("이 슬롯은 아직 잠겨 있습니다.");
        }
    }
}
