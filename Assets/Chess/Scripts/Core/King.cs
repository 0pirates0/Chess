using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public bool isWhite = true;

    // All 8 directions the King can move (1 step in each)
    private readonly int[,] directions = new int[,]
    {
        {1, 0},  // up
        {-1, 0}, // down
        {0, 1},  // right
        {0, -1}, // left
        {1, 1},  // up-right
        {1, -1}, // up-left
        {-1, 1}, // down-right
        {-1, -1} // down-left
    };

    public override void CalculateLegalMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newRow = row + directions[i, 0];
            int newCol = col + directions[i, 1];

            if (IsInsideBoard(newRow, newCol))
            {
                GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(newRow, newCol);

                if (!HasPiece(tile))
                {
                    ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol);
                }
                else if (IsEnemyPiece(tile))
                {
                    ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol); // could be red
                }
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

