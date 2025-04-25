using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    public int row, col;
    public bool isWhite;

    public void Select()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        CalculateLegalMoves();
    }

    public abstract void CalculateLegalMoves();
}
