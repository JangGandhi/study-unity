using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider;
    public int nextMove;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        Think();
    }

    void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rigidbody.position.x + (nextMove * 0.5f), rigidbody.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // Ray로 한 수 앞 위치의 바닥 확인
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null) // 바닥이 없다?!
        {
            nextMove *= -1; // 반대 방향으로 설정
            spriteRenderer.flipX = nextMove == 1;
            CancelInvoke("Think");
            Invoke("Think", 3);
        }
        rigidbody.linearVelocity = new Vector2(nextMove, rigidbody.linearVelocity.y);
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2); // -1 이상 2 미만
        Invoke("Think", Random.Range(3, 6));
        animator.SetInteger("WalkSpeed", nextMove);
        if (nextMove != 0) // 0일 때는 기존의 방향을 유지
        {
            spriteRenderer.flipX = nextMove == 1; // 왼쪽으로 이동 시 false, 오른쪽으로 이동 시 true
        }
    }

    public void OnDamaged()
    {
        CancelInvoke("Think"); // 이제 그만 생각해

        nextMove = 0; // 이제 그만 움직여
        animator.SetInteger("WalkSpeed", nextMove);

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        collider.enabled = false;
        rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("DeActive", 3);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}