using UnityEngine;
using System;
using System.Collections.Generic;

public class PorchRow : PorchElement
{
	#region Public data

	public int Index
	{
		get
		{
			return mIndex;
		}
	}

	#endregion

	#region Private data

	private PorchWall mWall;
	private int mIndex;

	#endregion

	#region Interface

	public PorchRow(PorchWall wall, int index)
	{
		mWall = wall;
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
				return mWall.GetRowHeightMin(mIndex);
			}

			return 0;
			//return AppRoot.Scene.Porch.GetWall(PorchWallId.Front).GetRowHeightMin(mIndex);
		}
	}

	public override float MaxOffset
	{
		get
		{
			if (mWall != null)
			{
				return mWall.GetRowHeightMax(mIndex);
			}

			return 0;
			//return AppRoot.Scene.Porch.GetWall(PorchWallId.Front).GetRowHeightMax(mIndex);
		}
	}

	public override float Offset
	{
		get
		{
			if (mWall != null)
			{
				return mWall.GetRowHeight(mIndex);
			}

			return 0;
			//return AppRoot.Scene.Porch.GetWall(PorchWallId.Front).GetRowHeight(mIndex);
		}
		set
		{
			if (mWall != null)
			{
				mWall.SetRowHeight(mIndex, value);
			}

			//AppRoot.Scene.Porch.GetWall(PorchWallId.Front).SetRowHeight(mIndex, value);
			//AppRoot.Scene.Porch.GetWall(PorchWallId.Left).SetRowHeight(mIndex, value);
			//AppRoot.Scene.Porch.GetWall(PorchWallId.Right).SetRowHeight(mIndex, value);
		}
	}

	public override bool IsHorizontal
	{
		get
		{
			return true;
		}
	}

	public override PorchElement Clone(bool side)
	{
		if (mWall != null)
		{
			//int index = AppRoot.Scene.Porch.GetWall(PorchWallId.Front).CloneRow(mIndex);
			//AppRoot.Scene.Porch.GetWall(PorchWallId.Left).CloneRow(mIndex);
			//AppRoot.Scene.Porch.GetWall(PorchWallId.Right).CloneRow(mIndex);

			int index = mWall.CloneRow(mIndex);
			PorchRow row = new PorchRow(mWall, index);
			AppRoot.Scene.SelectedWallElement = row;
			return row;
		}

		return null;
	}

	public override void Delete()
	{
		if (mWall != null)
		{
			mWall.RemoveRow(mIndex);
		}

		//AppRoot.Scene.Porch.GetWall(PorchWallId.Front).RemoveRow(mIndex);
		//AppRoot.Scene.Porch.GetWall(PorchWallId.Left).RemoveRow(mIndex);
		//AppRoot.Scene.Porch.GetWall(PorchWallId.Right).RemoveRow(mIndex);
		AppRoot.Scene.SelectedWallElement = null;
	}

	#endregion

	#region Implementation
	#endregion
}
