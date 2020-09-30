using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState 
{
    //Сессия запущенна?
    public static bool IsStartingGame;
    //Текущая сцена(Уровень)
    public static int currentLevel;
    //Уыеличить текущий уровень
    public static void IncreateCurrentLevel()
    {
        currentLevel += 1;
    }
}
