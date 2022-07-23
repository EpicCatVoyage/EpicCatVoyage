using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public float spawnTime = 5.0f;
    public float spawnCount = 10;

    public GameObject prefab;
    public Transform MouseRoot;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.0f, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        Vector3 position = new Vector3();

        float screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = screenHalfHeight * Camera.main.aspect;
        float angle = 0.0f;

        for (int i = 0; i < spawnCount; ++i)
        {
            switch (i % 4)
            {
                // 좌측에서 생성
                case 0:
                    position.x = -screenHalfWidth;
                    position.y = Random.Range(-screenHalfHeight, screenHalfHeight);
                    angle = -90.0f;
                    break;

                // 우측에서 생성
                case 1:
                    position.x = screenHalfWidth;
                    position.y = Random.Range(-screenHalfHeight, screenHalfHeight);
                    angle = 90.0f;
                    break;

                // 상단에서 생성
                case 2:
                    position.x = Random.Range(-screenHalfWidth, screenHalfWidth);
                    position.y = screenHalfHeight;
                    angle = 180.0f;
                    break;

                // 하단에서 생성
                case 3:
                    position.x = Random.Range(-screenHalfWidth, screenHalfWidth);
                    position.y = -screenHalfHeight;
                    angle = 0.0f;
                    break;
            }

            GameObject go = Instantiate(prefab, MouseRoot);

            go.transform.position = position;
            go.transform.Rotate(0.0f, 0.0f, angle);
        }
    }
}
