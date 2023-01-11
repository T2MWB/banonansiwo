using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject TrollPrefab;
    [SerializeField]
    private GameObject DraugarPrefab;

    [SerializeField]
    private float TrollInterval;
    [SerializeField]
    private float DraugarInterval;
    public bool DisableSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!DisableSpawn)
        {
            StartCoroutine(spawnEnemy(DraugarInterval, DraugarPrefab));
            StartCoroutine(spawnEnemy(TrollInterval, TrollPrefab));
        }
        
    }
    
    private IEnumerator spawnEnemy (float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
