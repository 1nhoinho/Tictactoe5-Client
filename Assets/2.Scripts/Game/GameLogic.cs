using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameLogic
{
    public BlockController blockController;  // Block을 처리할 객체

    private Constants.PlayerType[,] _board; // 보드의 상태 정보

    public BasePlayerState firstPlayerState;  // PlayerA
    public BasePlayerState secondPlayerState; // PlayerB


    public enum GameResult { None, Win, Lose, Draw }

    private BasePlayerState _currentPlayerState;  // 현재 턴의 플레이어

    public GameLogic(BlockController blockController, Constants.GameType gameType) 
    {
        this.blockController = blockController;

        // 보드의 상태 정보 초기화
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // Game Type 초기화
        switch (gameType)
        {
            case Constants.GameType.SinglePlay:
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                // 게임 시작
                SetState(firstPlayerState);
                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }    

    // 턴이 바뀔 때 기존 진행상태를 Exit 하고 이번 턴의 상태를 할당하고
    // 이번 턴의 상태에 Enter 호출
    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);

    }

    // _board 배열에 새로운 Marker 값을 할당
    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        if (_board[row, col] != Constants.PlayerType.None) return false;

        if (playerType == Constants.PlayerType.PlayerA )
        {
            _board[row, col] = playerType;
            blockController.PlaceMaker(Block.MarkerType.O, row, col);
            return true;
        }
        else if (playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMaker(Block.MarkerType.X, row, col);
            return true;
        }
        return false;
    }

    // Game Over 처리
    public void EndGame(GameResult gameResult)
    {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        //TODO : 유저에게 Game Over ㅍ시
        Debug.Log("Game Over");
    }


    // 게임의 결과 확인
    public GameResult CheckGameResult()
    {
        if (CheckGameWin(Constants.PlayerType.PlayerA, _board))
        {
            return GameResult.Win;
        }
        if (CheckGameWin(Constants.PlayerType.PlayerB, _board))
        { 
            return GameResult.Lose;
        }

        return GameResult.None;
    }

    // 비겼는지 확인
    public bool CheckGameDrow(Constants.PlayerType[,] board)
    {
        for (var row =0;  row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None) return false;
            }
        }
        return true;    
    }

    // 게임 승리 확인
    private bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        for (var row = 0; row < _board.GetLength(0); row++)
        {
            if (_board[row,0] == playerType &&
                _board[row, 1] == playerType &&
                _board[row, 2] == playerType)
            {
                return true;
            }
        }
        // Row 체크 후 일자면 TRue
        for (var col = 0; col < _board.GetLength(1); col++)
        {
            if (_board[0, col] == playerType &&
                _board[1, col] == playerType &&
                _board[2, col] == playerType)
            {
                return true;
            }
        }

        // 대각선 일자면 True
        if (_board[0, 0] == playerType &&
            _board[1, 1] == playerType &&
            _board[2, 2] == playerType)
        {
            return true;
        }

        if (_board[0, 2] == playerType &&
            _board[1, 1] == playerType &&
            _board[2, 0] == playerType)
            {
                return true;
            }
        return false;
        }
    }


