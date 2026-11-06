using System.Collections;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider;
    float h;
    [SerializeField] private float maxSpeed = 6.0f;
    [SerializeField] private float jumpPower = 0.0f;
    Animator animator;
    bool wasHorizontalReleased = false;
    bool wasJumpPressed = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonUp("Horizontal"))
        {
            wasHorizontalReleased = true;
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

        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            wasJumpPressed = true;
            animator.SetBool("isJumping", true);
        }
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(Vector2.right * h * 2, ForceMode2D.Impulse); // 경사로를 오를 수 있도록 힘을 증가
        if (rigidbody.linearVelocityX > maxSpeed) // 우측 최고 속도 제한
        {
            rigidbody.linearVelocity = new Vector2(maxSpeed, rigidbody.linearVelocityY);
        }
        if (rigidbody.linearVelocityX < -maxSpeed) // 좌측 최고 속도 제한
        {
            rigidbody.linearVelocity = new Vector2(-maxSpeed, rigidbody.linearVelocityY);
        }

        if (wasHorizontalReleased) // 조작키에서 손을 뗐을 때 감속
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.normalized.x * 0.2f, rigidbody.linearVelocityY);
            wasHorizontalReleased = false;
        }

        if (wasJumpPressed)
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            wasJumpPressed = false;
        }

        // RayCast 오브젝트 검색을 위해 Ray를 쏘는 방식
        if (rigidbody.linearVelocity.y < 0)
        {
            Debug.DrawRay(rigidbody.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    // Debug.Log(rayHit.collider.name);
                    animator.SetBool("isJumping", false);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.gameObject.tag == "Monster" && rigidbody.linearVelocityY < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    void OnDamaged(Vector2 enemyPos)
    {
        gameManager.HealthDown();
        gameManager.ScoreDown();
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - enemyPos.x > 0 ? 1 : -1;
        rigidbody.AddForce(new Vector2(dirc, 2) * 6, ForceMode2D.Impulse);
        animator.SetBool("isJumping", true);
        // Debug.Log(dirc);

        animator.SetTrigger("DoDamaged");
        if (gameManager.health > 0)
        {
            Invoke("OffDamaged", 1);
        }
    }

    void OffDamaged()
    {
        gameObject.layer = 3;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnAttack(Transform enemyTransform)
    {
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy.OnDamaged();
        gameManager.stageScore += 100;
        rigidbody.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            string itemTag = collision.gameObject.tag;
            switch (itemTag)
            {
                case "Bronze":
                    gameManager.stageScore += 50;
                    break;
                case "Silver":
                    gameManager.stageScore += 100;
                    break;
                case "Gold":
                    gameManager.stageScore += 300;
                    break;
                default:
                    break;
            }
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            gameManager.NextStage();
            collision.gameObject.SetActive(false);
        }
    }

    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        collider.enabled = false;
        rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
}