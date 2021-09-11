using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardGenerator : MonoBehaviour
{
    private Image[] _spawnedCorrectSymbols;

    private List<SymbolBehaviour[]> _rows;
    private List<int> _possibleRowSymbols;
    private static int[] _correctSymbols;
    private SymbolsEnum lastChosenSym;

    [Header("Profile")]
    private GenerationProfile _generationProfile;
    [SerializeField] private bool _keepLastCorrects;
    [Header("Prefabs")]
    [SerializeField] private GameObject _symbolPrefab;
    [SerializeField] private GameObject _correctSymbolPrefab;
    [Header("References")]
    [SerializeField] private SymbolsResource _resource;
    [SerializeField] private GridLayoutGroup _symbolsGrid;
    [SerializeField] private GridLayoutGroup _correctSymbolsGrid;

    private List<int> NewPossibles => new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

    private bool IsSymbolCorrect(int sToCheck)
    {
        for (int i = 0; i < _correctSymbols.Length; i++)
            if (sToCheck == _correctSymbols[i])
                return true;
        return false;
    }
    private bool IsSymbolCorrect(SymbolsEnum sToCheck) => IsSymbolCorrect((int) sToCheck);

    private void Awake()
    {
        // Random.InitState(10);
    }

    public void Generate(GenerationProfile profile)
    {
        _generationProfile = profile;
        lastChosenSym = SymbolsEnum.None;
        SetupGrid();
        PickCorrects();
        CreateCorrectSymbolsReference();
        CreateRows(_generationProfile.Rows, _generationProfile.Columns);
        System.GC.Collect();
    }

    private void SetupGrid()
    {
        _symbolsGrid.cellSize = _generationProfile.CellSize;
        _symbolsGrid.spacing = _generationProfile.Spacing;
        _symbolsGrid.constraintCount = _generationProfile.Columns;

        _correctSymbolsGrid.cellSize = _generationProfile.CellSize;
    }

    private void PickCorrects()
    {
        if (_keepLastCorrects && _correctSymbols != default) return;
        int correctAmount = _generationProfile.CorrectSymbols;
        List<int> possibles = NewPossibles;
        _correctSymbols = new int[correctAmount];

        for (int i = 0; i < correctAmount; i++)
        {
            if (possibles.Count == 0) possibles = NewPossibles;
            int chosen = possibles[Random.Range(0, possibles.Count)];
            possibles.Remove(chosen);
            _correctSymbols[i] = chosen;
        }
    }

    private void CreateRows(int rowCount, int columnCount)
    {
        _rows = new List<SymbolBehaviour[]>(rowCount);

        for (int i = 0; i < rowCount; i++)
        {
            PopulatePossibleRowSymbols(columnCount);
            _rows.Add(CreateRow(columnCount));
        }
    }

    private void PopulatePossibleRowSymbols(int columnCount)
    {
        _possibleRowSymbols = new List<int>();
        List<int> possibles = NewPossibles;

        int iterations = columnCount / possibles.Count;

        for (int i = 0; i < iterations; i++)
            foreach (int s in possibles)
                _possibleRowSymbols.Add(s);
    }

    private SymbolBehaviour[] CreateRow(int columnCount)
    {
        SymbolBehaviour[] row = new SymbolBehaviour[columnCount];
        for (int i = 0; i < columnCount; i++)
        {
            row[i] = (CreateBoardSymbol());
        }
        return row;
    }

    private SymbolBehaviour CreateBoardSymbol()
    {
        List<int> possibles = _possibleRowSymbols;
        GameObject newObj = Instantiate(_symbolPrefab, _symbolsGrid.transform);
        SymbolBehaviour behaviour = newObj.GetComponent<SymbolBehaviour>();
        SymbolsEnum chosenSym;

        // It tries to avoid equal symbols back-to-back, but there is almost always one
        int maxIter = 10;
        int i = 0;
        do
        {
            chosenSym = (SymbolsEnum) possibles[Random.Range(0, possibles.Count)];
            i++;
        } while (chosenSym == lastChosenSym && i < maxIter);

        behaviour.Init(_resource, chosenSym, IsSymbolCorrect(chosenSym));

        possibles.Remove((int) chosenSym);
        lastChosenSym = chosenSym;
        return behaviour;
    }

    private void CreateCorrectSymbolsReference()
    {
        if (_spawnedCorrectSymbols != default)
            for (int i = 0; i < _spawnedCorrectSymbols.Length; i++)
            {
                Destroy(_spawnedCorrectSymbols[i].gameObject);
            }
        _spawnedCorrectSymbols = new Image[_correctSymbols.Length];
        for (int i = 0; i < _correctSymbols.Length; i++)
        {
            Image newI = Instantiate(_correctSymbolPrefab, _correctSymbolsGrid.transform).GetComponent<Image>();

            _spawnedCorrectSymbols[i] = newI;
            newI.sprite = _resource.GetSpriteOf((SymbolsEnum) _correctSymbols[i]);
        }
    }
}