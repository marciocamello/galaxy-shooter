using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //public or private identify
    //data type ( int, floats, bool, strings )
    //every variable has a NAME
    //option vale assigned

    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool shieldsActive = false;
    public int lives = 3;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    //fireRare is 0.25f
    //canFore -- has the amount time between firing passed?
    //Time.time

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int hitCount = 0;

	// Use this for initialization
	private void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        /*if (_spawnManager != null)
        {
            _spawnManager.StartCoroutines();
        }*/

        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;
        
        // if co-op mode don't reload position
        if (_gameManager.isCoopMode == false)
        {
            //current pos = new position
            transform.position = new Vector3(0, 0, 0);
        }
    }
	
	// Update is called once per frame
	private void Update ()
    {
        // if player one
        if (isPlayerOne == true)
        {
            Movement();

            //if space key pressed
            //spawn laser at player position
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isPlayerOne == true)
            {
                //spawn my laser
                Shoot();
            }
        }

        // if player two
        if (isPlayerTwo == true)
        {
            PlayerTwoMovement();

            //if space key pressed
            //spawn laser at player position
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && isPlayerTwo == true)
            {
                //spawn my laser
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {

            _audioSource.Play();

            if (canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.53f, 0), Quaternion.identity);
            }
            
            _canFire = Time.time + _fireRate;
        }
    }

    // Movement method
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // if speed boost enabled
        //move 1.5x the normal speed
        //else
        //move normal speed
        if (isSpeedBoostActive == true)
        {
            if (_gameManager.isCoopMode == false)
            {
                transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
                transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
            }
            else
            {
                // if hit 8
                // move up
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.up * _speed * 1.5f * Time.deltaTime);
                }

                // if hit 6
                // move right
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * _speed * 1.5f * Time.deltaTime);
                }

                // if hit 2
                // move down
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.down * _speed * 1.5f * Time.deltaTime);
                }

                // if hit 4
                // move left
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * _speed * 1.5f * Time.deltaTime);
                }
            }
        }
        else
        {
            if (_gameManager.isCoopMode == false)
            {
                transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
                transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
            }
            else
            {

                // if hit 8
                // move up
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.up * _speed * Time.deltaTime);
                }

                // if hit 6
                // move right
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * _speed * Time.deltaTime);
                }

                // if hit 2
                // move down
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.down * _speed * Time.deltaTime);
                }

                // if hit 4
                // move left
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * _speed * Time.deltaTime);
                }
            }
        }

        //if player on the y is greater than 0
        //set player position on the Y to 0

        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector3(transform.position.x, 4.5f, 0);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }

        //if player position on the x is greater than 7
        //position on the x needs to be -7

        if (transform.position.x > 7f)
        {
            transform.position = new Vector3(-7f, transform.position.y, 0);
        }
        else if (transform.position.x < -7f)
        {
            transform.position = new Vector3(7f, transform.position.y, 0);
        }
    }

    // Player two Movement method
    private void PlayerTwoMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // if speed boost enabled
        //move 1.5x the normal speed
        //else
        //move normal speed
        if (isSpeedBoostActive == true)
        {
            // if hit 8
            // move up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * _speed * 1.5f * Time.deltaTime);
            }

            // if hit 6
            // move right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * _speed * 1.5f * Time.deltaTime);
            }

            // if hit 2
            // move down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * _speed * 1.5f * Time.deltaTime);
            }

            // if hit 4
            // move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * _speed * 1.5f * Time.deltaTime);
            }
        }
        else
        {
            // if hit 8
            // move up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            // if hit 6
            // move right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }

            // if hit 2
            // move down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

            // if hit 4
            // move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
        }

        //if player on the y is greater than 0
        //set player position on the Y to 0

        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector3(transform.position.x, 4.5f, 0);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }

        //if player position on the x is greater than 9.5
        //position on the x needs to be -9.5

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        //if player has shields
        //do nothing

        if (shieldsActive == true)
        {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        hitCount++;

        if (hitCount == 1)
        {
            //turn left engine_failure on
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            //turn lef engine_failure on
            _engines[1].SetActive(true);
        }

        //subtract 1 life from the player
        lives--;

        _uiManager.UpdateLives(lives);

        //if lives < 1 (meaning 0)
        //destroy this player
        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            _uiManager.CheckForBestScore();
            Destroy(this.gameObject);
        }
    }

    //method to enable to tiple shot powerup
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //method to enable to speed boost powerup
    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    //method to enable to shields powerup
    public void EnableShields()
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

    //coroutine method (inumerator) to powerDown tiple shot
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    //coroutine method (inumerator) to powerDown the speed boost
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

}
