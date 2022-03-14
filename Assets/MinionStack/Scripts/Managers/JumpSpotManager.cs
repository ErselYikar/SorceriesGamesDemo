using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpotManager : CustomBehaviour
{
    public List<JumpSpot> JumpSpotList;
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        InitializeJumpSpots();
    }

    private void InitializeJumpSpots()
    {
        JumpSpotList.ForEach(x => x.Initialize(GameManager));
    }
}
