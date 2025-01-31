using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameControl : MonoBehaviour
{

    private GameObject level;
    public TMP_Text candyText;
    public TMP_Text hs;
    public TMP_Text timeText;

    private int candies;
    private int health = 100;
    private bool isLvl1 = false;

    private float time = 0f;
    private int minutes = 0;
    private int seconds = 0;

    public Sprite full;
    public Sprite eightyFive;
    public Sprite seventy;
    public Sprite fiftyFive;
    public Sprite forty;
    public Sprite twentyFive;
    public Sprite ten;
    public Sprite empty;
    public UnityEngine.UI.Image healthBar;
    public TMP_Text healthPercentage;

    private Canvas actionCanvas;
    private Canvas gameOverCanvas;

    
    private Canvas completed;
    private UnityEngine.UI.Button nextLvl;
    private UnityEngine.UI.Button submit;
    private TMP_InputField nameInput;
    private TMP_Text newRec;
    private TMP_Text saved;
    private TMP_Text err;
    private TMP_Text timeRec;

    private string actionTyp;

    private AudioManager audioManager;

    private GameObject data;
    private string currentRec;

    private string currentTime;
    private string finalTime;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        nextLvl = GameObject.Find("NextLvl").GetComponent<UnityEngine.UI.Button>();
        nameInput = GameObject.Find("NameInput").GetComponent<TMP_InputField>();
        newRec = GameObject.Find("NewRec").GetComponent<TMP_Text>();
        submit = GameObject.Find("Submit").GetComponent<UnityEngine.UI.Button>();
        newRec = GameObject.Find("NewRec").GetComponent<TMP_Text>();
        saved = GameObject.Find("Success").GetComponent<TMP_Text>();
        err = GameObject.Find("Errors").GetComponent<TMP_Text>();
        timeRec = GameObject.Find("Time").GetComponent<TMP_Text>();
        data = GameObject.Find("DataManagement");

        hs.text = "High-Score: ";
        err.text = "";
        saved.enabled = false;
        currentRec = "00m00s";

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            isLvl1 = true;
            level = GameObject.Find("Level 1");
            currentRec = data.GetComponent<ReadWriteData>().GetHighestScore(1);
        }
        else
        {
            level = GameObject.Find("Level 2");
            nextLvl.gameObject.SetActive(false);
            currentRec = data.GetComponent<ReadWriteData>().GetHighestScore(2);
        }

        hs.text += currentRec;

        candyText.text = (isLvl1) ? "00/10" : "00/15";
        candies = 0;
        timeText.text = "Timer: 00m00s";
        



        healthBar.sprite = full;
        healthPercentage.text = "100%";

        actionCanvas = GameObject.Find("ActionCanvas").GetComponent<Canvas>();
        gameOverCanvas = GameObject.Find("GameOver").GetComponent<Canvas>();
        completed = GameObject.Find("Completed").GetComponent<Canvas>();

        actionCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        completed.enabled = false;

        Time.timeScale = 1;


    }

    // Update is called once per frame
    void Update()
    {
        candyText.text = (isLvl1) ? candies + "/10" : candies + "/15";

        time += Time.deltaTime;
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);

        currentTime = minutes.ToString("00") + "m" + seconds.ToString("00") + "s";
        timeText.text = "Timer: " + currentTime;

        // removing the blocking objects from the scene when the player collects the required amount of candies

        if (candies == 10)
        {
            Destroy(GameObject.Find("Closed"));
        }

        if (candies == 15)
        {
            Destroy(GameObject.Find("Toothpaste"));
        }

        // losing the game when the player's health reaches 0
        if (health >= 0)
        {
            healthPercentage.text = health + "%";
        }
        else
        {
            healthPercentage.text = "0%";
            audioManager.PlaySound(audioManager.lose);
            gameOverCanvas.enabled = true;
            Time.timeScale = 0;
        }

        // updating the health bar

        if (health > 85)
        {
            healthBar.sprite = full;
        }
        else if (health > 70)
        {
            healthBar.sprite = eightyFive;
        }
        else if (health > 55)
        {
            healthBar.sprite = seventy;
        }
        else if (health > 40)
        {
            healthBar.sprite = fiftyFive;
        }
        else if (health > 25)
        {
            healthBar.sprite = forty;
        }
        else if (health > 10)
        {
            healthBar.sprite = twentyFive;
        }
        else if (health > 0)
        {
            healthBar.sprite = ten;
        }
        else
        {
            healthBar.sprite = empty;
        }

    }


    // handling the submit button
    public void SubmitClick()
    {

        if (nameInput.text == null || nameInput.text.Trim().Length == 0)
        {
            err.text = "Write your name please!";
        }

        if (!hasOnlyDigitLetters(nameInput.text))
        {
            err.text = "Use only letters or numbers please! No Spaces as well";
        }

        if (nameInput.text.Length > 12)
        {
            err.text = "Name too long";
        }

        if (nameExists(nameInput.text))
        {
            err.text = "Use a different name please!";
        }

        if (nameInput.text != null && nameInput.text.Trim().Length > 0 && hasOnlyDigitLetters(nameInput.text) && !nameExists(nameInput.text) && nameInput.text.Length < 13)
        {
            saved.enabled = true;
            audioManager.PlaySound(audioManager.win);
            err.enabled = false;
            submit.interactable = false;
            WriteInTextFile();
            nameInput.enabled = false;

        }

    }

    // writing the name and time in the text file
    private void WriteInTextFile()
    {
        int level = (isLvl1) ? 1 : 2;
        data.GetComponent<ReadWriteData>().Write(level, nameInput.text + ": " + finalTime);
    }

    // checking if the name already exists on the list
    private bool nameExists(string str)
    {

        int level = (isLvl1) ? 1 : 2;
        if (data.GetComponent<ReadWriteData>().GetNames(level).Contains(str))
        {
            return true;
        }

        return false;

    }

    // checking if the string contains only digits and letters
    private bool hasOnlyDigitLetters(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (!(Char.IsDigit(c) || Char.IsLetter(c)))
            {
                return false;
            }
        }
        return true;
    }

    // adding candy to the player's inventory
    public void AddCandy()
    {
        
        candies++;
    }

    // reducing health from the player
    public void GotHit()
    {
        health -= 15;
    }

    // handling the player's win
    public void LevelCompleted()
    {
        finalTime = currentTime;
        timeRec.text = "Time: " + finalTime;

        if (string.Compare(currentRec, finalTime) > 0)
        {
            newRec.enabled = true;

        }
        else
        {
            newRec.enabled = false;

        }

        completed.enabled = true;
        Time.timeScale = 0;
    }

    // handling exit and restart actions
    public void ExitLevel()
    {

        actionTyp = "exit";
        actionCanvas.enabled = true;

    }

    public void RestartLevel()
    {

        actionTyp = "restart";
        actionCanvas.enabled = true;
    }

    public void GameAction(string status)
    {

        if (actionTyp == "exit")
        {
            if (status == "yes")
            {
                LoadMenu();
            }
            else
            {
                actionCanvas.enabled = false;
            }

        }

        if (actionTyp == "restart")
        {
            if (status == "yes")
            {
                ReloadLevel();

            }
            else
            {
                actionCanvas.enabled = false;
            }
        }
    }


    // loading the menu, reloading the level and loading the next level
    public void LoadMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }



}
