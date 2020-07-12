using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidAI : MonoBehaviour
{

    [SerializeField]
    private GameObject _asteroidExplosionPrefab;

    //variable for your speed
    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _clip;

    // Use this for initialization
    private void Start () {

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	private void Update () {

        Movement();
    }

    // Movement method
    private void Movement()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //when off the screen on he bottom
        //respan back on top with a new x position between the bounds of the screen
        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7f, 7f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            Instantiate(_asteroidExplosionPrefab, transform.position, Quaternion.identity);

            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            //damage player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_asteroidExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}
