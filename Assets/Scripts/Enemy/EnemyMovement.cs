using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;    
    [SerializeField] private bool _isMove;
    [SerializeField] private float _waitingTimeBeforeAttack;    

    private ShootEnemy _shootEnemy;    
    private NavMeshAgent _agent;
    private UnityAction<GameObject> _death;
    private ArenaManager _arenaManager;

    public float DistanceToPlayer;

    public UnityAction<GameObject> Death { get => _death; set => _death = value; }

    public void Init(Transform player, ArenaManager arenaManager)
    {
        _arenaManager = arenaManager;
        if (GetComponent<ShootEnemy>())
        {
            _shootEnemy = GetComponent<ShootEnemy>();
        }        
        _agent = GetComponent<NavMeshAgent>();
        _player = player;
        DistanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (DistanceToPlayer > _arenaManager.SpawnDistanceTooCloseToPlayer())
        {
            _isMove = true;
        }
        else
        {
            Debug.Log("Стоит ждет");
            _isMove = false;
        }
    }
    
    private void FixedUpdate()
    {
        if (_isMove == true)
        {
            _agent.SetDestination(_player.position);
            DistanceToPlayer = Vector3.Distance(transform.position, _player.position);
            if (DistanceToPlayer <= _agent.stoppingDistance) //Враг остановился
            {
                transform.LookAt(_player);
                if (_shootEnemy != null)
                {
                    _shootEnemy.OpenFire();
                }
            }
            else
            {
                if (_shootEnemy != null)
                {
                    _shootEnemy.CloseFire();
                }
            }            
        }
        else
        {
            _waitingTimeBeforeAttack -= Time.deltaTime;

            if (_waitingTimeBeforeAttack <= 0.0f)
            {
                _isMove = true;
            }
        }

        if (DistanceToPlayer <= 3f)
        {
            if (_shootEnemy != null)
            {
                _shootEnemy.CloseFire();
            }
            
        }
    }

    public void DeathEnemy()
    {
        Death?.Invoke(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WeaponPlayer>())
        {
            DeathEnemy();
        }
    }
}
