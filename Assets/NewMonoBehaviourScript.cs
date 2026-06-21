using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

using Random = UnityEngine.Random;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isSurvive = true;
    void Start()
    {
        // 6. 반복문
        // 7. 함수
        // 8. 클래스
        Animatronics oldFreddy = new Animatronics();
        oldFreddy.name = "Freddy Fazbear";
        oldFreddy.route = new List<int> {8, 7, 3, 0};
        oldFreddy.level = 19;

        Debug.Log(oldFreddy.Play());

        Debug.Log($"{oldFreddy.name}의 AI 레벨은 {oldFreddy.level}입니다.");
        oldFreddy.LevelUp();
        Debug.Log($"{oldFreddy.name}의 AI 레벨은 {oldFreddy.level}입니다.");

        Debug.Log("퍼펫이 뮤직박스에서 완전히 빠져나왔습니다.");
        PuppetMove();
        StartCoroutine(PuppetJumpScare());
        
        GoldenFreddy goldenFreddy = new GoldenFreddy();
        Debug.Log(goldenFreddy.Shutdown());
    }

    void PuppetMove()
    {
        List<int> puppetRoute = Random.Range(0, 2) == 0 ? new List<int> {11, 10, 7, 3, 1} : new List<int> {11, 10, 7, 4, 2};
        // for (int i = 0; i < 5; i++)
        // {
        //     Debug.Log($"퍼펫이 캠{puppetRoute[i]}(으)로 이동했습니다.");
        // }
        // Debug.Log("퍼펫이 경비실(으)로 이동했습니다.");

        foreach(int location in puppetRoute)
        {
            Debug.Log($"퍼펫이 캠{location}(으)로 이동했습니다.");
        }
        Debug.Log("퍼펫이 경비실(으)로 이동했습니다.");
    }

    System.Collections.IEnumerator PuppetJumpScare()
    {
        while (isSurvive)
        {
            yield return new WaitForSeconds(1.0f);
            int randomValue = Random.Range(0, 10);
            if (randomValue == 2)
            {
                isSurvive = false;
                Debug.Log($"퍼펫의 점프 스케어로 게임 오버! randomValue : {randomValue}" );
                yield break;
            }
            else
            {
                Debug.Log($"퍼펫이 공격하지 않았습니다. randomValue : {randomValue}");
            }
        }
    }
}