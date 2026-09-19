using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    float h;
    [SerializeField] private float maxSpeed = 6.0f;
    Animator animator;

    bool isButtonUp = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonUp("Horizontal"))
        {
            isButtonUp = true;
        }
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if (Mathf.Abs(rigidbody.linearVelocity.x) < 0.3f)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (rigidbody.linearVelocityX > maxSpeed) // 우측 최고 속도 제한
        {
            rigidbody.linearVelocity = new Vector2(maxSpeed, rigidbody.linearVelocityY);
        }
        if (rigidbody.linearVelocityX < -maxSpeed) // 좌측 최고 속도 제한
        {
            rigidbody.linearVelocity = new Vector2(-maxSpeed, rigidbody.linearVelocityY);
        }

        if (isButtonUp) // 조작키에서 손을 뗐을 때 감속
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.normalized.x * 0.2f, rigidbody.linearVelocityY);
            isButtonUp = false;
        }
    }
}