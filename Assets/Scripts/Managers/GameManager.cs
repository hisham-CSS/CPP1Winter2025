using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public event Action<PlayerController> OnPlayerSpawned;
    public UnityEvent<int> OnLifeValueChanged;

    #region GAME PROPERTIES
    [SerializeField] private int maxLives = 10;
    private int _lives = 5;
    public int lives
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                GameOver();
                return;
            }
            
            if (_lives > value) Respawn();

            _lives = value;

            if (_lives > maxLives) _lives = maxLives;

            OnLifeValueChanged?.Invoke(_lives);

            Debug.Log($"{gameObject.name} lives has changed to {_lives}");
        }
    }
    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            Debug.Log($"{gameObject.name} score has changed to {_score}");
        }
    }
    #endregion
    #region PLAYER CONTROLLER INFO
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    #endregion

    private Transform currentCheckpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        if (maxLives <= 0) maxLives = 5;
        _lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = (SceneManager.GetActiveScene().name.Contains("Level")) ? "TitleScreen" : "Level";
            SceneManager.LoadScene(sceneName);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            score++;
    }

    void GameOver()
    {
        Debug.Log("Game Over goes here");
    }

    void Respawn()
    {
        //TODO: ANIMATION BEFORE POSITION CHANGE
        _playerInstance.transform.position = currentCheckpoint.position;
    }

    public void InstantiatePlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;        
        OnPlayerSpawned?.Invoke(_playerInstance);
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
        Debug.Log("Checkpoint updated");
    }    
}
