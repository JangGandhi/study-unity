using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int stageScore;
    public int totalScore;
    public int stageIndex;
    public int health;
    public GameObject[] stages;
    public Image[] lifes;
    public TextMeshProUGUI stageIndexOutput;
    public TextMeshProUGUI scoreOutput;
    public Button continueButton;

    void Update()
    {
        stageIndexOutput.text = $"STAGE {stageIndex + 1}";
        scoreOutput.text = $"{totalScore + stageScore:000000}";
    }

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
            stageIndex--;
            Time.timeScale = 0;
            TextMeshProUGUI buttonText = continueButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Retry?";
            continueButton.gameObject.SetActive(true);
        }
    }

    public void HealthDown()
    {
        health--;
        lifes[health].color = new Color(1, 1, 1, 0.4f);
        if (health == 0)
        {
            continueButton.gameObject.SetActive(true);
        }

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
            if (health - 1 > 0)
            {
                HealthDown();
                player.RepositionPlayer();
            }
            else
            {
                player.OnDie();
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}