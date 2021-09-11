using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SessionManager
{
    public static COEStat FullStats { get; private set; }
    public static List<FatigueStat> FatigueStats { get; private set; }

    public static void NewSession(string name, string day, string month, string year)
    {
        SceneManager.LoadScene(1);
    }
    public static void LoadRealTest()
    {
        SceneManager.LoadScene(2);
    }

    public static void SessionResults(COEStat fullStats, List<FatigueStat> fatigueStats)
    {
        FullStats = fullStats;
        FatigueStats = fatigueStats;
        SceneManager.LoadScene(3);
    }

    public static void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}