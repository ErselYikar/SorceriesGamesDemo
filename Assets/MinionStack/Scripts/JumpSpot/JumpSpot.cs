using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpot : CustomBehaviour
{
    public Collider HighestCharacter;
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(TAGS.Player))
        {
            GameManager.PlayerManager.CurrentPlayer.JumpHeight = HighestCharacter.bounds.center.y +
                HighestCharacter.bounds.extents.y;
            GameManager.PlayerManager.CurrentPlayer.JumpPos = new Vector3(HighestCharacter.transform.position.x, GameManager.PlayerManager.CurrentPlayer.JumpHeight, HighestCharacter.transform.position.z);
            Debug.Log(GameManager.PlayerManager.CurrentPlayer.JumpHeight);
        }
    }
}
