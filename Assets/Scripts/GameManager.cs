using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public COEStat COE => _COE;

    [SerializeField] private BoardGenerator _generator;
    [SerializeField] private GenerationProfile _generationProfile;
    [Header("Tutorial")]
    [SerializeField] private bool _isTutorial;
    [SerializeField] private GameObject _enableWhenDone;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Toggle _understoodToggle;

    private List<FatigueStat> _fatigueStats;
    private Timer _timer;
    private Coroutine _oneMinuteCor;
    private COEStat _COE;

    private void Awake()
    {
        Instance = this;
        _COE = new COEStat(0, 0, 0);
        _fatigueStats = new List<FatigueStat>();
        _timer = GetComponent<Timer>();

        if (!_isTutorial)
        {
            _timer.Begin(_generationProfile.Duration, OnTimeEnd);
            _oneMinuteCor = StartCoroutine(COneMinute());
        }
        else
            OnCOEChange += EndTutorialCheck;

        _continueButton?.onClick.AddListener(LoadRealTest);
    }

    private void Start()
    {
        _generator.Generate(_generationProfile);
        OnCOEInitialized?.Invoke(_COE);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Time.timeScale = 50;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (_isTutorial)
        {
            _continueButton.interactable = _understoodToggle.isOn;
        }
    }

    private IEnumerator COneMinute()
    {
        yield return new WaitForSeconds(60.02f);
        MinutePassed();
    }

    private void MinutePassed()
    {
        SaveFatigueStat(_generationProfile.Duration - (int) _timer.Minutes - 1);
        _oneMinuteCor = StartCoroutine(COneMinute());
    }

    private void SaveFatigueStat(int minute)
    {
        COEStat fCoe = new COEStat(_COE.C, _COE.O, _COE.E);
        FatigueStat fat = new FatigueStat(minute, fCoe);
        _fatigueStats.Add(fat);
        fat.DebugTxt();
    }

    private void OnTimeEnd()
    {
        Time.timeScale = 1;
        if (!_isTutorial)
        {
            StopCoroutine(_oneMinuteCor);
            SaveFatigueStat(_generationProfile.Duration);
            SessionManager.SessionResults(_COE, _fatigueStats);
        }
    }

    public void Ticked(SymbolBehaviour sym)
    {
        if (sym.IsCorrect) // Correct
        {
            _COE.C++;
            _COE.O--;
        }
        else // Wrong answer
        {
            _COE.E++;
        }

        OnCOEChange?.Invoke(_COE);
    }

    public void UnTicked(SymbolBehaviour sym)
    {
        if (sym.IsCorrect) // Omitted
        {
            _COE.C--;
            _COE.O++;
        }
        else // Corrected wrong answer
        {
            _COE.E--;
        }
        OnCOEChange?.Invoke(_COE);
    }

    private void EndTutorialCheck(COEStat coe)
    {
        _enableWhenDone.SetActive(coe.O == 0);
    }

    private void LoadRealTest()
    {
        SessionManager.LoadRealTest();
    }

    public void InitialOmission(SymbolBehaviour sym)
    {
        if (sym.IsCorrect) // Omitted
        {
            _COE.O++;
        }
    }

    public System.Action<COEStat> OnCOEInitialized;
    public System.Action<COEStat> OnCOEChange;
}