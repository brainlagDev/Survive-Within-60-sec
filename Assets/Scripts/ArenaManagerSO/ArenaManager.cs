using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArenaManager", menuName = "ArenaManager")]
public class ArenaManager : ScriptableObject 
{
    [Header("Скорость игрока")]
    [SerializeField] private float playerSpeed;
    [Header("Максимальное колличество врагов на уровне")]
    [SerializeField] private int _numberEnemies;
    [Header("Интервал времени через которое проверяется максимальное количество врагов на уровне")]
    [SerializeField] private float _time;
    [Header("Префабы врагов")]
    [SerializeField] private GameObject[] _prefabsEnemy;
    [Header("Расстояние слишком близкого спавна врага к игроку")]
    [SerializeField] private float _spawnDistanceTooCloseToPlayer;
    [Header("Колличество врагов после которого падает ключ")]
    [SerializeField] private int _keyKillsCount;
    [Header("Имя портала")]
    [SerializeField] private string _portalName;
    [Header("Имя ключевого для выигрыша предмета")]
    [SerializeField] private string _keyName;    
    [Header("Скорость пуль")]
    [SerializeField] private float _bulletSpeed;
    [Header("Задержка между выстрелами игрока")]
    [SerializeField] private float _shootDelay; 
    [Header("Враг: Скорость пуль")]
    [SerializeField] private float _enemyBulletSpeed;
    [Header("Задержка между выстрелами игрока")]
    [SerializeField] private float _enemyShootDelay;
    [Header("Количество попаданий для тряски камеры")]
    [SerializeField] private int _hitsForTremor;
    [Header("Текущий уровне")]
    [SerializeField] private int _currentLevel;
    [Header("Настройки камеры")]
    [SerializeField] private GameObject _camera;

    private ArenaManager _arenaManager;

    public GameObject GetCamera()
    {
        return _camera;
    }

    public float GetSpeedPlayer()
    {
        return playerSpeed;
    }
    public int GetNumberEnemies()
    {
        return _numberEnemies;
    }
    public float GetSpawnTimeInterval()
    {
        return _time;
    }

    public GameObject[] GetPrefabsEnemy()
    {
        return _prefabsEnemy;
    }

    public float SpawnDistanceTooCloseToPlayer()
    {
        return _spawnDistanceTooCloseToPlayer;
    }

    public int GetKeyKillsCount()
    {
        return _keyKillsCount;
    }
    //KeySpawner props
    public string GetPortalName() {
        return _portalName;
    }

    public string GetKeyName() {
        return _keyName;
    }
    //Player Shoot props
    public float GetBulletSpeed()
    {
        return _bulletSpeed;
    }

    public float GetShootDelay()
    {
        return _shootDelay;
    }
    //Enemy props
    public int GetHitsForTremor()
    {
        return _hitsForTremor;
    }
     public float GetEnemyBulletSpeed()
    {
        return _enemyBulletSpeed;
    }
    public float GetEnemyShootDelay()
    {
        return _enemyShootDelay;
    }
    public int GetCurrentLevel()
    {
        return _currentLevel;
    }
}
