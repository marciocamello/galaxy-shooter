using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public bool isCoopMode = false;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject _coopPlayers;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private UIManager _uiManager;

    private SpawnManager _spawnManager;
   
    private Animator _pauseAnimator;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    //if game over is true
    //if space key pressed
    //spwan the player
    //gameOver is false
    //hide title screen

    private void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // if co-op mode is false instatiate the player
                if (isCoopMode == false)
                {
                    Instantiate(player, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    Instantiate(_coopPlayers, Vector3.zero, Quaternion.identity);
                }

                gameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.StartCoroutines();
            }
            else if(Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main_Menu");
            }
        }
        else
        {
            // if p key
            // pause menu
            // enable pause menu
            if (Input.GetKeyDown(KeyCode.P))
            {
                _pauseMenuPanel.SetActive(true);
                _pauseAnimator.SetBool("isPaused", true);
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        _pauseAnimator.SetBool("isPaused", false);
        Time.timeScale = 1;
    }
}
