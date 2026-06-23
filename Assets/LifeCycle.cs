using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LifeCycle : MonoBehaviour
{
    // // Input Manager : Old
    // void Update()
    // {
    //     if (Input.anyKeyDown) // 최초 1회 실행
    //     {
    //         Debug.Log("플레이어가 아무 키를 눌렀습니다.");
    //     }
    //     if (Input.anyKey) // 누르고 있는 프레임 동안 무제한 실행
    //     {
    //         Debug.Log("플레이어가 아무 키를 누르고 있습니다.");
    //     }

    //     if (Input.GetKeyDown(KeyCode.Return))
    //     {
    //         Debug.Log("플레이어가 Enter를 눌렀습니다.");
    //     }
    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         Debug.Log("앞쪽 이동 중");
    //     }
    //     if (Input.GetKeyUp(KeyCode.W))
    //     {
    //         Debug.Log("앞쪽 이동 정지");
    //     }

    //     if (Input.GetMouseButtonDown(0)) // 0은 왼쪽, 1은 오른쪽
    //     {
    //         Debug.Log("레일건 목표 설정 완료.");
    //     }
    //     if (Input.GetMouseButton(0))
    //     {
    //         Debug.Log("레일건 충전 중...");
    //     }
    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         Debug.Log("레일건 발사!");
    //     }

    //     if (Input.GetButton("Jump")) // 문자열은 버튼의 이름
    //     {
    //         Debug.Log("Ready...");
    //     }
    //     if (Input.GetButtonUp("Jump"))
    //     {
    //         Debug.Log("Go!!");
    //     }
    //     if (Input.GetButtonDown("Cancel"))
    //     {
    //         Debug.Log("FNaF였으면 바로 게임이 꺼졌을 것");
    //     }
    // // }

    // Input System : New
    void Update()
    {
        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) || (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) || (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame) || (Mouse.current != null && Mouse.current.middleButton.wasPressedThisFrame) || (Mouse.current != null && Mouse.current.forwardButton.wasPressedThisFrame) || (Mouse.current != null && Mouse.current.backButton.wasPressedThisFrame)) 
        {
            Debug.Log("플레이어가 아무 키를 눌렀습니다.");
        }
        if ((Keyboard.current != null && Keyboard.current.anyKey.isPressed) || (Mouse.current != null && Mouse.current.leftButton.isPressed) || (Mouse.current != null && Mouse.current.rightButton.isPressed) || (Mouse.current != null && Mouse.current.middleButton.isPressed) || (Mouse.current != null && Mouse.current.forwardButton.isPressed) || (Mouse.current != null && Mouse.current.backButton.isPressed)) 
        {
            Debug.Log("플레이어가 아무 키를 누르고 있습니다.");
        }

        if (Keyboard.current != null && Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("무한한 공간 저 너머로!");
        }
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("나는 게 아냐. 멋지게 떨어지는 거지.");
        }
    }
}