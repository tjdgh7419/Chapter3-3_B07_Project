using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Audio")]
    [Range(0f, 1f)] public static float MasterVolume = 1;
    [Range(0f, 1f)] public static float MusicVolume = 1;
    [Range(0f, 1f)] public static float EffactVolume = 1;

    [Header("Shadow")]
    public static bool Effact = true;
    public static bool Shadow = true;

    private Dictionary<string, GameObject> _uiList = new Dictionary<string, GameObject>();
    private void Awake()
    {
        Instance = this;

        InitUIList();
        /*
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        */
    }
    /*
    // ���ӽſ��� ü�°� ���� ��°� Ȯ���ϱ� ���� �ۼ�
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "UI_DEV_Scene")
        {
            var player = _uiList["DamageIndicator"].GetComponent<DamageIndicator>();
            if (Input.GetMouseButtonDown(0))
            {
                player.Flash();
                //player.TakeDamage(10f);
                //player.TakeDamage(10f, "MP");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                player.Healing(10f);
            }
            else if (Input.GetMouseButtonDown(2))
            {
                player.Healing(10f, "MP");
            }
        }
    }*/

    private void InitUIList()
    {
        int uiCount = transform.childCount;

        for (int i = 0; i < uiCount; i++)
        {
            Transform go = transform.GetChild(i);
            _uiList.Add(go.name, go.gameObject);
            go.gameObject.SetActive(false);
        }
    }

    public T OpenUI<T>()
    {
        var obj = _uiList[typeof(T).Name];
        obj.SetActive(true);
        return obj.GetComponent<T>();
    }

    public void SetAudioSetting(float master, float music, float efffact)
    {
        MasterVolume = master;
        MusicVolume = music;
        EffactVolume = efffact;
    }

    public void SetGraphicSetting(bool _effact, bool _shadow)
    {
        Effact = _effact;
        Shadow = _shadow;
    }

    //0�� �����ͺ��� 1�� �������� 2�� ����Ʈ����
    public float[] GetAudioSetting()
    {
        return new float[] {MasterVolume, MusicVolume, EffactVolume};
    }

    //0�� ����Ʈ 1�� ������
    public bool[] GetGraphicSetting()
    {
        return new bool[] { Effact, Shadow};
    }
}
