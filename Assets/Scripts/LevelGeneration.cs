using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
[RequireComponent(typeof(KeySpawner))]
public class LevelGeneration : MonoBehaviour
{
    public static LevelGeneration Reference;
    [SerializeField] private ArenaManager _arenaManager;
    [SerializeField] private Transform _startPlayerPosition;
    [Header("Префабы:")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _uiPrefab;
    [SerializeField] private GameObject _timerPrefab;    

    private Timer _timer;
    private UI _ui;
    private Action _endGame;
    private Transform _playerPosition;
    private EnemySpawner _enemySpawner;
    private KeySpawner _keySpawner;
    private GameObject _player;
    public GameObject Player => _player;

    private void Awake()
    {       
        Reference = this;
        _keySpawner = GetComponent<KeySpawner>();
        _enemySpawner = GetComponent<EnemySpawner>();        
    }

    private void Start()
    {
        Camera.main.transform.position = _arenaManager.GetCamera().transform.position;
        Camera.main.transform.rotation = _arenaManager.GetCamera().transform.rotation;
        var currentUi = Instantiate(_uiPrefab);
        _ui = currentUi.GetComponent<UI>();
        _ui.Init(_arenaManager, this, _keySpawner);
        _ui.ResetCountKill();
        _keySpawner.Init(_arenaManager);
        
        if (GameState.IsStartingGame)
        {            
            StartGame();
        }            
    }

    public void StartGame()
    {                
        _ui.HideButtonPlay();        
        _endGame += _ui.ShowPanelFailedTimeIsOver;
        Transform player = CreatePlayer();
        _enemySpawner.Init(player, _arenaManager, _ui);
        var timer = Instantiate(_timerPrefab);
        _ui.TimerViewInit(timer.GetComponent<Timer>());
        _timer = timer.GetComponent<Timer>();
        _timer.StartTimer(60, _endGame);
        GameState.IsStartingGame = true;
    }

    private Transform CreatePlayer()
    {
        _player =  Instantiate(_playerPrefab, _startPlayerPosition.position, Quaternion.identity);
        _player.GetComponent<Player>().Init(_enemySpawner, _arenaManager.GetSpeedPlayer(),_keySpawner,_arenaManager, _ui);        
        _player.name = "Player";
        return _player.GetComponent<Transform>();
    }
}
