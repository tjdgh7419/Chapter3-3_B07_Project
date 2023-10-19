using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Dictionary<string, GameObject> _uiList = new Dictionary<string, GameObject>();
    private void Awake()
    {
        Instance = this;

        InitUIList();
    }

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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        var obj = _uiList[typeof(T).Name];
        obj.SetActive(true);
        return obj.GetComponent<T>();
    }

    public void MouseUnlock()
    {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void MouseLock()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
