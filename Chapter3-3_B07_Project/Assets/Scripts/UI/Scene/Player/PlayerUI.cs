using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image ReducedHPBar;
    [SerializeField] private Image CurrentHPBar;

    [Header("MP")]
    [SerializeField] private Image ReducedMPBar;
    [SerializeField] private Image CurrentMPBar;

    [Range(0f, 1f)] private float hp = 1;
    [Range(0f, 1f)] private float mp = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float _damage, string type = null)
    {
        _damage /= 100;
        if (type == "MP")
        {
            if (mp < _damage)
            {
                _damage = mp;
            }
            mp -= _damage;
        }
        else
        {
            if (hp < _damage)
            {
                _damage = hp;
            }
            hp -= _damage;
        }
        StartCoroutine(TakeDamage(type));
    }

    public void Healing(float _heal, string type = null)
    {
        _heal /= 100;
        if (type == "MP")
        {
            if (mp + _heal >= 1)
            {
                _heal = 1 - mp;
            }
            mp += _heal;
        }
        else
        {
            if (hp + _heal >= 1)
            {
                _heal = 1 - hp;
            }
            hp += _heal;
        }
        StartCoroutine(Healing(type));
    }

    IEnumerator TakeDamage(string type = null)
    {
        yield return null;

        if (type == "MP")
        {
            CurrentMPBar.fillAmount = mp;

            while (true)
            {
                yield return null;
                if (ReducedMPBar.fillAmount <= CurrentMPBar.fillAmount + 0.0001f)
                {
                    ReducedMPBar.fillAmount = CurrentMPBar.fillAmount;
                    yield break;
                }
                ReducedMPBar.fillAmount = Mathf.Lerp(ReducedMPBar.fillAmount, mp, Time.deltaTime * 3);
            }
        }
        else
        {
            CurrentHPBar.fillAmount = hp;

            while (true)
            {
                yield return null;

                if (ReducedHPBar.fillAmount <= CurrentHPBar.fillAmount + 0.0001f)
                {
                    ReducedHPBar.fillAmount = CurrentHPBar.fillAmount;
                    yield break;
                }
                ReducedHPBar.fillAmount = Mathf.Lerp(ReducedHPBar.fillAmount, hp, Time.deltaTime * 3);
            }
        }
    }

    IEnumerator Healing(string type = null)
    {
        yield return null;

        if (type == "MP")
        {
            ReducedMPBar.fillAmount = mp;

            while (true)
            {
                yield return null;
                if (ReducedMPBar.fillAmount <= CurrentMPBar.fillAmount + 0.0001f)
                {
                    CurrentMPBar.fillAmount = ReducedMPBar.fillAmount;
                    yield break;
                }
                CurrentMPBar.fillAmount = Mathf.Lerp(CurrentMPBar.fillAmount, mp, Time.deltaTime * 3);
            }
        }
        else
        {
            ReducedHPBar.fillAmount = hp;

            while (true)
            {
                yield return null;
                if (ReducedHPBar.fillAmount <= CurrentHPBar.fillAmount + 0.0001f)
                {
                    CurrentHPBar.fillAmount = ReducedHPBar.fillAmount;
                    yield break;
                }
                CurrentHPBar.fillAmount = Mathf.Lerp(CurrentHPBar.fillAmount, hp, Time.deltaTime * 3);
            }
        }
    }
}
