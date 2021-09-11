using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{
    const int MIN_MAX_GRAPH = 250;
    [SerializeField] private TextMeshProUGUI _realizationPower;
    [SerializeField] private TextMeshProUGUI _concentrationPower;
    //[SerializeField] private TextMeshProUGUI _fatigueResistance;
    [Space]
    [SerializeField] private RectTransform _graphGrid;
    [SerializeField] private RectTransform _dummyMin;
    [SerializeField] private TextMeshProUGUI _finalCOE;

    private void Start()
    {
        int rp = SessionManager.FullStats.RealizationPower;

        //80---------muito lento
        // 80-100---------------lento
        // 100-150----------normal
        // 150-200-------------bom
        // > 200-

        if (rp < 80)
            _realizationPower.text = rp.ToString() + ": Muito Lento";
        else if (rp < 100)
            _realizationPower.text = rp.ToString() + ": Lento";
        else if (rp < 150)
            _realizationPower.text = rp.ToString() + ": Normal";
        else if (rp < 200)
            _realizationPower.text = rp.ToString() + ": Bom";
        else
            _realizationPower.text = rp.ToString() + ": Muito Bom!";
        _dummyMin.gameObject.SetActive(false);
        _finalCOE.text = SessionManager.FatigueStats[SessionManager.FatigueStats.Count - 1].COE.DebugTxt();
        GenerateGraph();
    }

    private void GenerateGraph()
    {
        foreach (FatigueStat f in SessionManager.FatigueStats)
        {
            GameObject go = Instantiate(_dummyMin.gameObject, _graphGrid);
            go.SetActive(true);
            go.GetComponent<GraphMinute>().Initialize(f.Minute.ToString(), f.FatigueResistance.ToString(), f.COE.DebugTxt());
        }
    }

    public void BackToSquare1()
    {
        SessionManager.LoadStartScene();
    }
}