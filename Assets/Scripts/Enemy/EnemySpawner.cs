using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{    
    [Header("Текущие живые враги")]
    [SerializeField] private List<GameObject> _currentLivingEnemies;
    [Header("Перед спавном врага префаб")]
    [SerializeField] private GameObject _enemySpawnPrefab;
    [SerializeField] private GameObject _enemiesHolder;

    private ArenaManager _arenaManager;
    private UI _ui;
    private Transform[] _pointSpawnPosition;
    private GameObject[] _prefabsEnemy;
    private Transform _playerPosition;
    private int _numberEnemies;
    private float _time;
    private IEnumerator coroutine;

    private void Awake()
    {
        _pointSpawnPosition = GetComponentsInChildren<Transform>();
    }

    public void Init(Transform playerPosition, ArenaManager arenaManager, UI ui)
    {
        _ui = ui;
        _arenaManager = arenaManager;
        _numberEnemies = arenaManager.GetNumberEnemies();
        _time = arenaManager.GetSpawnTimeInterval();
        _prefabsEnemy = arenaManager.GetPrefabsEnemy();
        _playerPosition = playerPosition;

        coroutine = CheckNumberEnemies();
        StartCoroutine(coroutine);
    }

    private IEnumerator CheckNumberEnemies()
    {
        while (true)
        {
            if (_currentLivingEnemies.Count < _numberEnemies)
            {
                //CreateEnemy();
                CreateEnemySpawn();
            }
            yield return new WaitForSeconds(_time);
        }
    }

    private void CreateEnemy()
    {
        var currentEnemy = Instantiate(_prefabsEnemy[Random.Range(0, _prefabsEnemy.Length)], _pointSpawnPosition[Random.Range(1, _pointSpawnPosition.Length)].position, Quaternion.identity, transform.GetChild(0));
        EnemyMovement enemyMovement = currentEnemy.GetComponent<EnemyMovement>();

        if (currentEnemy.GetComponent<ShootEnemy>())
        {
            currentEnemy.GetComponent<ShootEnemy>().Init(_arenaManager);
        }
        enemyMovement.Init(_playerPosition, _arenaManager);
        enemyMovement.Death += ExcludeEnemyFromList;
        _currentLivingEnemies.Add(currentEnemy);
    }
    private void CreateEnemySpawn()
    {
        GameObject spawn = Instantiate(_enemySpawnPrefab, _pointSpawnPosition[Random.Range(1, _pointSpawnPosition.Length)].position, Quaternion.identity, _enemiesHolder.transform);
        spawn.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y + 1, spawn.transform.position.z);
        //var currentEnemy = Instantiate(_prefabsEnemy[Random.Range(0, _prefabsEnemy.Length)], spawn.transform.position, Quaternion.identity, _enemiesHolder.transform);
        //currentEnemy.gameObject.SetActive(false);
        StartCoroutine(ActivationDelay(spawn));
        //currentEnemy.gameObject.SetActive(true);
        /*EnemyMovement enemyMovement = currentEnemy.GetComponent<EnemyMovement>();
        if (currentEnemy.GetComponent<ShootEnemy>())
        {
            currentEnemy.GetComponent<ShootEnemy>().Init(_arenaManager);
        }
        enemyMovement.Init(_playerPosition, _arenaManager);
        enemyMovement.Death += ExcludeEnemyFromList;
        _currentLivingEnemies.Add(currentEnemy);*/
    }

    private void ExcludeEnemyFromList(GameObject deadEnemy)
    {
        _currentLivingEnemies.Remove(deadEnemy);
        deadEnemy.GetComponent<EnemyMovement>().Death -= ExcludeEnemyFromList;
        _ui.LastKilledEnemy = deadEnemy;
        _ui.IncreateCounter();
        Destroy(deadEnemy);
    }

    private void GenerateMultipleEnemies(int numberEnemies)
    {
        for (int i = 0; i < numberEnemies; i++)
        {
            CreateEnemy();
        }
    }

    public List<GameObject> GetCurrentLivingEnemies()
    {
        return _currentLivingEnemies;
    }
    public void StopSpawn()
    {
        StopCoroutine(coroutine);
        /*for (int i = 0; i <_currentLivingEnemies.Count; i++)
        {
            _currentLivingEnemies.Remove(_currentLivingEnemies[i]);
        }*/
        /*foreach (GameObject enemy in _currentLivingEnemies)
        {
            _currentLivingEnemies.Remove(enemy);
        }*/
        //_currentLivingEnemies.Capacity = 0;
        GetComponent<LevelGeneration>().Player.GetComponent<Player>().IsLevelActive = false;
        for (int i = 0; i < _enemiesHolder.transform.childCount; i++)
        {
            Destroy(_enemiesHolder.gameObject);
        }
    }
    IEnumerator ActivationDelay(GameObject spawn)
    {
        yield return new WaitForSeconds(1f);
        Destroy(spawn.gameObject);
        //enemy.gameObject.SetActive(true);
        var currentEnemy = Instantiate(_prefabsEnemy[Random.Range(0, _prefabsEnemy.Length)], spawn.transform.position, Quaternion.identity, _enemiesHolder.transform);
        EnemyMovement enemyMovement = currentEnemy.GetComponent<EnemyMovement>();
        if (currentEnemy.GetComponent<ShootEnemy>())
        {
            currentEnemy.GetComponent<ShootEnemy>().Init(_arenaManager);
        }
        enemyMovement.Init(_playerPosition, _arenaManager);
        enemyMovement.Death += ExcludeEnemyFromList;
        _currentLivingEnemies.Add(currentEnemy);
    }
}