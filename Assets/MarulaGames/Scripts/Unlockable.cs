using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : CustomBehaviour
{

    public UnlockData UnlockData;
    public string UID;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        LoadData();
    }

    public virtual void SaveData()
    {
        PlayerPrefs.SetString(UID, JsonUtility.ToJson(UnlockData));
    }

    public virtual void LoadData()
    {
        if (PlayerPrefs.HasKey(UID))
        {
            UnlockData = JsonUtility.FromJson<UnlockData>(PlayerPrefs.GetString(UID));
        }
    }

    public void CreateGUID()
    {
        UID = System.Guid.NewGuid().ToString();
    }
}

[System.Serializable]
public class UnlockData
{
    public DanceSpotStates LockState;
    public TutorAreaStates TutorLockState;
    public DanceVideoSpotStates DanceVideoSpotState;
    public int Price = 0;
}
