using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _keyPrefab;
    [SerializeField] private GameObject _portalPrefab;

    private ArenaManager _arenaManager;
    private GameObject _portal;    
    private GameObject _key;
    
    public GameObject Portal => _portal;

    public void Init(ArenaManager arenaManager)
    {
        _arenaManager = arenaManager;
        _portal = Instantiate(_portalPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        _portal.name = _arenaManager.GetPortalName();
    }

    void Start()
    {
        
    }

    public void SpawnKeyByKillCount(Vector3 position)
    {
        //Key=Instantiate(_keyPrefab, position, Quaternion.identity);
        _key = Instantiate(_keyPrefab, position, Quaternion.Euler(new Vector3(0,0,-90)));
        _key.name = _arenaManager.GetKeyName();
        //GameObject.Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
        GetComponent<KeySpawner>().enabled = false;
    }
}
