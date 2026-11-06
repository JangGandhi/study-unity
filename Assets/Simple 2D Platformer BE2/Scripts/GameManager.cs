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
        if (health == 0)
        {
            player.OnDie();
        }
    }
    public void ScoreDown()
    {
        if (stageScore > 0)
        {
            stageScore -= 50;
        }
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthDown();
            if (health > 0)
            {
                collision.attachedRigidbody.linearVelocity = Vector2.zero;
                collision.transform.position = new Vector2(-3.5f, 4.5f);
            }
        }
    }
}
