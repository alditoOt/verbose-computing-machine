﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGrid : MonoBehaviour
{
    public Transform StartPoint;
    public Image Countdown;
    public GameObject endScreen;
    public bool gameLost = false;

    public void RespawnPiece(PlayerPiece piece, float cooldown)
    {
        piece.transform.position = StartPoint.position;
        // To-do: Use another class to handle correct generation of pieces, as well as showing them on UI
        piece.Piece.InitializePiece(GetRandomTetrimino());
        var currentPoint = piece.Piece.pieceGridLocator.GlobalCurrentTilesPositions();
        var lowestPoint = piece.Piece.Grid.GetLowestPoints(currentPoint);
        var offset = lowestPoint[0].y - currentPoint[0].y;
        var newPos = new Vector2(piece.transform.position.x, piece.transform.position.y + offset);
        piece.transform.position = newPos;
        var height = newPos.y - piece.Piece.Grid.transform.position.y;
        if (height >= TetrisGrid.VISUAL_HEIGHT)
        {
            endScreen.SetActive(true);
            gameLost = true;
            Debug.Log("F");
            // TO-DO: Stop the game when lost
        }
        Countdown.DOKill();
        Countdown.fillAmount = 1;
        Countdown.color = Color.white;
        Countdown.DOColor(Color.red, cooldown);
        Countdown.DOFillAmount(0f, cooldown);
    }

    private Tetrimino GetRandomTetrimino()
    {
        Array values = Enum.GetValues(typeof(Tetrimino));
        System.Random random = new System.Random();
        return (Tetrimino)values.GetValue(random.Next(values.Length));
    }
}