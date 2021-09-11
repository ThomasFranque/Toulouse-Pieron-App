using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SymbolBehaviour : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private Color _defaultColor = Color.black;
    [SerializeField] private Color _pressedColor = Color.gray;
    [SerializeField] private bool _isCorrect;
    [SerializeField] private bool _isTicked;
    [SerializeField] private SymbolsEnum _symbol;
    [Header("Events")]
    [SerializeField] private UnityEvent _onTicked;
    [SerializeField] private UnityEvent _onUnTicked;
    private SymbolsResource _resource;

    public bool IsCorrect => _isCorrect;
    public bool IsTicked => _isTicked;

    private void Start()
    {
        _button.onClick.AddListener(Click);
        _image.color = _defaultColor;
    }

    public void Init(SymbolsResource resource, SymbolsEnum symbol, bool isCorrect)
    {
        _resource = resource;
        _symbol = symbol;
        _isCorrect = isCorrect;
        _image.sprite = resource.GetSpriteOf(symbol);
        GameManager.Instance.InitialOmission(this);
    }

    private void Click()
    {
        _isTicked = !_isTicked;

        if (_isTicked)
        {
            _image.color = _pressedColor;
            _onTicked.Invoke();
            GameManager.Instance.Ticked(this);
        }
        else 
        {
            _image.color = _defaultColor;
            _onUnTicked.Invoke();
            GameManager.Instance.UnTicked(this);
        }
    }
}