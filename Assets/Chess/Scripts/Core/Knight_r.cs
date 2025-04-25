using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_r : ChessPiece
{
    private void OnMouseDown()
{
    Select();
}

    private readonly int[,] moves = new int[,]
    {
        {2, 1}, {1, 2}, {-1, 2}, {-2, 1},
        {-2, -1}, {-1, -2}, {1, -2}, {2, -1}
    };

    public override void CalculateLegalMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();

        row = 0;
        col = 6;

        for (int i = 0; i < moves.GetLength(0); i++)
        {
            int newRow = row + moves[i, 0];
            int newCol = col + moves[i, 1];

            if (!IsInsideBoard(newRow, newCol)) continue;

            GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(newRow, newCol);

            if (!HasPiece(tile))
            {
                ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol);
            }
            else if (IsEnemyPiece(tile))
            {
                ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol);
            }
        }
    }

    private bool IsInsideBoard(int r, int c)
    {
        return r >= 0 && r < 8 && c >= 0 && c < 8;
    }

    private bool HasPiece(GameObject tile)
    {
        return tile != null && tile.transform.childCount > 0;
    }

    private bool IsEnemyPiece(GameObject tile)
    {
        if (!HasPiece(tile)) return false;

        ChessPiece other = tile.GetComponentInChildren<ChessPiece>();
        return other != null && other.isWhite != this.isWhite;
    }
}
