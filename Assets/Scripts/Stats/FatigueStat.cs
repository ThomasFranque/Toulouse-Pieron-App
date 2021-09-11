using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FatigueStat
{
    private int _minute;
    private COEStat _COE;

    public FatigueStat(int minute, COEStat cOE)
    {
        _minute = minute;
        _COE = cOE;
    }

    public int Minute { get => _minute; }
    public COEStat COE { get => _COE; }

    public string DebugTxt()
    {
        Debug.Log("C:" + COE.C + " | O:" + COE.O + " | E:" + COE.E);
        return "C:" + COE.C + " | O:" + COE.O + " | E:" + COE.E;
    }

    public int FatigueResistance => _COE.FatigueResistance;
}
