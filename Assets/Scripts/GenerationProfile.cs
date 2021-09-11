using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Generation Profile")]
public class GenerationProfile : ScriptableObject
{
    [Header("Test Properties")]
    [SerializeField, Min(1)] private int _duration = 10;
    [SerializeField] private int _columns = 40;
    [SerializeField] private int _rows = 25;
    [SerializeField, Range(1, 6)] private int _correctSymbols = 3;
    [Header("Grid Properties")]
    [SerializeField] private Vector2 _cellSize = new Vector2(36, 36);
    [SerializeField] private Vector2 _spacing = new Vector2(3, 3);

    public int Duration { get => _duration; set => _duration = value; }
    public int Rows { get => _rows; set => _rows = value; }
    public int Columns { get => _columns; set => _columns = value; }
    public int CorrectSymbols { get => _correctSymbols; set => _correctSymbols = value; }
    public Vector2 CellSize { get => _cellSize; set => _cellSize = value; }
    public Vector2 Spacing { get => _spacing; set => _spacing = value; }
}