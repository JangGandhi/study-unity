using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello Unity!");

        // 1. 변수
        int level = 5;
        float weight = 91.8f; // 최적화를 위해 double보다 float를 자주 사용, 이때 실수만 입력하면 double 자료형으로 받아들이기에 f를 붙여 해당 값이 float 자료형임을 명시
        string playerName = "장훈진";
        bool isMaxLevel = false;

        Debug.Log("플레이어의 이름은?");
        Debug.Log(playerName);
        Debug.Log("플레이어의 레벨은?");
        Debug.Log(level);
        Debug.Log("플레이어의 몸무게는?");
        Debug.Log(weight);
        Debug.Log("플레이어는 최대 레벨인가?");
        Debug.Log(isMaxLevel);
    }
}
