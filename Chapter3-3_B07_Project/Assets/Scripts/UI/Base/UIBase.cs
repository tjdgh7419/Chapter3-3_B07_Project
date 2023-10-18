using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button closeButton;

    protected virtual void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }

    protected virtual void Close()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.EffactMusic.Click1SoundPlay();
    }
}
