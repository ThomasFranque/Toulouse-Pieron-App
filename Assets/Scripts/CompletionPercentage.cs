using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompletionPercentage : MonoBehaviour
{
    TextMeshProUGUI _txtPro;
    private int initialO;
    private void OnEnable()
    {
        _txtPro = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.OnCOEChange += UpdatePercentage;
        GameManager.Instance.OnCOEInitialized += UpdateInitialO;
    }

    private void OnDisable()
    {

        GameManager.Instance.OnCOEChange -= UpdatePercentage;
        GameManager.Instance.OnCOEInitialized -= UpdateInitialO;
    }

    private void UpdateInitialO(COEStat coe)
    {
        initialO = coe.O;
    }

    private void UpdatePercentage(COEStat coe)
    {
        //!  you left here!!
        float perc = ((float)GameManager.Instance.COE.C - (float)GameManager.Instance.COE.E )/ (float)initialO;
        perc *= 100f;
        perc = (float)System.Math.Truncate(perc);
        _txtPro.text = perc + "%";
    }
}