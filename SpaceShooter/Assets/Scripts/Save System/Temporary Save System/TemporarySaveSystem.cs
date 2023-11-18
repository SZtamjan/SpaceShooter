using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySaveSystem : MonoBehaviour
{
    public void WriteHighScore(int HS)
    {
        PlayerPrefs.SetInt("HighScore",HS);
    }

    public int ReadHighScore()
    {
        int HS = PlayerPrefs.GetInt("HighScore");
        return HS;
    }
}
