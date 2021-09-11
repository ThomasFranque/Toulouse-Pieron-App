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
    [SerializeField] private RectTransform _dummyPoint;

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

        GenerateGraph();
    }

    private void GenerateGraph()
    {
        int lowestStat = 9999999;

        foreach (FatigueStat f in SessionManager.FatigueStats)
        {
            int cur = f.FatigueResistance;

            if (cur < lowestStat)
                lowestStat = cur;
        }

        _dummyPoint.gameObject.SetActive(false);
        GraphPoint prev = default;
        List<GraphPoint> points = new List<GraphPoint>();
        foreach (FatigueStat f in SessionManager.FatigueStats)
        {
            GraphPoint newPoint = Instantiate(_dummyPoint.gameObject, _graphGrid).GetComponent<GraphPoint>();
            newPoint.gameObject.SetActive(true);

            float yIncrement = (f.FatigueResistance - lowestStat) * 30;
            newPoint.Init(yIncrement, f.Minute);

            points.Add(newPoint);

            prev = newPoint;
        }

        StartCoroutine(CDelayBeforeConnect(points));
    }

    // Delay because unity does not update grid layouts in the same frame (cheeky solution)
    private IEnumerator CDelayBeforeConnect(List<GraphPoint> points)
    {
        foreach (GraphPoint p in points)
        {
            yield return new WaitForSeconds(0.2f);
            p.Connect();
        }
    }

    public void BackToSquare1()
    {
        SessionManager.LoadStartScene();
    }
}