using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] protected Button closeButton;

    protected virtual void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }

    protected virtual void Close()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
        SoundManager.Instance.EffactMusic.Click1SoundPlay();
        UIManager.Instance.IsOnUI = false;
    }
}
