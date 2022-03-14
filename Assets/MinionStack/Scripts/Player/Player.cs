using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Player : CustomBehaviour
{
    private bool mGameStarted;
    public float JumpHeight;
    public Vector3 JumpPos;
    public bool IsControllerCharacter;
    public PlayerMovementVariables PlayerMovementVariable;
    private Vector3 mMoveSpeed;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        GameManager.OnStartGame += OnStartGame;

        InitializeWithCustomOptions();
    }
    private void InitializeWithCustomOptions()
    {
        if(GameManager.PlayerManager.CurrentPlayer == this)
        {
            IsControllerCharacter = true;
        }
        else
        {
            IsControllerCharacter = false;
        }
        if (IsControllerCharacter)
        {
            transform.position = Vector3.zero;
        }
        mMoveSpeed = PlayerMovementVariable.MovementSpeed;
    }

    private void Update()
    {
        if (mGameStarted && IsControllerCharacter)
        {
            PlayerMovement();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag(TAGS.Player))
        {
            IsControllerCharacter = false;
            hit.gameObject.GetComponent<Player>().IsControllerCharacter = true;
        }
    }

    private void PlayerMovement()
    {
        CharacterController.Move(mMoveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            transform.DOJump(JumpPos, JumpHeight, 1, 0.5f, false);
        }
    }

    private void OnStartGame()
    {
        mGameStarted = true;
    }

    private void OnEndGame()
    {
        mGameStarted = false;
    }

    private void OnDestroy()
    {
        if(GameManager != null)
        {
            GameManager.OnStartGame -= OnStartGame;
        }
    }
}
