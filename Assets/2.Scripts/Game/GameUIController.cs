using UnityEngine;
using static ConfirmPanelController;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackbutton()
    {
        GameManager.Instance.OpenConfirmpanel("게임 종료하기",
              () =>
            {
                GameManager.Instance.ChangeToMainScene();
            });
    }
}
