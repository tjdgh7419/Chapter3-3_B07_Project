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

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);     //�񵿱�ȭ�� ������ �ҷ�����
        op.allowSceneActivation = false;            //�ҷ� ���� �� �Ⱥ��̰�

        float timer = 0f;

        while (!op.isDone)      //�ҷ����� ���� ���� �ҷ����� �ʾҴٸ� �ݺ�
        {
            yield return null;

            timer += Time.deltaTime;
			
			if (op.progress < 0.9f)      //�ҷ��� ������ 90% �Ʒ����
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                progressText.text = $"�ε���...{(int)(progressBar.fillAmount * 100)}%";
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else if (timer > 1)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1, timer);
                progressText.text = $"�ε���...{(int)(progressBar.fillAmount * 100)}%";

                if (progressBar.fillAmount == 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            else
            {
                progressBar.fillAmount = 0.9f;
                progressText.text = "�ε���...90%";
            }
        }
    }
}
