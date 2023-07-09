using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    // For on and off the while loop in spawnroutine
    private bool _StopSpawning = false;
    void Start()
    {
        
    }
    public void StartSpawing()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnTripleshootRoutine());
    }
    // Update is called once per frame
    void Update()
    {

    }
    //Spawn Game Manager after 5 seconds
    //Create a coroutine of type IEnumerator -- Yeild events
    // While loop is infinite loop
    public void OnPlayerDeath()
    {
        _StopSpawning = true;
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_StopSpawning == false)
        {
            //public float randomx = Random.Range(-8.47f, 8.47f);

            GameObject NewEnemy = Instantiate(_enemyPrefab, transform.position + new Vector3(Random.Range(-8.47f, 8.47f), 7, 0), Quaternion.identity);
            NewEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(1.0f);
        }
    }
    IEnumerator SpawnTripleshootRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_StopSpawning == false)
        {
            int randomx = Random.Range(0, 3);
            Instantiate(powerups[randomx], transform.position + new Vector3(Random.Range(-8.47f, 8.47f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4, 8));
        }
    }
}
