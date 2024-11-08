using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
             Create("white_king", 4, 0),
            Create("white_queen", 3, 0),
            Create("white_rook", 0, 0), Create("white_rook", 7, 0),
            Create("white_knight", 6, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_bishop", 5, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1),
            Create("white_pawn", 2, 1), Create("white_pawn", 3, 1),
            Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1)
        };

        playerBlack = new GameObject[]
        {
             Create("black_king", 4, 7),
            Create("black_queen", 3, 7),
            Create("black_rook", 0, 7), Create("black_rook", 7, 7),
            Create("black_knight", 6, 7), Create("black_knight", 1, 7),
            Create("black_bishop", 2, 7), Create("black_bishop", 5, 7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6),
            Create("black_pawn", 2, 6), Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
        };

        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessmen cm = obj.GetComponent<Chessmen>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessmen cm = obj.GetComponent<Chessmen>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }
    public void SetTemporaryPosition(GameObject obj, int x, int y)
    {
        Chessmen cm = obj.GetComponent<Chessmen>();
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        positions[x, y] = obj;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    /*public void MakeMove(GameObject piece, int x, int y)
    {
        Chessmen chessman = piece.GetComponent<Chessmen>();

        SetPositionEmpty(chessman.GetXBoard(), chessman.GetYBoard());

        SetTemporaryPosition(piece, x, y);

        EndTurn();
    }

    public void EndTurn()
    {


        NextTurn();
    }*/

    public void NextTurn()
    {
        if (CheckForCheck())
        {
            GameObject.FindGameObjectWithTag("CheckText").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("CheckText").GetComponent<Text>().text = currentPlayer + " is in check!";
            Debug.Log(currentPlayer + " is in check!");
            if (CheckForCheckMate())
            {

                Winner(currentPlayer == "white" ? "black" : "white");
                return;
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("CheckText").GetComponent<Text>().enabled = false;
        }

        currentPlayer = currentPlayer == "white" ? "black" : "white";

    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }

    public GameObject FindKing(string player)
    {
        foreach (GameObject piece in (player == "white" ? playerWhite : playerBlack))
        {
            if (piece != null && piece.name.Contains("king"))
            {
                return piece;
            }
        }
        return null;
    }

    public bool CheckForCheck( )
    {
        GameObject king = FindKing(currentPlayer);
        if (king == null) return false;

        Chessmen kingChessman = king.GetComponent<Chessmen>();
        int kingX = kingChessman.GetXBoard();
        int kingY = kingChessman.GetYBoard();

        string enemyPlayer = currentPlayer == "white" ? "black" : "white";

        GameObject[] enemyPieces = enemyPlayer == "white" ? playerWhite : playerBlack;
        foreach (GameObject piece in enemyPieces)
        {
            if (piece != null)
            {
                Chessmen chessman = piece.GetComponent<Chessmen>();

                if (PieceCanAttackKing(chessman, kingX, kingY))
                {
                    return true;
                }
            }
        }
        return false;

    }

    public bool PieceCanAttackKing(Chessmen piece, int kingX, int kingY)
    {
        switch (piece.name)
        {
            case "black_queen":
            case "white_queen":
                return LineMoveCheck(piece, kingX, kingY, 1, 0) || LineMoveCheck(piece, kingX, kingY, 0, 1)
                    || LineMoveCheck(piece, kingX, kingY, 1, 1) || LineMoveCheck(piece, kingX, kingY, -1, 0)
                    || LineMoveCheck(piece, kingX, kingY, 0, -1) || LineMoveCheck(piece, kingX, kingY, -1, -1)
                    || LineMoveCheck(piece, kingX, kingY, -1, 1) || LineMoveCheck(piece, kingX, kingY, 1, -1);

            case "black_rook":
            case "white_rook":
                return LineMoveCheck(piece, kingX, kingY, 1, 0) || LineMoveCheck(piece, kingX, kingY, 0, 1)
                    || LineMoveCheck(piece, kingX, kingY, -1, 0) || LineMoveCheck(piece, kingX, kingY, 0, -1);

            case "black_bishop":
            case "white_bishop":
                return LineMoveCheck(piece, kingX, kingY, 1, 1) || LineMoveCheck(piece, kingX, kingY, -1, -1)
                    || LineMoveCheck(piece, kingX, kingY, -1, 1) || LineMoveCheck(piece, kingX, kingY, 1, -1);

            case "black_knight":
            case "white_knight":
                return KnightMoveCheck(piece, kingX, kingY);

            case "black_king":
            case "white_king":
                return SurroundMoveCheck(piece, kingX, kingY);

            case "black_pawn":
                return PawnMoveCheck(piece, kingX, kingY, -1);

            case "white_pawn":
                return PawnMoveCheck(piece, kingX, kingY, 1);
        }
        return false;
    }

    public bool LineMoveCheck(Chessmen piece, int targetX, int targetY, int xIncrement, int yIncrement)
    {
        int x = piece.GetXBoard() + xIncrement;
        int y = piece.GetYBoard() + yIncrement;

        while (PositionOnBoard(x,y) && GetPosition(x,y) == null)
        {
            if (x == targetX && y == targetY)
            {
                return true;
            }
            x += xIncrement;
            y += yIncrement;
        }

        if (PositionOnBoard(x,y) && x == targetX && y == targetY)
        {
            return true;
        }
        return false;
    }

    public bool KnightMoveCheck(Chessmen piece, int targetX, int targetY)
    {
        int x = piece.GetXBoard();
        int y = piece.GetYBoard();

        int[,] knightMoves = { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 } };

        for (int i = 0; i < knightMoves.GetLength(0); i++)
        {
            int newX = x + knightMoves[i, 0];
            int newY = y + knightMoves[i, 1];
            if (newX == targetX && newY == targetY && PositionOnBoard(newX, newY))
            {
                return true;
            }
        }

        return false;
    }

    public bool SurroundMoveCheck(Chessmen piece, int targetX, int targetY)
    {
        int x = piece.GetXBoard();
        int y = piece.GetYBoard();

        int[,] kingMoves = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

        for (int i = 0; i < kingMoves.GetLength(0); i++)
        {
            int newX = x + kingMoves[i, 0];
            int newY = y + kingMoves[i, 1];
            if (newX == targetX && newY == targetY && PositionOnBoard(newX, newY))
            {
                return true;
            }
        }
        return false;
    }

    public bool PawnMoveCheck(Chessmen piece, int targetX, int targetY, int direction)
    {
        int x = piece.GetXBoard();
        int y = piece.GetYBoard();

        
        if (PositionOnBoard(x + 1, y + direction) && x + 1 == targetX && y + direction == targetY)
        {
            return true;
        }
        if (PositionOnBoard(x - 1, y + direction) && x - 1 == targetX && y + direction == targetY)
        {
            return true;
        }

        return false;
    }

    public bool CheckForCheckMate()
    {
        if (!CheckForCheck())
            return false;

        GameObject[] playerPieces = currentPlayer == "white" ? playerWhite : playerBlack;

        foreach (GameObject piece in playerPieces)
        {
            if (piece == null) continue;

            Chessmen chessman = piece.GetComponent<Chessmen>();
            int originalX = chessman.GetXBoard();
            int originalY = chessman.GetYBoard();

            for (int x = 0; x<8; x++)
            {
                for  (int y = 0; y < 8; y++)
                {
                    if (!PieceCanAttackKing(chessman, x, y)) continue;

                    GameObject tempPiece = GetPosition(x, y);
                    SetPositionEmpty(originalX, originalY);
                    SetTemporaryPosition(piece, x, y);

                    bool stillInCheck = CheckForCheck();

                    SetTemporaryPosition(piece, originalX, originalY);
                    if (tempPiece != null) SetTemporaryPosition(tempPiece, x, y);

                    if (!stillInCheck) return false;

                }
            }
        }
        return true;

    }


}
