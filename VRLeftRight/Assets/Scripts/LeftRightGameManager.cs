using System.Collections;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System;
using System.Collections.Generic;
using Concentus.Enums;
using UnityEngine.UI;

[Serializable]
public class PlayerGuessData {
    public string answer;
    public string playerGuess;
    public Vector2 animatorState;
    public float guessTime;

    public PlayerGuessData(string _answer, string _playerGuess,Vector2 _animatorState, float _guessTime) {
        answer = _answer;
        playerGuess = _playerGuess;
        animatorState = _animatorState;
        guessTime = _guessTime;
    }
}

public class LeftRightGameManager : MonoBehaviour
{
    public enum GameState {
        Idle,
        Calibration,
        Survey,
        Guess,
        Load,
        CalculateScore,
        EndScreen
    };

    //=======================Hands=============================
    public GameObject leftHand;
    public GameObject rightHand;
    private string handsShown = "True";//true or false    
    
    
    //=======================Menu=============================


    public TMP_Text countDownTextGame;
    public TMP_Text scoreText;

    public GameObject[] menus;

    //=======================Calibration Variables======================

    int total = 5;
    int pressed = 0;
    public TMP_Text calibrationTextCounter;
    public GameObject[] calButtons;

    //=======================Game Variables=============================

    public LeftRightGenerator leftRightGenerator;
    public GameObject leftButton;
    public GameObject rightButton;
    string answer = "";

    public int rounds = 10;
    public int roundCounter = 0;
    private int correctGuesses = 0;
    public bool loading = false;
    float currentGuessTime = 0f;
    public bool canGuess = false;

    //====================Player Data Collection=====================
    List<float> guessTimes;
    List<string[]>playerSessionData;
    string uniqueID;
    public string handedness;//true of false    

    public CSVWriter csvWriter;
    public GameState gameState;

    void Start()
    {
        loading = false;
        // Set initial game state
        guessTimes = new List<float>();
        playerSessionData= new List<string[]>();

        correctGuesses = 0;

        gameState=GameState.Idle;

        ShowHands();
    }

    public void SetScreenActive(GameObject[] screens, int index)  {
        for (int i = 0; i < screens.Length; i++) {
            if (i == index) {
                screens[i].SetActive(true);
            } else {
                screens[i].SetActive(false);
            }
        }
    }

    public void ShowHands() {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        handsShown="True";
    }
    public void HideHands() {
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        handsShown="False";
    }

    void Update()
    {
        
        HandleGameState();
    }
   
   public void HandleGameState() {
        switch (gameState)
        {
            case GameState.Idle:
                Debug.Log("The game is in Idle State");
                //show idle screen begin button
                SetScreenActive(menus,0);
                ResetCalibration();
            break;
            case GameState.Calibration:
                Debug.Log("The game is in Calibration State");
                //show calibration screen begin button
                SetScreenActive(menus,1);
                
            break;
            case GameState.Survey:
                Debug.Log("The game is in Survey State");
                //show calibration screen begin button
                SetScreenActive(menus,4);
                
            break;
            case GameState.Load :
            Debug.Log("The game is in the load state.");
                SetScreenActive(menus,2);
                leftButton.SetActive(false);
                rightButton.SetActive(false);
                if (loading) {
                    return;
                }
                if (roundCounter <= rounds) {    
                    loading = true;
                    StartCoroutine(StartCountdown());
                } else {
                    gameState=GameState.CalculateScore;
                }
                
                break;
            case GameState.Guess :
                leftButton.SetActive(true);
                rightButton.SetActive(true);
                Debug.Log("The game is in the game state.");
                currentGuessTime+=Time.deltaTime;
                break;

            case GameState.CalculateScore:
                Debug.Log("The game is in the calculate scrore state.");
                //game logic:
                SetScreenActive(menus,3);
                CalculateScore();
                loading = false;
                gameState=GameState.EndScreen;
                break;
            case GameState.EndScreen:
                Debug.Log("The game is in the endscreen state.");
                //game logic:
                CalculateScore();
                loading = false;
                break;
        }
    }

    public void CalibrateButtonClick(GameObject obj) {
        // calButtons[buttonIndex].SetActive(false);
        obj.SetActive(false);
        Debug.Log("Hiding button " + obj.name);
        pressed+=1;
        calibrationTextCounter.text=pressed.ToString() + "/5";
        if (pressed >= total) {
            //proceed
            rounds=10;
            roundCounter=1;
            correctGuesses = 0;
            loading=false;
            gameState = GameState.Survey;
        }
    }

    public void EnterSurveyQuestion(string ans) {

        handedness=ans;
        rounds=10;
        roundCounter=1;
        correctGuesses = 0;
        loading=false;
        gameState = GameState.Load;
    }

    

    public void CalculateScore()
    {
        //SaveData();

        float finalScore = (float)correctGuesses / (float)rounds;
        float sum = 0;
        foreach (float time in guessTimes)
        {
            sum += time;
        }
        float average = sum / (float)rounds;

        // Format finalScore as a percentage
        string formattedFinalScore = (finalScore * 100f).ToString("F2") + "%";
        // Format average with two decimal places
        string formattedAverage = average.ToString("F2");

        scoreText.text = "Session Stats:\n" +
                         "Accuracy: " + formattedFinalScore + "\n" +
                         "Average Answer Time: " + formattedAverage + "\n" +
                         "Nice Work!";

    }


    

    public void BeginSession() {
        guessTimes = new List<float>();
        ResetCalibration();
        gameState=GameState.Calibration;
        uniqueID = System.Guid.NewGuid().ToString();
        //mainmenu.getHandsShown
    }

    public void RestartSession() {
        ReplayResetGameVariables();
        guessTimes = new List<float>();
        ResetCalibration();
        gameState= GameState.Idle;
    }

    public void ResetCalibration() {
        foreach (GameObject but in calButtons)    {
            but.SetActive(true);
        }
        pressed=0;
        calibrationTextCounter.text="0/5";
    }

    IEnumerator StartCountdown()
    {
        
        countDownTextGame.text = "Guess in: 3";
        yield return new WaitForSeconds(1f);
        countDownTextGame.text = "Guess in: 2";
        yield return new WaitForSeconds(1f);
        countDownTextGame.text = "Guess in: 1";
        yield return new WaitForSeconds(1f);
        Debug.Log("Countdown complete!");
        answer = leftRightGenerator.GenerateRandomDirection();
        countDownTextGame.text = "Guess!";
        canGuess = true;
        gameState = GameState.Guess;
        guessTimes.Add(currentGuessTime);
        currentGuessTime = 0f;
    }

    public void CompareGuess(string _guess) {
        if (_guess.Equals(answer)) {
            Debug.Log("Correct Guess");
            correctGuesses+=1;
        } else {
            Debug.Log("Incorrect Guess");
        }
        
        gameState = GameState.Load;
        loading = false;
        leftRightGenerator.HideHands();
        string timeStamp = Time.time.ToString();
        
        string[] tempData = {timeStamp,
                            uniqueID,
                            handsShown,
                            handedness,
                            roundCounter.ToString(),
                            _guess,
                            answer,
                            currentGuessTime.ToString()};
        roundCounter+=1;
        playerSessionData.Add(tempData);
        SaveData(tempData);
        
    }

    public void SaveData(string[] data)
    {
        StartCoroutine(SaveDataCoroutine(data));
    }

    private IEnumerator SaveDataCoroutine(string[] data)
    {
        csvWriter.AppendCSV(data);
        yield return null;  

        Debug.Log("Data saved.");
    }

    public void ReplayResetGameVariables() {
        rounds=10;
        roundCounter=1;
        correctGuesses = 0;
        loading=false;
        gameState = GameState.Load;
    }


    //===========================Left Right Buttons ==========================

    public void SelectLeft() {
        if (canGuess) {
            Debug.Log("SELECT LEFT");
            canGuess = false;
            CompareGuess("Left");
        } 
    }

    public void SelectRight() {
        if (canGuess) {
            canGuess = false;
            Debug.Log("SELECT RIGHT");
            CompareGuess("Right");
        } 
    }
}
