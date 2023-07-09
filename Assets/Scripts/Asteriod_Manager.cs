using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod_Manager : MonoBehaviour
{
    private float _rotateSpeed = 3.0f;
    [SerializeField]
    private GameObject _Explossion;
    [SerializeField]
    private Spawn_Manager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager_").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_Explossion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawing();
            Destroy(this.gameObject, 0.3f);
        }
    }
}
