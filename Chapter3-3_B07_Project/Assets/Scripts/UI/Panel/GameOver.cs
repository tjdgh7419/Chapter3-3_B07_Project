using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image gameoverBG;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button buttton;
    [SerializeField] private TextMeshProUGUI butttonText;

    private void Start()
    {
        buttton.onClick.AddListener(() => LoadSceneManager.LoadScene("StartScene"));
        StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float a = 0;

        SoundManager.Instance.EffactMusic.DamageSoundPlay();

        while (a < 1.0f)
        {
            a +=  Time.deltaTime / 2;
            gameoverBG.color = new Color(0.0f, 0.0f, 0.0f, a);
            text.color = new Color(1.0f, 0.0f, 0.0f, a);
            buttton.gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, a);
            butttonText.color = new Color(1.0f, 1.0f, 1.0f, a);
            yield return null;
        }

        yield break;
    }
}
