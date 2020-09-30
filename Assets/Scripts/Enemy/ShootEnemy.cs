using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [SerializeField] private float _intervalBetweenShots;
    private bool _isOpenFire = false;
    private bool _isFire;

    public Transform shootPos;
    public GameObject bulletPrefab;
    //import preferences from ArenaManager
    [SerializeField] private ArenaManager _arenaManager;

    public void Init(ArenaManager arenaManager)
    {
        _arenaManager = arenaManager;
    }

    public void OpenFire()
    {
        if (_isOpenFire == false )
        {
            _isFire = true;
            StartCoroutine(CheckNumberEnemies());
            _isOpenFire = true;
        }       
    }

    public void CloseFire()
    {
        _isFire = false;        
        StopCoroutine(CheckNumberEnemies());        
    }

    private void ShootS()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootPos.up * _arenaManager.GetEnemyBulletSpeed(), ForceMode.Impulse);
    }

    private IEnumerator CheckNumberEnemies()
    {
        while (_isFire)
        {
            ShootS();
            yield return new WaitForSeconds(_arenaManager.GetEnemyShootDelay());
        }
        _isOpenFire = false;
    }
}
