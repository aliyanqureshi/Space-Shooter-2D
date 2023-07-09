using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _Scoress = 10;
    [SerializeField]
    private int speed = 5;
    private int _MultiplerSpeed = 2;
    [SerializeField]
    private GameObject _laserprefab;
    private float _firerate = 0.5f;
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private Spawn_Manager _Spawn_Manager;
    [SerializeField]
    private GameObject Tripleshootprefbab;
    [SerializeField]
    private bool _tripleShoot = false;
    [SerializeField]
    private bool _SpeedUp = false;
    private bool _shieldup = false;
    [SerializeField]
    private GameObject _ShieldVisualizer;
    [SerializeField]
    private UI_Manager _UI_Manager;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;
    [SerializeField]
    private AudioClip _LaserShootAudio;
    private AudioSource _AudioSource;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-0.21f, -1.98f, 0);

        _Spawn_Manager = GameObject.Find("SpawnManager_").GetComponent<Spawn_Manager>();
        _UI_Manager = GameObject.Find ("Canvas").GetComponent<UI_Manager>();
        _AudioSource = GetComponent<AudioSource>();

        if ( _AudioSource == null ) {
            Debug.Log("This Audio Source is null");
        }
        else
        {
            _AudioSource.clip = _LaserShootAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovementCalculation();
        

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            shootLaser();
        }

    }

    void MovementCalculation()
    {
        if (transform.position.y <= -4.030974)
         {
             transform.position = new Vector3(transform.position.x, -4.030974f, 0);
         }
         else if (transform.position.y >= 5.984093)
         {
             transform.position = new Vector3(transform.position.x, 5.984093f, 0);
         }
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(HorizontalInput, VerticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x <= -15)

        {
            transform.position = new Vector3(13f, transform.position.y, 0);
        }
        else if (transform.position.x >= 13)
        {
            transform.position = new Vector3(-15f, transform.position.y, 0);
        }
    }

    void shootLaser()
    {
        _canfire = Time.time + _firerate;

        if (_tripleShoot == true)
        {
            Instantiate(Tripleshootprefbab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 1.07f, 0), Quaternion.identity);
        }
        _AudioSource.Play();
    }



    public void Damage()
    {
        if (_shieldup == true)
        {
            _shieldup = false;
            _ShieldVisualizer.SetActive(false);
            return;
        }
        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        _UI_Manager.updateLives(_lives);
        if (_lives < 1) { Destroy(this.gameObject);

            _Spawn_Manager.OnPlayerDeath();
        }
    }
    public void Tripleshootactive()
    {
        _tripleShoot = true;
        StartCoroutine(TripleShootPowerDownRoutine());
    }
    public void SpeedPowerupActive()
    {
        _SpeedUp = true;
        StartCoroutine(SpeedpowerUp());
        speed *= _MultiplerSpeed;
    }

    public void ShieldActive()
    {
        _shieldup = true;
        _ShieldVisualizer.SetActive(true);
    }
    IEnumerator TripleShootPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShoot = false;
    }
    IEnumerator SpeedpowerUp()
    {
        yield return new WaitForSeconds(5.0f);
        _SpeedUp = false;
        speed /= _MultiplerSpeed;
    }
    public void AddScore(int points)
    {
        _Scoress += points ;
        _UI_Manager.updateScore(_Scoress);
    }
}