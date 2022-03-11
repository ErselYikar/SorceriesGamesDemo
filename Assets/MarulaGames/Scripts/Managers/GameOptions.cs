using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOptions : CustomBehaviour
{
    [Header("GAMEPLAY MECHANICS")]
    public GameMechanics GameMechanic;
    public List<GameMechanic> Mechanics;

    [Space(10)]
    [Header("COUNTDOWN")]
    public bool UseCountdown;
    public CountdownController CountdownController;

    [Space(10)]
    [Header("PLAYER NAME OPTIONS")]
    public bool UsePlayerName;
    public TMP_InputField InputFieldName;

    private TMP_InputField mInputFieldName;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        InitializeCustomOptions();

        Mechanics.ForEach(x => x.Deactivate());

        if(GameMechanic != GameMechanics.Other)
        {
            Mechanics[(int)GameMechanic].Initialize(this);
        }
    }

    private void InitializeCustomOptions()
    {
        if (UseCountdown)
        {
            CountdownController.Initialize(GameManager);
        }

        if (UsePlayerName)
        {
            mInputFieldName = Instantiate(InputFieldName, GameManager.UIManager.GetPanel(Panels.MainMenu).Transform);
            ((MainMenuPanel)GameManager.UIManager.GetPanel(Panels.MainMenu)).SetNameInputFieldTo(mInputFieldName);
        }
    }

    public bool CanUseTap()
    {
        switch (GameMechanic)
        {
            case GameMechanics.Tap:
                return true;
            default:
                return false;
        }
    }

    public void SetName()
    {
        if (UsePlayerName)
        {
            var tmpName = InputFieldName.text;

            if (!string.IsNullOrEmpty(tmpName))
            {
                GameManager.PlayerManager.UpdateNameData(tmpName);
            }
        }
    }

    public void GetName()
    {
        if (UsePlayerName)
        {
            var fieldName = GameManager.PlayerManager.GetName();

            if (fieldName == "Player")
            {
                fieldName = null;
            }

            InputFieldName.SetTextWithoutNotify(fieldName);
        }

    }
}
