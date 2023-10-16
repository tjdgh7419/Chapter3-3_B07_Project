using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUp : GameUIBase
{
    [SerializeField] private Button cheackButton;

    [Header ("Info")]
    [SerializeField] private TextMeshProUGUI headingText;
    [SerializeField] private TextMeshProUGUI explanationText;

    private Action OnConfirm;
    private Action OnQuest;

    protected virtual void Awake()
    {
        base.Awake();
        cheackButton.onClick.AddListener(Confirm);
    }

    public void SetAction(string _headingText, string _explanationText, Action onConfirm = null, Action onQuest = null)
    {
        headingText.text = _headingText;
        explanationText.text = _explanationText;

        OnConfirm = onConfirm;
        OnQuest = onQuest;
    }

    void Confirm()
    {
        /*if (OnConfirm != null)
        {
            OnConfirm();
            OnConfirm = null;
        }*/
        OnQuest?.Invoke();
        OnQuest = null;
        OnConfirm?.Invoke();
        OnConfirm = null;

        Close();
    }
}
