using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public int nextMove;
    Animator animator;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Think();
    }
    void Start()
    {

    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        // 낭떠러지의 시간이다
        Vector2 frontVec = new Vector2(rigidbody.position.x + (nextMove * 0.5f) , rigidbody.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // Ray로 한 수 앞 위치의 바닥 확인
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null) // 바닥이 없을 경우
        {
            // Debug.Log("이 너머, 낭떠러지 있다");
            nextMove *= -1; // 역방향으로
        }
        rigidbody.linearVelocity = new Vector2(nextMove, rigidbody.linearVelocity.y);
        // CancelInvoke();
        // Invoke("Think", Random.Range(3, 6));

    }
    void Think()
    {
        nextMove = Random.Range(-1, 2); // -1 이상 2 미만
        Invoke("Think", Random.Range(3, 6));
    }
}