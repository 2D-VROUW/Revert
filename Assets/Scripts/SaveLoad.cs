using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public static int savePointIndex = 10; // ���̺� ����Ʈ ��ġ (�����÷��̿��� ����)
    public static int currentSavePoint;
    public static int currentSelectedSlot;
    public GameObject[] slotPrefab;
    public GameObject currentSlot;
    public Transform SaveSlotCanvas;
    private GameObject selectMark;

    float[,] uiPos = {
        { -235.8f,  323.3f },
        { 45.2f, 224.7f },
        { -182.5f, 149.1f },
        { -64.5f,  8.5f },
        { -349.9f, -179.8f },
        { -188.7f,  -304.1f },
        { -159.2f, -200.5f },
        { 121.6f, -209.1f },
        { 271f,  -59.6f },
        { 513f, 147f }
    };

    void Start()
    {
        for (int i = 0; i < slotPrefab.Length; i++)
        {
            slotPrefab[i].gameObject.SetActive(false);
        }
        currentSlot.SetActive(false);
        Invoke("StartSlotFadeIn", 0.3f);
    }

    void StartSlotFadeIn()
    {
        StartCoroutine(FadeInSaveSlots());
    }

    IEnumerator FadeInSaveSlots()
    {
        for (int i = 0; i < slotPrefab.Length; i++)
        {
            if (i < savePointIndex)
            {
                slotPrefab[i].gameObject.SetActive(true);
                slotPrefab[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(uiPos[i, 0], uiPos[i, 1]);

                // CanvasGroup�� ������ �߰�
                CanvasGroup canvasGroup = slotPrefab[i].GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = slotPrefab[i].AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0f;

                // ������ õõ�� ����
                float duration = 0.5f; // ���̵��� ���� �ð�
                float elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                canvasGroup.alpha = 1f;
            }
            yield return new WaitForSeconds(0.02f); // ��ư�� ���������� ��Ÿ���� ����
        }
    }

    public void Selecting(int slotIndex)
    {
        currentSlot.SetActive(true);
        currentSlot.transform.position = slotPrefab[slotIndex].transform.position;
    }

    public void NotSelecting()
    {
        if (currentSlot != null)
        {
            currentSlot.SetActive(false);
        }
    }

    // ���� ���� �� ȣ�� (�ε� ��� ���� ����)
    public void OnSlotClicked(int slotIndex)
    {
        if (slotIndex < savePointIndex)
        {
            currentSelectedSlot = slotIndex;
            Debug.Log($"Slot {slotIndex} ���õ�. ���̺� �����͸� �ε��մϴ�.");
            SceneManager.LoadScene("1LevelDesign");
        }
        else
        {
            Debug.Log("�� ������ ���� ��� �ֽ��ϴ�.");
        }
    }

    public void PriateCalls()
    {
        SceneManager.LoadScene("PirateShip");
    }

    public void BossMab()
    {
        SceneManager.LoadScene("");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
