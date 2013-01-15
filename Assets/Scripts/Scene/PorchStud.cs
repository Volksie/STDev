using UnityEngine;
using System;
using System.Collections.Generic;

public class PorchStud : PorchElement
{
	#region Public data

	public PorchWall Wall
	{
		get
		{
			return mWall;
		}
	}

	#endregion

	#region Private data

	private PorchWall mWall;
	private int mIndex;

	#endregion

	#region Interface

	public PorchStud(PorchWall w, int index)
	{
		mWall = w;
		mIndex = index;
	}

	#endregion

	#region Implementation of PorchElement

	public override float MinOffset
	{
		get
		{
			if (mWall != null)
			{
				return mWall.GetVStudPosMin(mIndex);
			}

			return 0;
		}
	}

	public override float MaxOffset
	{
		get
		{
			if (mWall != null)
			{
				return mWall.GetVStudPosMax(mIndex);
			}

			return 0;
		}
	}

	public override float Offset
	{
		get
		{
			if (mWall != null)
			{
				return mWall.GetVStudPos(mIndex);
			}

			return 0;
		}
		set
		{
			if (mWall != null)
			{
				mWall.SetVStudPos(mIndex, value);
			}
		}
	}

	public override PorchElement Clone(bool leftSide)
	{
		if (mWall != null)
		{
			PorchStud stud = new PorchStud(mWall, mWall.CloneVStud(mIndex, leftSide ? -1 : 1));
			AppRoot.Scene.SelectedWallElement = stud;
			return stud;
		}

		return null;
	}

	public override void Delete()
	{
		if (mWall != null)
		{
			mWall.RemoveVStud(mIndex);
			mWall = null;
		}

		AppRoot.Scene.SelectedWallElement = null;
	}

	#endregion

	#region Implementation
	#endregion
}
