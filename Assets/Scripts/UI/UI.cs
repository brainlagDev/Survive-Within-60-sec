using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _textCounter;    
    [SerializeField] private TimerView _timerView;    
    [SerializeField] private Text _textFailed;
    [SerializeField] private KeySpawner _keySpawner;
    //[SerializeField] private Image _changeBar;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _winnerPanel;
    [SerializeField] private GameObject _looserPanel;
    [SerializeField] private GameObject _betweenPanel;
    [SerializeField] private LevelGeneration _levelGeneration;
    
    private ArenaManager _arenaManger;
    private int count;

    public GameObject LastKilledEnemy;

    public void Init(ArenaManager arenaManager, LevelGeneration levelGeneration, KeySpawner keySpawner)
    {
        _arenaManger = arenaManager;
        _levelGeneration = levelGeneration;
        _keySpawner = keySpawner;
    }

    public void ChangeCounter()
    {        
        _textCounter.text = "Убито: " + count;
        if(count == _arenaManger.GetKeyKillsCount())
        {
            _keySpawner.SpawnKeyByKillCount(LastKilledEnemy.transform.position);
        }
    }

    public void IncreateCounter()
    {
        count += 1;
        ChangeCounter();
    }

    public void ResetCountKill()
    {
        count = 0;
        ChangeCounter();
    }

    public void ShowPanelFailedTimeIsOver()
    {
        ShowPanelLooser("Время вышло");        
    }

    /*public void SetHpInBar(float hp)
    {        
        _changeBar.fillAmount = hp * 0.01f;
    }*/

    public void TimerViewInit(Timer timer)
    {
        _timerView.Init(timer);
    }

    public void HideButtonPlay()
    {
        if (_playButton != null)
        {
            _playButton.SetActive(false);
        }
    }

    public void PlayGame()
    {
        _levelGeneration.StartGame();
    }

    public void AgainLevel()
    {
        SceneManager.LoadScene(GameState.currentLevel);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(GameState.currentLevel);
    }

    public void Exit()
    {
        Debug.Log("Вышел из игры");
        Application.Quit();
    }

    public void ShowPanelWinner()
    {
        GameState.IncreateCurrentLevel();
        _textFailed.text = "Вы прошли уровень";
        _betweenPanel.SetActive(true);
        _winnerPanel.SetActive(true);
        _looserPanel.SetActive(false);
    }

    public void ShowPanelLooser(string text)
    {
        _textFailed.text = text;
        _betweenPanel.SetActive(true);
        _winnerPanel.SetActive(false);
        _looserPanel.SetActive(true);
    }
}
