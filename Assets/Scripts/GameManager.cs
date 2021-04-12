using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Declaration of this class as a singleton
    private static GameManager _instance;

    public static GameManager Instance
    {

        get
        {
            if(_instance == null)
            {
                GameObject gameObject = new GameObject("GameManager");
                gameObject.AddComponent<GameManager>();
                return _instance;
            }
            
            return _instance;
            
        }
    }
    #endregion

    #region Prefab objects
    public GameObject Ball;
    private GameObject _ballInstantiated;
    public GameObject Paddle;
    private GameObject _paddleInstantiate;
    public GameObject[] Levels;
    private GameObject _currentLevelInstantiate;
    #endregion



    public Text BallsTextBox;
    public Text ScoreTextBox;
    public Text LevelTextBox;

    public Text StartGameTextBox;
    public Text GameCompleteTextBox;
    public Text GameOverTextBox;
    public Button StartGameButton;


    private int _balls;
    private int _score;
    private int _level;

    public int GetBalls()
    { return _balls; }
    public void SetBalls(int balls)
    { _balls = balls; }

    public int GetScore()
    { return _score; }
    public void SetScore(int value)
    { _score = value; }
    
    public int GetLevel()
    { return _level; }
    public void SetLevel(int value)
    { _level = value; }


    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }


    private const string _ballsLabel = "Balls: ";
    private const string _scoreLabel = "Score: ";
    private const string _levelLabel = "Level: ";

    
    public enum States
    {
        Initialization,
        StartGame,
        GameInProgress,
        GameComplete,
        GameOver,
        Pause
    }

    private States _currentState;

    private void Awake()
    {
        _instance = this;
        _currentState = States.Initialization;
    }


    // Start is called before the first frame update
    void Start()
    {
        
        SetStatistics();
    }

    private void Initialize()
    {
        _balls = 3;
        _level = 0;
        _score = 0;
        StartGameTextBox.gameObject.SetActive(true);
        StartGameButton.gameObject.SetActive(true);
        StartGameButton.interactable = true;
       
        
    }

    public void  FloorHit()
    {
        _balls -= 1;
        if(_balls < 0)
        {
            SetState(States.GameOver);
        }
        else
        {
            SetState(States.StartGame);
        }

        Destroy(_ballInstantiated);
        Destroy(_paddleInstantiate);
    }

    private void SetStatistics()
    {
        StringBuilder sb = new StringBuilder(_ballsLabel);
        sb.Append(_balls);
        BallsTextBox.text = sb.ToString();

        sb.Clear();
        sb.Append(_scoreLabel);
        sb.Append(_score);
        ScoreTextBox.text = sb.ToString();


        sb.Clear();
        sb.Append(_levelLabel);
        sb.Append(_level);
        LevelTextBox.text = sb.ToString();


    }

    private bool IsGameComplete()
    {

        int childNum = _currentLevelInstantiate.transform.childCount;
        return childNum <= 0; 
    }

    // Update is called once per frame
    void Update()
    {
        SetStatistics();
        if(_currentState == States.Initialization)
        {

            Initialize();

        }
        else if(_currentState == States.StartGame)
        {
            GameCompleteTextBox.gameObject.SetActive(false);
            InstantiateGameStartObject();
            SetState(States.GameInProgress);

        }
        else if (_currentState == States.GameInProgress)
        {
            SetStatistics();
            if(IsGameComplete())
            {
                SetState(States.GameComplete);
            }
        }
        else if(_currentState == States.GameOver)
        {
            GameOverTextBox.gameObject.SetActive(true);
        }
        else if(_currentState == States.GameComplete)
        {
            GameCompleteTextBox.gameObject.SetActive(true);
            Debug.LogWarning("Starting new level");
            StartCoroutine(StartNewLevel());
            Debug.LogWarning("Done Starting new level");
        }

    }

    private void InstantiateGameStartObject()
    {

        _ballInstantiated =  Instantiate(Ball);
        _paddleInstantiate = Instantiate(Paddle);
    }

    private void InstantiateBrickLevel()
    {

        _currentLevelInstantiate = Instantiate(Levels[_level]);
    }

    public void OnStartButtonClick()
    {
        StartGameTextBox.gameObject.SetActive(false);
        StartGameButton.gameObject.SetActive(false);
        StartGameButton.interactable = false;

        InstantiateBrickLevel();

        _balls -= 1;
        SetState(States.StartGame);

    }

    public void SetState(States newState)
    {
        _currentState = newState;
    }

    private IEnumerator StartNewLevel()
    {
        Debug.LogWarning("Starting new level");
        SetState(States.Pause);
        Destroy(_ballInstantiated);
        Destroy(_paddleInstantiate);
        Destroy(_currentLevelInstantiate);
        yield return new WaitForSeconds(3.0f);

        _level += 1;
        SetState(States.StartGame);
        InstantiateBrickLevel();
    }
}
