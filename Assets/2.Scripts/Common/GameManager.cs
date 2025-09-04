using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject confirmPanel;

    // Main Scene에서 선택한 게임 타입 변수
    private Constants.GameType _gameType;

    // Panel을 띄우기 위한 Canvas 정보
    private Canvas _canvas;

    // Game Logic
    private GameLogic _gameLogic;





    // Main 에서 Game Scene 으로 전환시 호출될 메서드
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    // Game Scene 에서 Main 으로 전환시 호출될 메서드
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
            // Block 초기화
            var blockController = FindFirstObjectByType<BlockController>();
            blockController.InitBlocks();

            //GameLogic 생성
            if (_gameLogic != null)
            {
                // TODO : 기존 게임 로직을 소멸
            }
            
            _gameLogic = new GameLogic(blockController, _gameType);
            
        }
    }
}
