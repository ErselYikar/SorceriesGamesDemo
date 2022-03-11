using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JsonConverter : CustomBehaviour 
{
    public PlayerData PlayerData;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize (gameManager);

        LoadPlayerData();
    }

    private string LoadResourcesTextFile(string file)
    {
        var sourceText = Resources.Load (file) as TextAsset;
        return sourceText.text;
    }

    private void LoadPlayerData()
    {
        var data = PlayerPrefs.GetString(Constants.PLAYER_DATA);

        if (string.IsNullOrEmpty(data))
        {
            var marketPlayerData = new List<PlayerPurchaseData>();

            var defaultPlayer = new PlayerPurchaseData
            {
                Id = 0,
                PurchaseState = PlayerStates.Selected
            };
            marketPlayerData.Add(defaultPlayer);

            for (int i = 1; i < Constants.TOTAL_PLAYER_COUNT; i++)
            {
                var player = new PlayerPurchaseData
                {
                    Id = i,
                    PurchaseState = PlayerStates.Purchase
                };

                marketPlayerData.Add(player);
            }

            PlayerData = new PlayerData
            {
                Name = "",
                LevelNumber = 1,
                CoinCount = 0,
                SelectedPlayerId = 0,
                SelectedLevelNumber = 0,
                PlayerPurchaseData = marketPlayerData
            };

            SavePlayerData();
        }
        else
        {
            PlayerData = JsonUtility.FromJson<PlayerData>(data);
        }
    }

    public void SavePlayerData()
    {
        var jsonData = JsonUtility.ToJson(PlayerData);
        PlayerPrefs.SetString(Constants.PLAYER_DATA, jsonData);
        PlayerPrefs.Save();
    }
}
