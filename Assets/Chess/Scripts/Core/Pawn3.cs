using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn3 : ChessPiece
{
    private void OnMouseDown()
{
    Select();
}


    public override void CalculateLegalMoves()
    {
        row = 1;
        col = 2;
        int direction = isWhite ? -1 : 1;
        int startRow = isWhite ? 1 : 6;

        // Forward move
        TryHighlight(row + direction, col);

        // Diagonal captures
        TryHighlightIfEnemy(row + direction, col - 1);
        TryHighlightIfEnemy(row + direction, col + 1);
    }

    private void TryHighlight(int r, int c)
    {
        if (IsInsideBoard(r, c) && !HasPiece(r, c))
        {
            ChessBoardPlacementHandler.Instance.Highlight(r, c);
        }
    }

    private void TryHighlightIfEnemy(int r, int c)
    {
        GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(r, c);
        if (IsInsideBoard(r, c) && tile != null && IsEnemyPiece(tile))
        {
            ChessBoardPlacementHandler.Instance.Highlight(r, c);
        }
    }

    private bool IsInsideBoard(int r, int c)
    {
        return r >= 0 && r < 8 && c >= 0 && c < 8;
    }

    private bool HasPiece(int r, int c)
    {
        GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(r, c);
        return tile != null && tile.transform.childCount > 0;
    }

    private bool IsEnemyPiece(GameObject tile)
    {
        if (tile.transform.childCount == 0) return false;

        ChessPiece other = tile.GetComponentInChildren<ChessPiece>();
        return other != null && other.isWhite != this.isWhite;
    }
}
