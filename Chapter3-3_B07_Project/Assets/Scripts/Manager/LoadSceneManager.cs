using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI progressText;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string _nextScene)
    {
        Time.timeScale = 1.0f;
        nextScene = _nextScene;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);     //비동기화로 다음씬 불러오기
        op.allowSceneActivation = false;            //불러 오는 씬 안보이게

        float timer = 0f;

        while (!op.isDone)      //불러오는 씬이 전부 불려오지 않았다면 반복
        {
            yield return null;

            timer += Time.deltaTime;
			
			if (op.progress < 0.9f)      //불러온 정도가 90% 아래라면
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                progressText.text = $"로딩중...{(int)(progressBar.fillAmount * 100)}%";
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else if (timer > 1)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1, timer);
                progressText.text = $"로딩중...{(int)(progressBar.fillAmount * 100)}%";

                if (progressBar.fillAmount == 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            else
            {
                progressBar.fillAmount = 0.9f;
                progressText.text = "로딩중...90%";
            }
        }
    }
}
