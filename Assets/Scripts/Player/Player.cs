using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private ArenaManager _arenaManager;

    [SerializeField] private EnemySpawner _enemySpawner;
    private KeySpawner _keySpawner;
    private GameObject _closiestEnemy;
    private UI _ui;

    public bool IsLevelActive=true;
    public float velocity_modifier;
    public float shift;
    
    //[SerializeField] private int _hitsCount=0;

    public void Init(EnemySpawner enemySpawner, float speed, KeySpawner keySpawner, ArenaManager manager, UI ui)
    {
        _ui = ui;
        _enemySpawner = enemySpawner;
        _keySpawner = keySpawner;
        _arenaManager = manager;
        velocity_modifier = speed;
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(0, 0,0);
        if (Input.GetAxis("Horizontal") != 0)
        {
            direction += Vector3.right * Input.GetAxis("Horizontal");
            GetComponent<Shoot>().IsPlayerMoving=true;
        }
        else
        {
            GetComponent<Shoot>().IsPlayerMoving = false;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            direction += Vector3.forward * Input.GetAxis("Vertical");
            GetComponent<Shoot>().IsPlayerMoving = true;
            shift += Input.GetAxis("Vertical");
            shift=Mathf.Clamp(shift, -1f, 1f);
        }
        else
        {
            if(shift>0)
            {
                shift -= 0.1f;
                shift = Mathf.Clamp(shift, 0f, 1f);
            }
            else
            {
                if(shift<0)
                {
                    shift += 0.1f;
                    shift = Mathf.Clamp(shift, -1f, 0f);
                }
            }
            GetComponent<Shoot>().IsPlayerMoving = false;
        }
        transform.gameObject.GetComponent<Rigidbody>().velocity = direction * velocity_modifier;
        float _minDistance = 100;

        if (IsLevelActive == true)
        {
            int index = 0;
            for (int i = 0; i < _enemySpawner.GetCurrentLivingEnemies().Count; i++)
            {
                if ((_enemySpawner.GetCurrentLivingEnemies()[i].GetComponent<EnemyMovement>().DistanceToPlayer < _minDistance))
                {
                    _minDistance = _enemySpawner.GetCurrentLivingEnemies()[i].GetComponent<EnemyMovement>().DistanceToPlayer;
                    index = i;
                }
            }
            if (_enemySpawner.GetCurrentLivingEnemies().Count > 0)
            {
                _closiestEnemy = _enemySpawner.GetCurrentLivingEnemies()[index];
                transform.GetChild(0).LookAt(new Vector3(_closiestEnemy.transform.position.x, 1, _closiestEnemy.transform.position.z));
            }
        }

        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z + shift);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == _arenaManager.GetKeyName())
        {
            GameObject.Destroy(other.gameObject);
            _keySpawner.Portal.GetComponent<BoxCollider>().enabled = true;
            StopLevel();
        }
        else
        {
            if (other.name == _arenaManager.GetPortalName())
            {
                GameObject.Destroy(other.gameObject);
                NextLevel();
            }
        }
    }
    /*public void HitsCountPlus()
    {
        _hitsCount++;
        if (_hitsCount == _arenaManager.GetHitsForTremor())
        {
            _hitsCount = 0;
            StartCoroutine(ShakeCamera(0.2f,0.05f));
        }
    }*/
   /* private IEnumerator ShakeCamera(float duration,float displacement)
    {
        Vector3 startPosition = Camera.main.transform.position;
        float time= 0f;
        while (time < duration)
        {
            Camera.main.transform.position = Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 11.39f, transform.position.z + displacement);

            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = Camera.main.transform.position = startPosition;
    }*/
    public void NextLevel()
    {
        _ui.ShowPanelWinner();
    }

    public void GetDamaged()
    {
        _ui.ShowPanelLooser("Bы погибли");        
    }

    private void StopLevel()
    {
        _enemySpawner.StopSpawn();
        Debug.Log("Stop Level");
    }
}
