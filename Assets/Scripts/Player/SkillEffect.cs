using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlaySkillEffect()
    {
        Debug.LogError("SkillEffect ������Ʈ�� ã��!");
        if (anim != null )
        {
            Debug.Log("tlqkf �ִµ�");
        }
        anim.SetTrigger("doSkill");
    }
}
