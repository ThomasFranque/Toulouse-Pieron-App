using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COEStat
{
    // C - Correct
    // O - Omissions
    // E - Incorrect

    public COEStat(int c, int o, int e)
    {
        C = c;
        O = o;
        E = e;
    }

    public int C { get; set; }
    public int O { get; set; }
    public int E { get; set; }

    public void DebugTxt()
    {
        Debug.Log("C:" + C + " | O:" + O + " | E:" + E);
    }

    public int RealizationPower => C;
    public float ConcentrationPower => ((O + E) / C) * 100f;
    public int FatigueResistance => C - (O + E);
}