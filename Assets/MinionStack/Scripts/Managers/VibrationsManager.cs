using MoreMountains.NiceVibrations;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VibrationsManager : CustomBehaviour
{
    public bool IsVibrationOn { get; set; } = true;

    private Coroutine mPowerOffRoutine;
    private float mVibrationDelay = 0.45f;
    private int mVibrationRepeatRate = 3;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        InitializeVibrationState();
    }

    public void InitializeVibrationState()
    {
        IsVibrationOn = PlayerPrefs.GetInt(Constants.VIBRATION_STATE, 1) == 1;
    }

    public void SaveVibrationsChangeState()
    {
        PlayerPrefs.SetInt(Constants.VIBRATION_STATE, IsVibrationOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleVibration()
    {
        IsVibrationOn = !IsVibrationOn;
        SaveVibrationsChangeState();
    }

    #region Default Vibrations

    public void Vibrate()
    {
        if (IsVibrationOn)
        {
            Handheld.Vibrate();
        }
    }

    public void LongVibrate()
    {
        if (IsVibrationOn)
        {
            StopCoroutine("LongVibration");
            StartCoroutine(LongVibration());
        }
    }

    private IEnumerator LongVibration()
    {
        for (int i = 0; i < mVibrationRepeatRate; i++)
        {
            Handheld.Vibrate();
            yield return new WaitForSeconds(mVibrationDelay);
        }
    }
    #endregion

    #region Nice Vibrations
    public void PlayVibration(HapticTypes hapticType)
    {
        if (!IsVibrationOn) return;
        MMVibrationManager.Haptic(hapticType);
    }

    public void PlayPowerOff()
    {
        if (IsVibrationOn)
        {
            if (mPowerOffRoutine != null)
            {
                StopCoroutine(mPowerOffRoutine);
            }

            mPowerOffRoutine = StartCoroutine(PowerOffRoutine());
        }
    }

    private IEnumerator PowerOffRoutine()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure);
        yield return new WaitForSecondsRealtime(mVibrationDelay);
        MMVibrationManager.Haptic(HapticTypes.Warning);
    }

    #endregion

}
