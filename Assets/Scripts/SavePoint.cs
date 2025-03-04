using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] int savePos = 0;


    public static int savePointIndex = 0;
    public static int currentSavePoint = 0;
    private bool usedSave = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!usedSave)
            {
                savePointIndex += 1;
                usedSave = true;
                Debug.Log(savePointIndex);
            }
            currentSavePoint = savePos;
        }
    }
}
