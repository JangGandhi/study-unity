using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int stageScore;
    public int totalScore;
    public int stageIndex;
    public int health;
    public GameObject[] stages;

    public void NextStage()
    {
        stageIndex++;
        totalScore += stageScore;
        stageScore = 0;

        if (stageIndex < stages.Length)
        {
            stages[stageIndex - 1].SetActive(false);
            stages[stageIndex].SetActive(true);
            player.RepositionPlayer();
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("게임 클리어!");
        }
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
                player.RepositionPlayer();
            }
        }
    }
}