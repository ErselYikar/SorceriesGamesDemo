using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CustomBehaviour
{
    public Player Player;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        InitializePlayer();
    }

    private void InitializePlayer()
    {
        Player.Initialize(GameManager);
    }
}
