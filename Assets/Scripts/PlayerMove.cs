using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�÷��̾� �¿� �̵�
    [SerializeField] private float speed = 8f;//�÷��̾� ���ǵ�
    private float moveInput = 0f;//�÷��̾� �¿��̵� input
    private bool isFacingRight = true;//�¿� ó�ٺ��°�

    //�÷��̾� ����
    private float jumpingPower = 16f;//���� ����
    
    //�÷��̾� �뽬
    private bool isDash=false;
    private bool canDash = true;
    [SerializeField] private float dashDuration = 0.2f;//�뽬 ���ӽð�
    [SerializeField] private float dashCoolTime = 2.0f;//�뽬 ��Ÿ��
    [SerializeField] private float dashSpeed = 10.0f;//�뽬 �ӵ�

    //�׿�
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    // Update is called once per frame
    void Update()
    {
        
        if( Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }
        else if ( Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }
        else
        {
            moveInput = 0f;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash&&!isDash) 
        {
            StartCoroutine(dash());
        }
        Flip();
    }

    IEnumerator dash()
    {
        isDash = true;
        canDash = false;
        
        Debug.Log("Dash!");
        
        //rb.AddForce(new Vector2(horizontal* dashSpeed, 1f), ForceMode2D.Impulse);  // ��� �� ���ϱ�

        float dashDirection = transform.localScale.x > 0 ? 1 : -1;
        rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);

        yield return new WaitForSeconds(dashDuration);
        isDash = false;
        yield return new WaitForSeconds(dashCoolTime);
        canDash = true;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        /*if(coll.collider.CompareTag("Rope"))
        {
            Rope rope=coll.gameObject.GetComponent<Rope>();
            rope.AnchorPlayer(this.gameObject.transform);
        }*///���� �׼Ǿ��� ���� ����
    }
}


