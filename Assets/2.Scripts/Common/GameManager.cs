using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject confirmPanel;

    // Main Scene���� ������ ���� Ÿ�� ����
    private Constants.GameType _gameType;

    // Panel�� ���� ���� Canvas ����
    private Canvas _canvas;

    // Main ���� Game Scene ���� ��ȯ�� ȣ��� �޼���
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    // Game Scene ���� Main ���� ��ȯ�� ȣ��� �޼���
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }


    public void OpenConfrimpanel(string message)
    {
        if ( _canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message);
        }
    }


    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
       _canvas = FindFirstObjectByType<Canvas>();
    }
}
