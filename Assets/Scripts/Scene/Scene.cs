using UnityEngine;
using System;
using System.Collections.Generic;

public enum CameraModeId
{
	DefaultView,
	Locked, 
	RotateZoom
}

public class Scene
{
	#region Public data

	public Transform CameraTarget
	{
		get
		{
			return mCameraTarget;
		}
		set
		{
			CamLookAt s1 = Camera.main.GetComponent<CamLookAt>();
			TBDragOrbitMod s2 = Camera.main.GetComponent<TBDragOrbitMod>();
			s1.target = value;
			s2.SetTargetSmooth(value);
			mCameraTarget = value;
		}
	}

	public CameraModeId CameraMode
	{
		get
		{
			return mCameraMode;
		}
		set
		{
			if (mCameraMode != value)
			{
				Camera c = Camera.main;
				CamLookAt s1 = c.GetComponent<CamLookAt>();
				TBDragOrbitMod s2 = c.GetComponent<TBDragOrbitMod>();

				if (value == CameraModeId.DefaultView)
				{
					s1.enabled = false;
					s2.enabled = false;

					c.transform.position = mCameraPos_Default;
					c.transform.rotation = mCameraRotation_Default;
				}
				else if (value == CameraModeId.Locked)
				{
					s1.enabled = false;
					s2.enabled = false;
				}
				else if (value == CameraModeId.RotateZoom)
				{
					s1.enabled = false;
					s2.enabled = true;
				}

				mCameraMode = value;
			}
		}
	}

	public ModelDisplayModeId DisplayMode
	{
		get
		{
			return mDisplayMode;
		}
		set
		{
			if (mPorch != null)
			{
				mPorch.DisplayMode = value;

				foreach (IModel m in mPorch)
				{
					m.DisplayMode = value;
				}
			}

			mDisplayMode = value;
		}
	}

	public PorchWallId SelectedWallId
	{
		get
		{
			return mSelectedWall;
		}
		set
		{
			Transform t = mPorch.GetWallCenterTransform(value);
			float yaw = 0;

			if (value == PorchWallId.Left)
			{
				yaw = 90;
			}
			else if (value == PorchWallId.Right)
			{
				yaw = -90;
			}
			else if (value == PorchWallId.None)
			{
				t = mPorch.Go.transform;
			}

			SetCameraPos(t, yaw);
			mSelectedWall = value;
		}
	}

	public PorchElement SelectedWallElement
	{
		get
		{
			return mSelectedWallElement;
		}
		set
		{
			mSelectedWallElement = value;
		}
	}

	public PorchWall SelectedWall
	{
		get
		{
			return Porch.GetWall(mSelectedWall);
		}
	}

	public Porch Porch
	{
		get
		{
			if (mPorch == null)
			{
				CreateModel(Porch.ModelName);
			}

			return mPorch;
		}
	}

	public GameObject WallMainIcon
	{
		get
		{
			return mWallMainIcon;
		}
	}

	public GameObject WallSideIcon1
	{
		get
		{
			return mWallSideIcon1;
		}
	}

	public GameObject WallSideIcon2
	{
		get
		{
			return mWallSideIcon2;
		}
	}

    public Model SelectedModel
    {
        get
        {
            return mSelectedModel;
        }
        set
        {
            mSelectedModel = value;
        }
    }

	#endregion

	#region Private data

	private ModelDisplayModeId mDisplayMode = ModelDisplayModeId.Default;

	private CameraModeId mCameraMode = CameraModeId.DefaultView;
	private Transform mCameraTarget;
	private Vector3 mCameraPos_Default;
	private Quaternion mCameraRotation_Default;

	private GameObject mWallMainIcon;
	private GameObject mWallSideIcon1;
	private GameObject mWallSideIcon2;

	private Porch mPatternPorch;
	private Door36 mPatternDoor36;
	private StudHoriz mPatternStudHoriz;
	private Stud15 mPatternStud15;
	private Stud35 mPatternStud35;

	private Porch mPorch;
	private PorchElement mSelectedWallElement = null;
	private PorchWallId mSelectedWall = PorchWallId.Front;

    private Model mSelectedModel;

	#endregion

	#region Interface

	public Scene()
	{
	}

	public void Initialize()
	{
		mPatternPorch = new Porch(false);
		mPatternDoor36 = new Door36(false);
		mPatternStudHoriz = new StudHoriz(false);
		mPatternStud15 = new Stud15(false);
		mPatternStud35 = new Stud35(false);

		mPatternPorch.Visible = false;
		mPatternDoor36.Visible = false;
		mPatternStudHoriz.Visible = false;
		mPatternStud15.Visible = false;
		mPatternStud35.Visible = false;

		mCameraPos_Default = Camera.main.transform.position;
		mCameraRotation_Default = Camera.main.transform.rotation;
		mCameraTarget = AppRoot.Instance.transform;

		mWallMainIcon = GameObject.Find(Constants.PorchMainIconName);
		mWallMainIcon.active = false;
		mWallSideIcon1 = GameObject.Find(Constants.PorchSide1IconName);
		mWallSideIcon1.active = false;
		mWallSideIcon2 = GameObject.Find(Constants.PorchSide2IconName);
		mWallSideIcon2.active = false;
	}

	public IModel CreateModel(string name)
	{
		IModel result = null;

		if (name == Porch.ModelName)
		{
			if (mPorch != null)
			{
				Debug.LogWarning("Only one porch model is allowed in scene, ignoring creation of another.");
			}
			else
			{
				result = mPatternPorch.Clone() as IModel;
				mPorch = result as Porch;
			}
		}
		else if (name == Door36.ModelName)
		{
			result = mPatternDoor36.Clone() as IModel;
		}
		else if (name == StudHoriz.ModelName)
		{
			result = mPatternStudHoriz.Clone() as IModel;
		}
		else if (name == Stud15.ModelName)
		{
			result = mPatternStud15.Clone() as IModel;
		}
		else if (name == Stud35.ModelName)
		{
			result = mPatternStud35.Clone() as IModel;
		}
		if (result != null)
		{
			result.DisplayMode = mDisplayMode;
			result.Go.tag = Constants.ModelTagName;
		}

		return result;
	}

	public void Update()
	{
		
	}

	public void Clear()
	{
		if (mPorch != null)
		{
			foreach (IModel m in mPorch)
			{
				m.Dispose();
			}

			mPorch.Dispose();
			mPorch = null;
		}
	}

	public void ResetCamera()
	{
		TBDragOrbitMod s = Camera.main.GetComponent<TBDragOrbitMod>();
		s.IdealYaw = 0;
		CameraTarget = Porch.Go.transform;
	}

	public void SetCameraPos(Transform target, float yaw)
	{
		TBDragOrbitMod s = Camera.main.GetComponent<TBDragOrbitMod>();
		s.IdealYaw = yaw;
		CameraTarget = target;
	}

	#endregion

	#region Implementation

	

	#endregion
}
