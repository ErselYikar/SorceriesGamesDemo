using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : CustomBehaviour
{
    public AudioSource ClickAudioSource;
    public AudioClip[] ClickAudioClips;

    [Space(10)]
    public AudioSource GameStateAudioSource;
    public AudioClip[] GameStateAudioClips;

    [Space(10)]
    public AudioSource PlayerInteractionAudioSource;
    public AudioClip[] PlayerInteractionAudioClips;

    public bool IsSoundOn { get; set; }

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);

        InitializeSoundState();
    }

    public void ToggleSound()
    {
        IsSoundOn = !IsSoundOn;
        SaveSoundChangeState();
    }

    public void PlayClickSound(ClickSounds clickSound)
    {
        if (!IsSoundOn) return;

        ClickAudioSource.clip = ClickAudioClips[(int)clickSound];
        ClickAudioSource.Play();
    }

    public void PlayGameStateSound(GameStateSounds snd)
    {
        if (!IsSoundOn) return;
        GameStateAudioSource.clip = GameStateAudioClips[(int)snd];
        GameStateAudioSource.Play();
    }

    public void PlayPlayerInteractionSound(PlayerInteractionSounds snd)
    {
        if (!IsSoundOn) return;
        PlayerInteractionAudioSource.clip = PlayerInteractionAudioClips[(int)snd];
        PlayerInteractionAudioSource.Play();
    }

    public void InitializeSoundState()
    {
        IsSoundOn = PlayerPrefs.GetInt(Constants.SOUND_STATE, 1) == 1;
    }

    public void SaveSoundChangeState()
    {
        PlayerPrefs.SetInt(Constants.SOUND_STATE, IsSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

}