using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject tail;//������ ��

    static public Rigidbody2D FindBefore(HingeJoint2D linkedHinge)
    {
        HingeJoint2D cur = linkedHinge;
        Rigidbody2D connectedRigidbody = linkedHinge.connectedBody;
        Rope headRope;// ����Ƽ���� Static Rope�� Ropescript

        while (true)//Rope ��ũ��Ʈ�� �� ������Ʈ�� ã�� �� ���� ����
        {
            connectedRigidbody = cur.connectedBody;
            if (connectedRigidbody.GetComponent<Rope>() != null) break;
            cur = connectedRigidbody.GetComponent<HingeJoint2D>();
        }//Found head node

        headRope = connectedRigidbody.gameObject.GetComponent<Rope>();
        HingeJoint2D tailHj = headRope.tail.GetComponent<HingeJoint2D>();
        Rigidbody2D prevObj = headRope.tail.GetComponent<Rigidbody2D>();

        while (tailHj != linkedHinge)//linkedHinge�� ���� �ִ� ������Ʈ�� ã�� �� ���� ����
        {
            prevObj = connectedRigidbody;
            connectedRigidbody = tailHj.connectedBody;
            tailHj = connectedRigidbody.GetComponent<HingeJoint2D>();
        }
        return prevObj;
    }
    static public Rigidbody2D FindHead(HingeJoint2D linkedHinge)//FindBefore�κ��� Rope��ũ��Ʈ�� �� ������Ʈ�� ã�� ������ ��ũ��Ʈ
    {
        HingeJoint2D cur = linkedHinge;
        Rigidbody2D connectedRigidbody = linkedHinge.connectedBody;

        while (true)
        {
            connectedRigidbody = cur.connectedBody;
            if (connectedRigidbody.GetComponent<Rope>() != null) break;
            cur = connectedRigidbody.GetComponent<HingeJoint2D>();
        }//Found head node
        return connectedRigidbody;
    }
    public GameObject GetTail()
    {
        return tail;
    }
}
