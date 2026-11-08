using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int stageScore;
    public int totalScore;
    public int stageIndex;
    public int health;

    public void NextStage()
    {
        stageIndex++;
        totalScore += stageScore;
        stageScore = 0;
    }

    public void HealthDown()
    {
        health--;
    }

    public void ScoreUp(int score)
    {
        stageScore += score;
    }

    public void ScoreDown()
    {
        if (stageScore > 0)
        {
            stageScore -= 50;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Player가 떨어져 죽었을 경우
        {
            HealthDown();
            if (health > 0)
            {
                collision.attachedRigidbody.linearVelocity = Vector2.zero;
                collision.transform.position = new Vector2(-3.5f, 4.5f); // 시작 지점으로 이동
            }
        }
    }
}