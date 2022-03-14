using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public GameOptions GameOptions;
    public UIManager UIManager;
    public JsonConverter JsonConverter;
    public SoundManager SoundManager;
    public InputManager InputManager;
    public PlayerManager PlayerManager;
    public LevelManager LevelManager;
    public VibrationsManager VibrationsManager;
    public RewardManager RewardManager;
    public CameraManager CameraManager;
    public JumpSpotManager JumpSpotManager;
    public Confetti Confetti;
    public RemoteConfigManager RemoteConfigManager;

    public event Action OnStartGame;
    public event Action OnCountdownFinished;
    public event Action OnGameFinished;
    public event Action OnRestartGame;
    public event Action OnResumeGame;
    public event Action OnResetToMainMenu;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    public event Action<float> OnTargetPressed;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //PlayerPrefs.DeleteAll();
        RemoteConfigManager.Initialize(this);
        GameOptions.Initialize(this);
        JsonConverter.Initialize(this);
        InputManager.Initialize(this);
        SoundManager.Initialize(this);
        PlayerManager.Initialize(this);
        LevelManager.Initialize(this);
        VibrationsManager.Initialize(this);
        RewardManager.Initialize(this);
        CameraManager.Initialize(this);
        JumpSpotManager.Initialize(this);
        UIManager.Initialize(this);
        Confetti.Initialize(this);  
    }

    public void StartGame()
    {
        if (OnStartGame != null)
        {
            OnStartGame();
        }
    }

    public void CountdownFinished()
    {
        if (OnCountdownFinished != null)
        {
            OnCountdownFinished();
        }
    }

    public void GameFinished()
    {
        if (OnGameFinished != null)
        {
            OnGameFinished();
        }
    }

    public void ResetToMainMenu()
    {
        if (OnResetToMainMenu != null)
        {
            OnResetToMainMenu();
        }
    }

    public void RestartGame()
    {
        if (OnRestartGame != null)
        {
            OnRestartGame();
        }
    }

    public void ResumeGame()
    {
        if (OnResumeGame != null)
        {
            OnResumeGame();
        }
    }

    public void LevelCompleted()
    {
        if (OnLevelCompleted != null)
        {
            OnLevelCompleted();
        }
    }

    public void LevelFailed()
    {
        if (OnLevelFailed != null)
        {
            OnLevelFailed();
        }
    }

    public void TargetPressed(float Multiplier)
    {
        if (OnTargetPressed != null)
        {
            OnTargetPressed(Multiplier);
        }
    }
}