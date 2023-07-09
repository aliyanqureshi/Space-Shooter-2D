using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _players;
    private Animator _anim;
    private AudioSource _audioSource;
    float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _players = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        if ( _anim == null)
        {
            Debug.Log("Animator is null");
        }
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.down * speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Players")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("Enemy_Collision");
            speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.3f);
           

        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_players != null)
            {
                _players.AddScore(10);
            }
            _anim.SetTrigger("Enemy_Collision");
            speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.3f);
            // _players.AddScore();
            
        }
    }
}