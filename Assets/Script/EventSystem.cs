using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;
    public static event Action OnGameOver;

    private int score = 0;     

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            score += 10;
            OnScoreChanged?.Invoke(score);
        }
        if(score >= 100)
        {
            OnGameOver?.Invoke();
        }
    }
}
