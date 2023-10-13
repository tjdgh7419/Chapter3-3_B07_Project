using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneBase : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(Close);
    }

    protected virtual void Close()
    {
        gameObject.SetActive(false);
        UIManager.Instance.OpenUI<ButtonsPanel>();
    }
}
