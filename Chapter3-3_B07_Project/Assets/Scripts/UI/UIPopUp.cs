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

    private void Start()
    {
        cheackButton.onClick.AddListener(Confirm);
    }

    public void SetAction(string _headingText, string _explanationText, Action onConfirm = null)
    {
        headingText.text = _headingText;
        explanationText.text = _explanationText;

        OnConfirm = onConfirm;
    }

    void Confirm()
    {
        /*if (OnConfirm != null)
        {
            OnConfirm();
            OnConfirm = null;
        }*/
        OnConfirm?.Invoke();
        OnConfirm = null;

        Close();
    }
}
