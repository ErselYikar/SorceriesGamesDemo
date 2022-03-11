using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapOnTime : GameMechanic
{
    public TapOnTimeTypes Type;

    [Header("3D Options")]
    public Target Target3D;
    public Vector3 TargetPosition;

    [Header("2D Options")]
    public TapOnTime2DAlignments Alignment;

    public List<TapOnTimeController> TapOnTimeControllerList;

    private Target mCurrentTarget;
    private TapOnTimeController mCurrentTapOnTimeController;

    public override void Initialize(GameOptions gameOptions)
    {
        base.Initialize(gameOptions);

        InitializeCustomOptions();

        GameManager.InputManager.OnTouched += OnTouched;
    }

    public override void InitializeCustomOptions()
    {
        base.InitializeCustomOptions();

        Deactivate();

        if (Type == TapOnTimeTypes.Type2D)
        {
            mCurrentTapOnTimeController = Instantiate(GetTapOnTime(), GameManager.UIManager.GetPanel(Panels.Hud).Transform);
            //GetTapOnTime().gameObject.SetActive(true);
        }
        else
        {
            //Target3D.gameObject.SetActive(true);
            mCurrentTarget = Instantiate(Target3D, TargetPosition, Quaternion.identity);
        }
    }

	public override void Deactivate()
	{
		base.Deactivate();

        //TapOnTimeControllerList.ForEach(x => x.gameObject.SetActive(false));
        //TapOnTimeControllerList.ForEach(x => x.gameObject.SetActive(false));
        //Target3D.gameObject.SetActive(false);
        //mCurrentTapOnTimeController.gameObject.SetActive(false);
        //mCurrentTarget.gameObject.SetActive(false);
	}

	public TapOnTimeController GetTapOnTime()
    {
        return TapOnTimeControllerList[(int)Alignment];
    }

    #region Events

    private void OnTouched()
    {
        if(Type == TapOnTimeTypes.Type2D)
        {
            //GetTapOnTime().Clicked();
            mCurrentTapOnTimeController.Clicked();
        }
        else
        {
            //Target3D.Clicked();
            mCurrentTarget.Clicked();
        }
    }

	private void OnDestroy()
	{
        if(GameManager != null)
        {
            if(GameManager.InputManager != null)
            {
                GameManager.InputManager.OnTouched -= OnTouched;
            }
        }
	}

	#endregion
}
