using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("PlayerCondition")]
    [SerializeField] private PlayerConditionManager PCM;

    [Header("HP")]
    [SerializeField] private Image ReducedHPBar;
    [SerializeField] private Image CurrentHPBar;
    [SerializeField] private TextMeshProUGUI HPText;

    [Header("MP")]
    [SerializeField] private Image ReducedMPBar;
    [SerializeField] private Image CurrentMPBar;
    [SerializeField] private TextMeshProUGUI MPText;

    private float maxHp;
    private float maxMp;
    private float currentHp;
    private float currentMp;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = PCM.hp.maxValue;
        maxMp = PCM.mp.maxValue;
        currentHp = PCM.hp.curValue;
        currentMp = PCM.mp.curValue;

        UpdateBar();
    }

    private void UpdateBar()
    {
        HPText.text = $"{currentHp}/{maxHp}";
        MPText.text = $"{currentMp}/{maxMp}";
    }

    public void TakeDamage(float _damage, string type = null)
    {
        if (type == "MP")
        {
            if (currentMp < _damage)
            {
                _damage = currentMp;
            }
            currentMp -= _damage;
        }
        else
        {
            if (currentHp <= _damage)
            {
                _damage = currentHp;
                UIManager.Instance.OpenUI<GameOver>();
            }
            currentHp -= _damage;
        }
        StartCoroutine(TakeDamage(type));
    }

    public void Healing(float _heal, string type = null)
    {
        if (type == "MP")
        {
            if (currentMp + _heal >= maxMp)
            {
                _heal = maxMp - currentMp;
            }
            currentMp += _heal;
        }
        else
        {
            if (currentHp + _heal >= maxHp)
            {
                _heal = maxHp - currentHp;
            }
            currentHp += _heal;
        }
        StartCoroutine(Healing(type));
    }

    IEnumerator TakeDamage(string type = null)
    {
        yield return null;
        UpdateBar();

        if (type == "MP")
        {
            CurrentMPBar.fillAmount = currentMp / maxMp;

            while (true)
            {
                yield return null;
                if (ReducedMPBar.fillAmount <= CurrentMPBar.fillAmount + 0.0001f)
                {
                    ReducedMPBar.fillAmount = CurrentMPBar.fillAmount;
                    yield break;
                }
                ReducedMPBar.fillAmount = Mathf.Lerp(ReducedMPBar.fillAmount, currentMp / maxMp, Time.deltaTime * 3);
            }
        }
        else
        {
            CurrentHPBar.fillAmount = currentHp / maxHp;

            while (true)
            {
                yield return null;

                if (ReducedHPBar.fillAmount <= CurrentHPBar.fillAmount + 0.0001f)
                {
                    ReducedHPBar.fillAmount = CurrentHPBar.fillAmount;
                    yield break;
                }
                ReducedHPBar.fillAmount = Mathf.Lerp(ReducedHPBar.fillAmount, currentHp / maxHp, Time.deltaTime * 3);
            }
        }
    }

    IEnumerator Healing(string type = null)
    {
        yield return null;
        UpdateBar();
        
        if (type == "MP")
        {
            ReducedMPBar.fillAmount = currentMp / maxMp;

            while (true)
            {
                yield return null;
                if (ReducedMPBar.fillAmount <= CurrentMPBar.fillAmount + 0.0001f)
                {
                    CurrentMPBar.fillAmount = ReducedMPBar.fillAmount;
                    yield break;
                }
                CurrentMPBar.fillAmount = Mathf.Lerp(CurrentMPBar.fillAmount, currentMp / maxMp, Time.deltaTime * 3);
            }
        }
        else
        {
            ReducedHPBar.fillAmount = currentHp / maxHp;

            while (true)
            {
                yield return null;
                if (ReducedHPBar.fillAmount <= CurrentHPBar.fillAmount + 0.0001f)
                {
                    CurrentHPBar.fillAmount = ReducedHPBar.fillAmount;
                    yield break;
                }
                CurrentHPBar.fillAmount = Mathf.Lerp(CurrentHPBar.fillAmount, currentHp / maxHp, Time.deltaTime * 3);
            }
        }
    }
}
