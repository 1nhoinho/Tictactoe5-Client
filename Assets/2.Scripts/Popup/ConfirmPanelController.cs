using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    // Confirm ��ư Ŭ���� ȣ��� Delegate
    public delegate void OnConfirmButtonClicked();
    private OnConfirmButtonClicked _onConfirmButtonClicked;


    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;
        base.Show();
    }

    // Ȯ�� ��ư 
    public void OnClickConfirmButton()
    {
        Hide(() =>
        {
            _onConfirmButtonClicked?.Invoke();
        });
    }

    // X ��ư
    public void OnClickCloseButton()
    {
        Hide();
    }

}
