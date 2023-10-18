using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Image damgeImage;
    [SerializeField] private float flashSpeed = 1f;

    private Coroutine coroutine;

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        gameObject.SetActive(true);
        damgeImage.gameObject.SetActive(true);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 1f;
        float a = startAlpha;

        SoundManager.Instance.EffactMusic.DamageSoundPlay();

        while (a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            damgeImage.color = new Color(1.0f, 0.0f, 0.0f, a);
            yield return null;
        }

        gameObject.SetActive(false);
        damgeImage.gameObject.SetActive(false);
        yield break;
    }
}
