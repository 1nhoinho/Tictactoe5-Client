using UnityEngine;
using static ConfirmPanelController;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackbutton()
    {
        GameManager.Instance.OpenConfirmpanel("���� �����ϱ�",
              () =>
            {
                GameManager.Instance.ChangeToMainScene();
            });
    }
}
