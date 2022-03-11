using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : CustomBehaviour
{
    public Player CurrentPlayer;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        CurrentPlayer.Initialize(gameManager);
        GameManager.OnStartGame += OnStartGame;
        GameManager.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.OnGameFinished += OnGameFinished;     
    }

    #region PLAYER DATA
    public void UpdateCoinCountData(int collectedCoin)
    {
        GameManager.JsonConverter.PlayerData.CoinCount += collectedCoin;
        GameManager.JsonConverter.SavePlayerData();
    }

    public void UpdateLevelData()
    {
        GameManager.JsonConverter.PlayerData.LevelNumber++;
        GameManager.JsonConverter.SavePlayerData();
    }

    public void UpdateNameData(string name)
    {
        GameManager.JsonConverter.PlayerData.Name = name;
        GameManager.JsonConverter.SavePlayerData();
    }

    public void UpdateSelectedPlayerId(int id)
    {
        GameManager.JsonConverter.PlayerData.SelectedPlayerId = id;
        GameManager.JsonConverter.SavePlayerData();
    }

    public void UpdateSelectedLevelNumber(int number)
    {
        GameManager.JsonConverter.PlayerData.SelectedLevelNumber = number;
        GameManager.JsonConverter.SavePlayerData();
    }


    public string GetName()
    {
        var nm = GameManager.JsonConverter.PlayerData.Name;

        if(string.IsNullOrEmpty(nm)){
            return "Player";
        }

        return nm;
    }

    public int GetLevelNumber()
    {
        return GameManager.JsonConverter.PlayerData.LevelNumber;
    }

    public int GetTotalCoinCount()
    {
        return GameManager.JsonConverter.PlayerData.CoinCount;
    }

    public int GetSelectedPlayerId()
    {
        return GameManager.JsonConverter.PlayerData.SelectedPlayerId;
    }

    public int GetSelectedLevelNumber()
    {
        return GameManager.JsonConverter.PlayerData.SelectedLevelNumber;
    }

    public bool CanPurchase(int cost)
    {
        return GameManager.PlayerManager.GetTotalCoinCount() >= cost;
    }

    #endregion

    #region Events

    private void OnStartGame()
    {
    }

    private void OnResetToMainMenu()
    {
    }

    private void OnGameFinished()
    {
    }

	private void OnDestroy()
	{
        if(GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
            GameManager.OnResetToMainMenu -= OnResetToMainMenu;
        }
	}

	#endregion
}
