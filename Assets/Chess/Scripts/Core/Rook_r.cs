using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook_r : ChessPiece
{
    private void OnMouseDown()
{
    Select();
}

    private readonly int[,] directions = new int[,]
    {
        {1, 0},   // up
        {-1, 0},  // down
        {0, 1},   // right
        {0, -1}   // left
    };

    public override void CalculateLegalMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        row = 0;
        col = 7;

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dRow = directions[i, 0];
            int dCol = directions[i, 1];
            int step = 1;

            while (true)
            {
                int newRow = row + step * dRow;
                int newCol = col + step * dCol;

                if (!IsInsideBoard(newRow, newCol))
                    break;

                GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(newRow, newCol);

                if (!HasPiece(tile))
                {
                    ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol);
                }
                else
                {
                    if (IsEnemyPiece(tile))
                        ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol);
                    break;
                }

                step++;
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
