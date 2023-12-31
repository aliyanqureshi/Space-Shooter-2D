using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShoot : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int PowerupsiD;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            switch (PowerupsiD)
            {
                case 0:
                    player.Tripleshootactive();
                    break;
                case 1:
                    player.SpeedPowerupActive();
                    break;
                case 2:
                    player.ShieldActive();
                    break;

            }

        }
        Destroy(gameObject);
    }
}
