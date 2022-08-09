using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string NextScene;
  
    public static void LoadScene(string SceneName)
    {
        NextScene = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(NextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        while(!op.isDone)
        {
            Debug.Log("·Îµù Áß");
            yield return null;

            if(op.progress > 0.9f)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if(timer >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }
}
