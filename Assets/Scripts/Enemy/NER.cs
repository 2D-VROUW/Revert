using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NER : EnemyMove
{
    public GameObject attackRange;
    public GameObject NER_bullet;
    private GameObject target;

    private CircleCollider2D rangeCollider;
    private BoxCollider2D takeDamageCollider;
    private SpriteRenderer spriteRenderer;

    private float attackTurm = 0f;
    private bool canAttack;
    private bool Wait = true;

    //명령
    private void Update()
    {
        if (isStunned == true) return;
        anim.speed = 1f * 2f / attackSpeed;
        if (canAttack && Wait)
        {
            anim.SetTrigger("isTrigger");
            Invoke("Attack", 0.1f);
            StartCoroutine("WaitAttack");
        }
        if (target != null)
        {
            spriteRenderer.flipX = target.transform.position.x < transform.position.x;
        }
    }
    //사거리 받기
    void Start()
    {
        attackSpeed = 2f;
        rangeCollider = attackRange.GetComponent<CircleCollider2D>();
        rangeCollider.isTrigger = true;
        takeDamageCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //플레이어 감지
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = collision.gameObject;
            anim.SetBool("isReady", true);
            StopMoving();
            speed = 0;
            StartCoroutine("Aim");
        }
    }
    //플레이어 놓침
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
            StopMoving();
            anim.SetBool("isReady", false);
            canAttack = false;
            speed = 2.5f;
            StopCoroutine("Aim");
        }
    }
    //조준시간
    private IEnumerator Aim()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    //공격 텀
    private IEnumerator WaitAttack()
    {
        Wait = false;
        yield return new WaitForSeconds(attackSpeed);
        Wait = true;
    }
    //실제 공격
    private void Attack()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        directionToPlayer.z = 0f;

        GameObject cpy_bullet = Instantiate(NER_bullet, transform.position, transform.rotation);
        Destroy(cpy_bullet, 5f);

        NER_bullet bulletComponent = cpy_bullet.GetComponent<NER_bullet>();
        bulletComponent.SetDirection(directionToPlayer);
    }
    //이동 애니메이션 관리
    protected override void StartMoving()
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", true);
        }
    }

    protected override void StopMoving()
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", false);
        }
    }
}
