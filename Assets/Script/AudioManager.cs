using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip shopMusic;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioClip loseMusic;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip bouncerSound;
    [SerializeField] private AudioClip ballLaunch;
    [SerializeField] private AudioClip pinBounce;
    [SerializeField] private AudioClip purchase;
    [SerializeField] private AudioClip roundCompleted;
    [SerializeField] private AudioClip ballFalled;

    [Header("Pitch Variation")]
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    //CAMBIAR MUSICA --->   AudioManager.Instance.PlayMusic(MusicTrack.Shop);
    //PLAY SFX --------->   AudioManager.Instance.PlaySFX(SFX.BallBounce);


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        musicSource.loop = true;
        sfxSource.loop = false;
        PlayMusic(MusicTrack.Game);
    }


    // MUSIC

    public void PlayMusic(MusicTrack track)
    {
        AudioClip clip = null;

        switch (track)
        {
            case MusicTrack.Game:
                clip = gameMusic;
                break;

            case MusicTrack.Shop:
                clip = shopMusic;
                break;

            case MusicTrack.Victory:
                clip = victoryMusic;
                break;

            case MusicTrack.Lose:
                clip = loseMusic;
                break;
        }

        if (clip == null)
        {
            Debug.LogWarning("No hay AudioClip asignado para: " + track);
            return;
        }

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }


    public void StopMusic()
    {
        musicSource.Stop();
    }


    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }


    // SFX

    public void PlaySFX(SFX sound)
    {
        AudioClip clip = GetClip(sound);

        if (clip == null)
        {
            Debug.LogWarning("No hay AudioClip asignado para: " + sound);
            return;
        }

        if (sound == SFX.BallBounce)
        {
            sfxSource.pitch = Random.Range(minPitch, maxPitch);
        }
        else
        { 
            sfxSource.pitch = 1f; 
        }
   
        sfxSource.PlayOneShot(clip);
        sfxSource.pitch = 1f;
    }

    private AudioClip GetClip(SFX sound)
    {
        switch (sound)
        {
            case SFX.BallBounce:
                return bouncerSound;

            case SFX.BallLaunch:
                return ballLaunch;

            case SFX.ButtonClick:
                return pinBounce;

            case SFX.Purchase:
                return purchase;

            case SFX.RoundCompleted:
                return roundCompleted;

            case SFX.BallFalled:
                return ballFalled;

            default:
                return null;
        }
    }



    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}


// MUSIC ENUM

public enum MusicTrack
{
    Game,
    Shop,
    Victory,
    Lose
}


// SFX ENUM

public enum SFX
{
    BallBounce,
    BallLaunch,
    ButtonClick,
    Purchase,
    RoundCompleted,
    BallFalled
}