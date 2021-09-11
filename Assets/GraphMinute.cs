using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphMinute : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _min;
    [SerializeField] private TextMeshProUGUI _res;
    [SerializeField] private TextMeshProUGUI _coe;

    public void Initialize(string min, string res, string coe)
    {
        _min.text = min;
        _res.text = res;
        _coe.text = coe;
    }
}
