using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Symbols Data")]
public class SymbolsResource : ScriptableObject
{
    [SerializeField] private Sprite _topRight;
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _botRight;
    [SerializeField] private Sprite _bot;
    [SerializeField] private Sprite _botLeft;
    [SerializeField] private Sprite _left;
    [SerializeField] private Sprite _topLeft;
    [SerializeField] private Sprite _top;

    public Sprite[] SpriteCollection;

    public Sprite TopRight => _topRight;
    public Sprite Right => _right;
    public Sprite BotRight => _botRight;
    public Sprite Bot => _bot;
    public Sprite BotLeft => _botLeft;
    public Sprite Left => _left;
    public Sprite TopLeft => _topLeft;
    public Sprite Top => _top;

    private void OnEnable()
    {
        CreateCollection();
    }

    private void CreateCollection()
    {
        SpriteCollection = new Sprite[8]
        {
            TopLeft,
            Top,
            TopRight,
            Right,
            BotRight,
            Bot,
            BotLeft,
            Left
        };
    }

    public Sprite GetSpriteOf(SymbolsEnum symbol)
    {
        if (SpriteCollection == default) CreateCollection();
        return SpriteCollection[(int) symbol];
    }
}