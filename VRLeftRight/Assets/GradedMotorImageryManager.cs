using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GradedMotorImageryManager : MonoBehaviour
{
    public AudioClip meditationAudio;
    public AudioSource source;
    public SessionState sessionState;
    public GameObject[] screens;
    public enum SessionState
    {
        Idle,
        Session
    };

    private bool fogChanging = false;
    private float fogDensityTarget = 1f; // Change this to your desired fog density
    private float fogDensityChangeRate = 0.5f; // Adjust this to control the speed of fog change
    private float fogDensityCurrent = 0f;

    public bool audioPlaying = false;

    private void Start()
    {
        source.clip = meditationAudio;
        sessionState = SessionState.Idle;
        SetScreen(0);
        audioPlaying = false;
    }

    private void Update()
    {
        switch (sessionState)
        {
            case SessionState.Idle:
                SetScreen(0);
                audioPlaying = false;
                source.Stop();
                break;
            case SessionState.Session:
                SetScreen(1);
                if (audioPlaying==false)
                {
                    audioPlaying = true;
                    source.Play();
                }
                if (source.isPlaying==false && audioPlaying == true)
                {
                    //means the audio is over -- switch to idle
                    sessionState = SessionState.Idle;
                }
                
                break;
            default:
                break;
        }

        if (fogChanging && sessionState==SessionState.Idle)
        {
            ChangeFogDensity(true);
        } else if (fogChanging && sessionState!=SessionState.Session)
        {
            ChangeFogDensity(false);
        }
    }

    public void SetScreen(int target)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (i == target)
            {
                screens[i].gameObject.SetActive(true);
            } else
            {
                screens[i].gameObject.SetActive(false);
            }
        }
    }

    public void BeginSession()
    {
        fogChanging = true;
        fogDensityTarget = 1f;
        fogDensityCurrent = 0f;
    }

    private void ChangeFogDensity(bool darken)
    {
        if (darken)
        {
            if (RenderSettings.fogDensity < fogDensityTarget)
            {
                fogDensityCurrent += fogDensityChangeRate * Time.deltaTime;
                RenderSettings.fogDensity = Mathf.Min(fogDensityCurrent, fogDensityTarget);
            }
            else
            {
                fogChanging = false;
                sessionState = SessionState.Session;
            }
        }
        else
        {
            if (RenderSettings.fogDensity > fogDensityTarget)
            {
                fogDensityCurrent -= fogDensityChangeRate * Time.deltaTime;
                RenderSettings.fogDensity = Mathf.Max(fogDensityCurrent, fogDensityTarget);
            }
            else
            {
                fogChanging = false;
                sessionState = SessionState.Idle;
            }
        }
    }

    public void StopSession()
    {
        source.Stop();
        fogChanging = false;
        sessionState=SessionState.Idle;
        fogDensityTarget = 0.01f;
        fogDensityCurrent = 1f;
        RenderSettings.fogDensity = 0f;
    }

    private void DarkenSurroundings()
    {
        // Implement darkening surroundings logic here
    }

    private void LightenSurroundings()
    {
        // Implement lightening surroundings logic here
    }
}
