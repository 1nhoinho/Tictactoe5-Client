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

    // Game Logic
    private GameLogic _gameLogic;





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


    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if ( _canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClicked);
        }
    }


    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
       _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block �ʱ�ȭ
            var blockController = FindFirstObjectByType<BlockController>();
            blockController.InitBlocks();

            //GameLogic ����
            if (_gameLogic != null)
            {
                // TODO : ���� ���� ������ �Ҹ�
            }
            
            _gameLogic = new GameLogic(blockController, _gameType);
            
        }
    }
}
