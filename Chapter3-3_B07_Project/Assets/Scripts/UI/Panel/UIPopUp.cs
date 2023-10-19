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
    private Action UnConfirm;

    protected virtual void Awake()
    {
        base.Awake();
        cheackButton.onClick.AddListener(Confirm);
        closeButton.onClick.AddListener(NotConfirm);
    }

    public void SetAction(string _headingText, string _explanationText, Action onConfirm = null, Action unConfirm = null)
    {
        headingText.text = _headingText;
        explanationText.text = _explanationText;

        OnConfirm = onConfirm;
        UnConfirm = unConfirm;
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

    void NotConfirm()
    {
        OnCheackButton();
        Close();
        UnConfirm?.Invoke();
        UnConfirm = null;

    }

    public void OffCheackButton()
    {
        if (cheackButton.gameObject.active)
        {
            cheackButton.gameObject.SetActive(false);
        }
    }
    
    public void OnCheackButton()
    {
        if (!cheackButton.gameObject.active)
        {
            cheackButton.gameObject.SetActive(true);
        }
    }
}
