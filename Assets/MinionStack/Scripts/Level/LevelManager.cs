using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : CustomBehaviour
{
    public int CurrentLevelNumber;

    public List<Level> Levels;
    
    public int ActivatedLevelNumber;

    private bool mCurrentLevelPassed = false;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        CurrentLevelNumber = GameManager.PlayerManager.GetLevelNumber();
        ActivatedLevelNumber = CurrentLevelNumber;
        Levels.ForEach(x => x.Initialize(gameManager));
        ActivateCurrentLevel();

        GameManager.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.OnLevelCompleted += OnLevelCompleted;
        GameManager.OnLevelFailed += OnLevelFailed;
    }

    private void ActivateCurrentLevel()
    {
        CurrentLevelNumber = GameManager.PlayerManager.GetLevelNumber();

        if (IsLastLevelPlayed())
        {
            if (mCurrentLevelPassed)
            {
                ActivatedLevelNumber = SelectRandomLevel();
                mCurrentLevelPassed = false;
            }
            else
            {
                ActivatedLevelNumber = GameManager.PlayerManager.GetSelectedLevelNumber();
            }
        }
        else
        {
            ActivatedLevelNumber = CurrentLevelNumber - 1;
        }

        Levels[ActivatedLevelNumber].Activate();
    }

    public Level GetActivatedLevel()
    {
        return Levels[ActivatedLevelNumber];
    }

    public bool IsLastLevelPlayed()
    {
        return Levels.Count <= CurrentLevelNumber - 1;
    }

    private int SelectRandomLevel()
    {
        //var rndLevelId = (int)Random.Range(Levels.Count - (Levels.Count - 5), Levels.Count);
        var rndLevelId = (int)Random.Range(0, Levels.Count);
        GameManager.PlayerManager.UpdateSelectedLevelNumber(rndLevelId);
        return rndLevelId;
    }

    #region Events

    private void OnResetToMainMenu()
    {
        ActivateCurrentLevel();
    }

    private void OnLevelCompleted()
    {
        mCurrentLevelPassed = true;
        GameManager.PlayerManager.UpdateLevelData();
    }

    private void OnLevelFailed()
    {
        mCurrentLevelPassed = false;
    }

    private void OnDestroy()
	{
        if(GameManager != null)
        {
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
            GameManager.OnLevelCompleted -= OnLevelCompleted;
            GameManager.OnLevelFailed -= OnLevelFailed;
        }
	}

	#endregion
}
