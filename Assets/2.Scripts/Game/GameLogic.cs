using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameLogic
{
    public BlockController blockController;  // Block�� ó���� ��ü

    private Constants.PlayerType[,] _board; // ������ ���� ����

    public BasePlayerState firstPlayerState;  // PlayerA
    public BasePlayerState secondPlayerState; // PlayerB


    public enum GameResult { None, Win, Lose, Draw }

    private BasePlayerState _currentPlayerState;  // ���� ���� �÷��̾�

    public GameLogic(BlockController blockController, Constants.GameType gameType) 
    {
        this.blockController = blockController;

        // ������ ���� ���� �ʱ�ȭ
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // Game Type �ʱ�ȭ
        switch (gameType)
        {
            case Constants.GameType.SinglePlay:
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                // ���� ����
                SetState(firstPlayerState);
                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }    

    // ���� �ٲ� �� ���� ������¸� Exit �ϰ� �̹� ���� ���¸� �Ҵ��ϰ�
    // �̹� ���� ���¿� Enter ȣ��
    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);

    }

    // _board �迭�� ���ο� Marker ���� �Ҵ�
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

    // Game Over ó��
    public void EndGame(GameResult gameResult)
    {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        //TODO : �������� Game Over ����
        Debug.Log("Game Over");
    }


    // ������ ��� Ȯ��
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

    // ������ Ȯ��
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

    // ���� �¸� Ȯ��
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
        // Row üũ �� ���ڸ� TRue
        for (var col = 0; col < _board.GetLength(1); col++)
        {
            if (_board[0, col] == playerType &&
                _board[1, col] == playerType &&
                _board[2, col] == playerType)
            {
                return true;
            }
        }

        // �밢�� ���ڸ� True
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


