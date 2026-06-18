using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello Unity!");

        // // 1. 변수
        // int level = 5;
        // float weight = 91.8f; // 최적화를 위해 double보다 float를 자주 사용, 이때 실수만 입력하면 double 자료형으로 받아들이기에 f를 붙여 해당 값이 float 자료형임을 명시
        // string playerName = "장훈진";
        // bool isMaxLevel = false;

        // Debug.Log("플레이어의 이름은?");
        // Debug.Log(playerName);
        // Debug.Log("플레이어의 레벨은?");
        // Debug.Log(level);
        // Debug.Log("플레이어의 몸무게는?");
        // Debug.Log(weight);
        // Debug.Log("플레이어는 최대 레벨인가?");
        // Debug.Log(isMaxLevel);


        // // 2. 그룹형 변수
        // string[] monster = {"전수빈", "고종현"};
        // string boss = "권민준";
        // Debug.Log(monster[0] + "은(는) 일반 몬스터입니다.");
        // Debug.Log($"{monster[1]}은(는) 일반 몬스터입니다.");
        // Debug.Log(boss + "은(는) 보스 몬스터입니다.");
        // // Debug.Log(boss[0] + boss[1] + "" + boss[2]); // 문자열로 인식하기 위해서는 순서도 중요

        // // int[] monsterLevel = {1, 4};
        // int[] monsterLevel = new int[2];
        // monsterLevel[0] = 1;
        // monsterLevel[1] = 4;

        // for (int i = 0; i < monster.Length; i++)
        // {
        //     Debug.Log($"{monster[i]}은(는) {monsterLevel[i]} 레벨입니다.");
        // }

        // List<String> effect = new List<string>();
        // effect.Add("맹독");
        // effect.Add("화상");
        // effect.Add("마비");
        // effect.Add("얼음");
        // effect.Add("잠듦");

        // Debug.Log($"무작위 상태이상 선택: {effect[UnityEngine.Random.Range(0, 5)]}");

        // effect.Remove("잠듦"); // 값으로 삭제
        // effect.RemoveAt(0); // 인덱스로 삭제


        // // 3. 연산자
        // int exp = 0;
        // int level = 1;
        // float strength = 3.1f;

        // exp += 1200; // 사냥으로 경험치를 획득
        // exp += 800; // 사냥으로 경험치를 획득

        // while (exp >= (level * 100))
        // {
        //     exp -= (level * 100);
        //     level++;
        //     strength = level * 3.1f;
        //     Debug.Log($"Level Up! {level} 레벨! | EXP : {exp} | STR : {strength:F1}");
        // }

        // int maxLevel = 100;
        // bool isMaxLevel = level == maxLevel;
        // Debug.Log($"플레이어는 최고 레벨입니까? {isMaxLevel}");

        // bool isFinishTutorial = level > 5;
        // Debug.Log($"플레이어는 튜토리얼을 완료했습니까? {isFinishTutorial}");

        // int health = 30;
        // int mana = 25;
        // bool isBadCondition = health <= 50 && mana <= 20;
        // // bool isBadCondition = health <= 50 || mana <= 20;
        // Debug.Log($"플레이어의 상태가 나쁩니까? {isBadCondition}");

        // string condition = isBadCondition ? "좋음" : "나쁨";
        // Debug.Log($"플레이어의 상태 : {isBadCondition}");


        // 4. 키워드
        // int float double string new List


        // 5. 조건문
        List<string> item = new List<string>();
        item.Add("자동 치유 물약");
        item.Add("자동 마나 물약");

        int health = 50;
        int mana = 50;
        health -= 40;
        mana -= 40;
        bool isBadCondition = health <= 30 || mana <= 30;
        string condition = isBadCondition ? "나쁨" : "좋음";

        if (condition == "나쁨")
        {
            Debug.Log("플레이어의 상태가 나쁩니다! R을 눌러 아이템을 사용하세요!");
            if (health <= 30 && item.Contains("자동 치유 물약"))
            {
                item.Remove("자동 치유 물약");
                health += 20;
                Debug.Log("자동 치유 물약이 사용됐습니다. 체력이 30 회복됐습니다.");
            }
            if (mana <= 30 && item.Contains("자동 마나 물약"))
            {
                item.Remove("자동 마나 물약");
                mana += 10;
                Debug.Log("자동 마나 물약이 사용됐습니다. 마나가 10 회복됐습니다.");
            }
        }
        else
        {
            Debug.Log("플레이어의 상태가 좋습니다.");
        }

        List<string> monster = new List<string> { "프레디", "보니", "치카", "폭시", "골든 프레디" };
        switch (monster[4])
        {
            case "프레디" :
                Debug.Log("우측을 주의하세요! CCTV로 그를 찾아 움직임을 저지할 수 있습니다.");
                break;
            case "보니" :
                Debug.Log("좌측을 주의하세요! 불빛을 비춰 안전한 상태인지 확인해야 합니다.");
                break;
            case "치카" :
                Debug.Log("우측을 주의하세요! 불빛을 비춰 안전한 상태인지 확인해야 합니다.");
                break;
            case "폭시" :
                Debug.Log("좌측을 주의하세요! 폭시가 Pirate Cove에서 완전히 벗어났다면 좌측 문을 빠르게 닫아 방어할 수 있습니다.");
                break;
            default :
                Debug.Log("IT'S ME");
                Debug.Log("IT'S ME");
                Debug.Log("IT'S ME");
                Debug.Log("IT'S ME");
                break;
        }
    }
}
