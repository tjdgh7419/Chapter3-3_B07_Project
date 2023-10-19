using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timerText;
    public int[,] monstersCount =
        { {10, 0, 0 }, { 0, 10, 0 }, { 5, 5, 0 }, { 4, 4, 4 }, { 0, 5, 5 },
        { 10, 0, 5 }, { 5, 5, 5 }, { 20, 0, 0 }, { 0, 20, 0 }, { 0, 0, 15 }
        , { 10, 10, 0 }, { 10, 10, 5 }, { 20, 20, 0 }, {20, 20, 15 }};
    public int currentRound = 0;
    private float roundDuration = 30.0f; // 2분
    private float breakDuration = 10.0f; // 0.5분
    private int totalRounds = 15;
    private bool isBreakTime = true;

    [HideInInspector]public GameObject monsterObject;

    private MonsterManager monsterManager;
    private void Start()
    {
        monsterManager = GameManager.Instance.monsterManager;
        UpdateRoundText();
        StartCoroutine(StartRound());
    }

    private void UpdateRoundText()
    {
        roundText.text = "Round: " + currentRound + "/" + totalRounds;
    }

    private IEnumerator StartRound()
    {
        roundDuration = 30f;
        breakDuration = 10f;
        if (currentRound > totalRounds)
        {
            // 게임 종료
            Debug.Log("게임 종료");
            yield break;
        }

        if (isBreakTime)
        {
            SoundManager.Instance.BackMusic.WaveOff();
            yield return new WaitForSeconds(breakDuration);
            isBreakTime = false;
        }
        else
        {
            SpawnWithRound();
            SoundManager.Instance.BackMusic.WaveOn();
            yield return new WaitForSeconds(roundDuration);
            currentRound++;
            isBreakTime = true;
        }

        UpdateRoundText();
        StartCoroutine(StartRound());
        yield break;
    }
    public void SpawnWithRound()
    {
        if (currentRound % 5 != 0)
        {
            if (monstersCount[currentRound - 1, 0] > 0)
            {
                StartCoroutine(SummonMonster(0));
            }
            if (monstersCount[currentRound - 1, 1] > 0)
            {
                StartCoroutine(SummonMonster(1));
            }
            if (monstersCount[currentRound - 1, 2] > 0)
            {
                StartCoroutine(SummonMonster(2));
            }
        }
        else
        {
            GameManager.Instance.monsterManager.CreateBossMob();
        }
    }
    IEnumerator SummonMonster(int type)
    {
        for (int i = 0; i < monstersCount[currentRound - 1, type]; i++)
        {
            monsterObject = monsterManager.SpawnFromPool(type);
            monsterObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        
    }
    private void Update()
    {
        // 타이머 표시
        if (isBreakTime)
        {
            breakDuration -= Time.deltaTime;
            timerText.text = "Break Time: 00 : " + (int)breakDuration;
        }
        else
        {
            roundDuration -= Time.deltaTime;
            timerText.text = $"{(int)roundDuration / 60} : {(int)roundDuration % 60}";
        }
    }
}
