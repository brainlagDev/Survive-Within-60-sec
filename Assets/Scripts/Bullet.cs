using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (collision.gameObject.GetComponent<Player>().IsLevelActive == true)
            {
                collision.gameObject.GetComponent<Player>().GetDamaged();
            }
        }
        if(collision.gameObject.GetComponent<EnemyMovement>())
        {
            //LevelGeneration.Reference.Player.GetComponent<PlayerMovement>().HitsCountPlus();
            collision.gameObject.GetComponent<EnemyMovement>().DeathEnemy();
        }
        
        Destroy(gameObject);                
    }
}
