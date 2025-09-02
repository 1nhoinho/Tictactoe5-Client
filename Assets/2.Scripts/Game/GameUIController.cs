using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackbutton()
    {
        GameManager.Instance.OpenConfrimpanel("게임 종료하기");
    }
}
