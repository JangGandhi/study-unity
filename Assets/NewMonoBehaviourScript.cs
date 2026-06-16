using System;
using System.Collections.Generic;
using NUnit.Framework;
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
    }
}
