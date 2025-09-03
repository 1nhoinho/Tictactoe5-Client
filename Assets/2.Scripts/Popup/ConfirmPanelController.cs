using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    // Confirm 버튼 클릭시 호출된 Delegate
    public delegate void OnConfirmButtonClicked();
    private OnConfirmButtonClicked _onConfirmButtonClicked;


    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;
        base.Show();
    }

    // 확인 버튼 
    public void OnClickConfirmButton()
    {
        Hide(() =>
        {
            _onConfirmButtonClicked?.Invoke();
        });
    }

    // X 버튼
    public void OnClickCloseButton()
    {
        Hide();
    }

}
