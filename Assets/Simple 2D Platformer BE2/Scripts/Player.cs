using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    float h;
    [SerializeField] private float maxSpeed = 5;

    bool isButtonUp = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonUp("Horizontal"))
        {
            isButtonUp = true;
        }
    }

    void FixedUpdate()
    {
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (rigid.linearVelocityX > maxSpeed) // 우측 최고 속도 제한
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocityY);
        }
        if (rigid.linearVelocityX < -maxSpeed) // 좌측 최고 속도 제한
        {
            rigid.linearVelocity = new Vector2(-maxSpeed, rigid.linearVelocityY);
        }

        if (isButtonUp) // 조작키에서 손을 뗐을 때 감속
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x * 0.5f, rigid.linearVelocityY);
            isButtonUp = false;
        }
    }
}