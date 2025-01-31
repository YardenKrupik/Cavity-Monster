using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource musicSource;
    public AudioSource SFXSource;

    private static float musicVol;
    private static float sFXVol;

    public AudioClip menu;
    public AudioClip lvl1;
    public AudioClip lvl2;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip collect;
    public AudioClip hit;
    public AudioClip hitFluorid;
    public AudioClip shoot;
    public AudioClip tooth;
    public AudioClip killFluoride;
    public AudioClip toothbrush;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip click;
    public AudioClip check;
    private static int index = 0;

    void Start()
    {

        if (SceneManager.GetActiveScene().name == "Level1") musicSource.clip = lvl1;
        else if (SceneManager.GetActiveScene().name == "Level2") musicSource.clip = lvl2;
        else musicSource.clip = menu;



        if (index == 0)
        {
            musicVol = 0.02f;
            sFXVol = 0.04f;
        }

        index++;

        musicSource.volume = musicVol;
        SFXSource.volume = sFXVol;


        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {


    }

    // Play Background Music according to the scene
    public void PlayMusic(string clip)
    {
        switch (clip)
        {
            case "menu":
                musicSource.clip = menu;
                musicSource.Play();
                break;
            case "lvl1":
                musicSource.clip = lvl1;
                musicSource.Play();
                break;
            case "lvl2":
                musicSource.clip = lvl2;
                musicSource.Play();
                break;
        }
    }


    // Play Sound Effects
    public void PlaySound(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Play Sound Effects at a specific point
    public void PlaySoundAtPoint(AudioClip clip, Vector2 location)
    {
        AudioSource.PlayClipAtPoint(clip, location);
    }

    // Set the volume of the music
    public void MusicVolume(float volume)
    {

        musicVol = volume;

        musicSource.volume = musicVol;
    }

    // Set the volume of the sound effects
    public void SFXVolume(float volume)
    {
        sFXVol = volume;
        SFXSource.volume = sFXVol;
    }

    // Get the volume of the music
    public float GetMusicVolume()
    {
        return musicVol;
    }

    // Get the volume of the sound effects
    public float GetSFXVolume() 
    {
        return sFXVol; 
    }


}
