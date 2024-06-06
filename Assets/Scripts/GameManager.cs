using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private RawImage gameOverImage;
    [SerializeField] private Button retryButton;


    private bool playerActive = false;
    private bool gameOver = false;
    private bool gameStarted = false;


    public bool PlayerActive => playerActive;
    public bool GameOver => gameOver;
    public bool GameStarted => gameStarted;
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        gameOverImage.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);


    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerCollided()
    {
        gameOver = true;
        ShowGameOverScreen();
    }

    public void PlayerStartedGame()
    {
        playerActive = true;

    }

    public void EnterGame()
    {

        mainMenu.SetActive(false);
        GameOverScreen.SetActive(false);
        gameStarted = true;


    }

    public void ShowGameOverScreen()
    {
        if (gameOver && GameOverScreen != null) // add null check
        {
            GameOverScreen.SetActive(true);

            // Disable other cameras
            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (Camera camera in cameras)
            {
                if (camera != GameOverScreen.GetComponentInChildren<Camera>())
                {
                    camera.enabled = false;
                }
            }

            gameOverImage.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
    }
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            gameOverImage.gameObject.SetActive(false);
        }
    }
    public void RetryGame()
    {

        mainMenu.SetActive(false);
        GameOverScreen.SetActive(false);

        // UnityEngine.SceneManagement.SceneManager.LoadScene("ZombieGame");
        SceneManager.LoadScene("ZombieGame", LoadSceneMode.Single);

        gameOver = false;
        gameStarted = true;
        playerActive = true;


    }
}
