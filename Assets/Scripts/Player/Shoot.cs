using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform shootPos;
    public GameObject bulletPrefab;
    //import preferences from ArenaManager
    [SerializeField] private ArenaManager _arenaManager;
    public bool IsPlayerMoving = false;
    public AudioClip ShootSound;

    private void Start()
    {
        StartCoroutine(CheckNumberEnemies());
    }
    
    private void ShootS()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootPos.up * _arenaManager.GetBulletSpeed(), ForceMode.Impulse);
        transform.GetComponent<AudioSource>().PlayOneShot(ShootSound);
    }

    private IEnumerator CheckNumberEnemies()
    {
        while (true)
        {
            if (IsPlayerMoving == false)
            {
                ShootS();
            }
            yield return new WaitForSeconds(_arenaManager.GetShootDelay());
        }
    }
}
