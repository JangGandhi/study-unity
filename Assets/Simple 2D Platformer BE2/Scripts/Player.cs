using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider;
    Animator animator;
    AudioSource audioSource;
    float h;
    [SerializeField] private float maxSpeed = 6.0f;
    [SerializeField] private float jumpPower = 0.0f;
    bool wasHorizontalReleased = false;
    bool wasJumpPressed = false;
    bool isInputEnabled = true;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isInputEnabled)
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

            if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
            {
                wasJumpPressed = true;
                animator.SetBool("isJumping", true);
            }
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
            PlayAudio("JUMP");
            wasJumpPressed = false;
        }

        if (rigidbody.linearVelocity.y < 0) // RayCast 오브젝트 검색을 위해 Ray를 쏘는 방식
        {
            Debug.DrawRay(rigidbody.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.8f)
                {
                    animator.SetBool("isJumping", false);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.gameObject.tag == "Monster" && rigidbody.linearVelocityY < 0 && transform.position.y > collision.transform.position.y) // Collision의 tag가 Monster면서 Player가 Collision보다 위에서 떨어졌을 경우
            {
                OnAttack(collision.transform); // Player의 공격으로 간주
            }
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    void OnDamaged(Vector2 enemyPos)
    {
        if (gameManager.health - 1 > 0) // Player의 체력이 남아 있는 경우
        {
            gameManager.HealthDown();
            gameManager.ScoreDown();
            gameObject.layer = LayerMask.NameToLayer("PlayerDamaged"); // Player를 공격받은 상태로 변경
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            int dirc = transform.position.x - enemyPos.x > 0 ? 1 : -1;
            rigidbody.AddForce(new Vector2(dirc * 2, 1) * 16, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            animator.SetTrigger("DoDamaged");
            Invoke("OffDamaged", 1); // 1초 뒤 공격받은 상태를 해제
            PlayAudio("DAMAGED");
        }
        else // Player의 체력이 남아 있지 않은 경우
        {
            OnDie();
        }
    }

    void OffDamaged()
    {
        gameObject.layer = 3;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnAttack(Transform enemyTransform) // Player의 공격
    {
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy.OnDamaged();
        gameManager.ScoreUp(100);
        rigidbody.AddForce(Vector2.up * 16, ForceMode2D.Impulse);
        PlayAudio("ATTACK");
    }
    public void OnDie()
    {
        gameManager.HealthDown();
        gameManager.ScoreDown();

        gameObject.layer = LayerMask.NameToLayer("PlayerDamaged"); // Player를 공격받은 상태로 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        isInputEnabled = false;
        spriteRenderer.flipY = true;
        collider.enabled = false;
        rigidbody.AddForce(Vector2.up * 16, ForceMode2D.Impulse);
        animator.speed = 0;
        PlayAudio("DIE");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item")) // Player가 Item을 획득
        {
            string itemTag = collision.gameObject.tag;
            switch (itemTag)
            {
                case "Bronze":
                    gameManager.ScoreUp(50);
                    break;
                case "Silver":
                    gameManager.ScoreUp(100);
                    break;
                case "Gold":
                    gameManager.ScoreUp(300);
                    break;
                default:
                    break;
            }
            collision.gameObject.SetActive(false);
            PlayAudio("ITEM");
        }

        if (collision.gameObject.CompareTag("Finish")) // Player가 Finish에 도착
        {
            gameManager.NextStage();
            collision.gameObject.SetActive(false);
            PlayAudio("FINISH");
        }
    }

    public void RepositionPlayer()
    {
        rigidbody.linearVelocity = Vector2.zero;
        transform.position = new Vector2(-3.5f, 4.5f);
    }

    void PlayAudio(string name)
    {
        switch (name)
        {
            case "JUMP":
                {
                    audioSource.clip = audioJump;
                    break;
                }
            case "ATTACK":
                {
                    audioSource.clip = audioAttack;
                    break;
                }
            case "DAMAGED":
                {
                    audioSource.clip = audioDamaged;
                    break;
                }
            case "ITEM":
                {
                    audioSource.clip = audioItem;
                    break;
                }
            case "DIE":
                {
                    audioSource.clip = audioDie;
                    break;
                }
            case "FINISH":
                {
                    audioSource.clip = audioFinish;
                    break;
                }
            default:
                {
                    break;
                }
        }
        audioSource.Play();
    }
}