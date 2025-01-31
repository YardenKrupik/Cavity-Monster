using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro.EditorUtilities;

public class MainMenu : MonoBehaviour
{
    private int levelToLoad;
    private Canvas mainMenuCanvas;
    private Canvas hSCanvas;
    private Canvas moreCanvas;
    private Canvas instruCanvas;
    private Canvas audioCanvas;

    public Slider musicSlider;
    public Slider SFXSlider;

    private AudioManager audioManager;

    private TMP_Text topLevel1;
    private TMP_Text topLevel2;
    private GameObject data;

    private static int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        musicSlider.value = audioManager.GetMusicVolume();
        SFXSlider.value = audioManager.GetSFXVolume();

        mainMenuCanvas = GameObject.Find("MenuCanvas").GetComponent<Canvas>();
        hSCanvas = GameObject.Find("HSCanvas").GetComponent<Canvas>();
        moreCanvas = GameObject.Find("MoreCanvas").GetComponent<Canvas>();
        instruCanvas = GameObject.Find("InstructionsCanvas").GetComponent<Canvas>();
        audioCanvas = GameObject.Find("AudioCanvas").GetComponent<Canvas>();

        topLevel1 = GameObject.Find("Level1List").GetComponent<TMP_Text>();
        topLevel2 = GameObject.Find("Level2List").GetComponent<TMP_Text>();
        data = GameObject.Find("DataManagement");

        mainMenuCanvas.enabled = true;
        mainMenuCanvas.gameObject.SetActive(true);
        hSCanvas.enabled = false;
        hSCanvas.gameObject.SetActive(false);
        moreCanvas.enabled = false;
        moreCanvas.gameObject.SetActive(false);
        instruCanvas.enabled = false;
        instruCanvas.gameObject.SetActive(false);
        audioCanvas.enabled = false;
        audioCanvas.gameObject.SetActive(false);

        musicSlider.value = audioManager.GetMusicVolume();
        SFXSlider.value = audioManager.GetSFXVolume();


        // Set the volume of the music and sound effects for the firt time
        if (index == 0)
        {
            musicSlider.value = 0.02f;
            SFXSlider.value = 0.04f;
            SetMusicVolume(0.02f);
            SetSFXVolumeQuiet(0.04f);
        }

        index++;


    }

    // Update is called once per frame
    void Update()
    {

        if (musicSlider.value != audioManager.GetMusicVolume())
            SetMusicVolume(musicSlider.value);

        if (SFXSlider.value != audioManager.GetSFXVolume())
            SetSFXVolume(SFXSlider.value);



    }

    // Set the volume of the music
    private void SetMusicVolume(float volume)
    {
        audioManager.MusicVolume(volume);

    }

    // Set the volume of the sound effects
    private void SetSFXVolume(float volume)
    {
        audioManager.SFXVolume(volume);
        audioManager.PlaySound(audioManager.check);
    }

    // Set the volume of the sound effects but with no sound check
    private void SetSFXVolumeQuiet(float volume)
    {
        audioManager.SFXVolume(volume);
    }

    // Load the level
    public void LoadLevel(int level)
    {
        audioManager.PlaySound(audioManager.click);
        levelToLoad = level;
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }

    // Show the high scores lists
    public void ShowHighScores()
    {
        audioManager.PlaySound(audioManager.click);
        topLevel1.text = data.GetComponent<ReadWriteData>().GetRecordList(1);
        topLevel2.text = data.GetComponent<ReadWriteData>().GetRecordList(2);
        mainMenuCanvas.enabled = false;
        mainMenuCanvas.gameObject.SetActive(false);
        hSCanvas.enabled = true;
        hSCanvas.gameObject.SetActive(true);
    }

    // Shows more options
    public void ShowMore()
    {
        audioManager.PlaySound(audioManager.click);
        mainMenuCanvas.enabled = false;
        mainMenuCanvas.gameObject.SetActive(false);
        moreCanvas.enabled = true;
        moreCanvas.gameObject.SetActive(true);
        instruCanvas.enabled = false;
        instruCanvas.gameObject.SetActive(false);
        audioCanvas.enabled = false;
        audioCanvas.gameObject.SetActive(false);
    }

    // Shows the instructions
    public void ShowInstructions()
    {
        audioManager.PlaySound(audioManager.click);
        moreCanvas.enabled = false;
        moreCanvas.gameObject.SetActive(false);
        instruCanvas.enabled = true;
        instruCanvas.gameObject.SetActive(true);
    }

    // Shows the audio settings
    public void ShowAudio()
    {
        audioManager.PlaySound(audioManager.click);
        moreCanvas.enabled = false;
        moreCanvas.gameObject.SetActive(false);
        audioCanvas.enabled = true;
        audioCanvas.gameObject.SetActive(true);
    }

    // Shows the main menu
    public void ShowMainMenu()
    {
        audioManager.PlaySound(audioManager.click);
        mainMenuCanvas.enabled = true;
        mainMenuCanvas.gameObject.SetActive(true);
        hSCanvas.enabled = false;
        hSCanvas.gameObject.SetActive(false);
        instruCanvas.enabled = false;
        instruCanvas.gameObject.SetActive(false);
        moreCanvas.enabled = false;
        moreCanvas.gameObject.SetActive(false);
    }

    // Quit the game
    public void QuitGame()
    {
        audioManager.PlaySound(audioManager.click);
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
