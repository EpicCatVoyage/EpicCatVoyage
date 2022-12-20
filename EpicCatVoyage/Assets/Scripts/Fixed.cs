using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : MonoBehaviour
{
    private void Start()
    {
        SetResolution(); // �ʱ⿡ ���� �ػ� ����
    }

    /* �ػ� �����ϴ� �Լ� */
    public void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        Screen.SetResolution(setWidth, setHeight, true);
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}