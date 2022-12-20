using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : MonoBehaviour
{
    private void Start()
    {
        SetResolution(); // 초기에 게임 해상도 고정
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution()
    {
        int setWidth = 1920; // 사용자 설정 너비
        int setHeight = 1080; // 사용자 설정 높이

        Screen.SetResolution(setWidth, setHeight, true);
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}