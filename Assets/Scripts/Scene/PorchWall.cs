using UnityEngine;
using System;
using System.Collections.Generic;

public class PorchWall
{
	#region Public data

	public readonly PorchWallId Id;
	public readonly Porch Porch;

	public IEnumerable<IModel> VStuds
	{
		get
		{
			return mVStuds;
		}
	}

	public IEnumerable<IModel> HRows
	{
		get
		{
			return mHRows;
		}
	}

	public bool Enabled
	{
		get
		{
			return mEnabled;
		}
		set
		{
			mEnabled = value;
		}
	}

	public Vector3 Pivot
	{
		get
		{
			return mPivot;
		}
	}

	public float Height
	{
		get
		{
			return Porch.Size.y;
		}
	}

	public float Length
	{
		get
		{
			return mLength;
		}
	}

	public float MinLength
	{
		get
		{
			//if (Id == PorchWallId.Front)
			//{
			//    return Constants.PorchWidthMin - Constants.PorchWallLengthOffset;
			//}
			//else
			//{
			//    return Constants.PorchDepthMin - Constants.PorchWallLengthOffset;
			//}

			return 2 * Constants.Ft2M;
		}
	}

	public float MaxLength
	{
		get
		{
			if (Id == PorchWallId.Front)
			{				
				return Porch.Size.x - Constants.PorchWallLengthOffset;
			}
			else
			{				
				return Porch.Size.z - Constants.PorchWallLengthOffset;
			}
		}
	}

	public int VStudsCount
	{
		get
		{
			return mVStuds.Count;
		}
	}

	public int MinVStudsCount
	{
		get
		{
			return 0;
		}
	}

	public int MaxVStudsCount
	{
		get
		{
			return Mathf.CeilToInt(mLength * Constants.M2Ft * 0.5f);
		}
	}

	public int HRowsCount
	{
		get
		{
			return mHRows.Count;
		}
	}

	public int MinHRowsCount
	{
		get
		{
			return 0;
		}
	}

	public float MaxHRowsCount
	{
		get
		{
			return 3;
		}
	}

	public Transform CenterTransform
	{
		get
		{
			if (mCenterDummy == null)
			{
				mCenterDummy = new GameObject("WallCenterPoint_" + Id);
			}

			return mCenterDummy.transform;
		}
	}

	#endregion

	#region Private data

	private bool mEnabled = false;
	private GameObject mCenterDummy;

	protected Vector3 mPivot = new Vector3();
	protected float mLength = 0;
	protected List<IModel> mVStuds = new List<IModel>();
	protected List<IModel> mHRows = new List<IModel>();

    protected Door36 mDoor;

	#endregion

	#region Interface

	public PorchWall(PorchWallId id, Porch parent)
	{
		Id = id;
		Porch = parent;
	}

	public IModel GetVStud(int index)
	{
		return mVStuds[index];
	}

	public float GetVStudPosMin(int index)
	{
		return 6 * Constants.In2M;
	}

	public float GetVStudPosMax(int index)
	{
		return Length - 6 * Constants.In2M;
	}

	public float GetVStudPos(int index)
	{
		if (index >= 0 && index < mVStuds.Count)
		{
			if (Id == PorchWallId.Front)
			{
				return mVStuds[index].Position.x - mPivot.x;
			}
			else
			{
				return mPivot.z - mVStuds[index].Position.z;
			}
		}

		return 0;
	}

	public void SetVStudPos(int index, float pos)
	{
		if (index >= 0 && index < mVStuds.Count)
		{
			IModel m = mVStuds[index];
			Vector3 v = m.Position;

			if (Id == PorchWallId.Front)
			{
				v.x = mPivot.x + pos;
			}
			else
			{
				v.z = mPivot.z - pos;
			}

			m.Position = v;
		}
	}

	public int CloneVStud(int index, int offsetSign)
	{
		if (index >= 0 && index < mVStuds.Count)
		{
			IModel pattern = mVStuds[index];
			IModel clone = pattern.Clone() as IModel;
			Vector3 v = clone.Position;

			if (Id == PorchWallId.Front)
			{
				v.x += offsetSign * 6 * Constants.In2M;
			}
			else
			{
				v.z -= offsetSign * 6 * Constants.In2M;
			}

			clone.Position = v;
			mVStuds.Add(clone);
            AppRoot.Scene.SelectedModel = clone as Model;
			return mVStuds.Count - 1;
		}

		return -1;
	}

	public void RemoveVStud(int index)
	{
		if (index >= 0 && index < mVStuds.Count)
		{
			mVStuds[index].Dispose();
			mVStuds.RemoveAt(index);
		}
	}

	public IModel GetRow(int index)
	{
		return mHRows[index];
	}

	public float GetRowHeight(int index)
	{
		if (index >= 0 && index < mHRows.Count)
		{
			return mHRows[index].Position.y - mPivot.y;
		}

		return 0;
	}

	public float GetRowHeightMin(int index)
	{
		return 6 * Constants.In2M;
	}

	public float GetRowHeightMax(int index)
	{
		return Height - 6 * Constants.In2M;
	}

	public void SetRowHeight(int index, float height)
	{
		if (index >= 0 && index < mHRows.Count)
		{
			IModel m = mHRows[index];
			Vector3 v = m.Position;
			v.y = mPivot.y + height;
			m.Position = v;
		}
	}

	public int CloneRow(int index)
	{
		if (index >= 0 && index < mHRows.Count)
		{
			IModel pattern = mHRows[index];
			IModel clone = pattern.Clone() as IModel;            
			Vector3 v = clone.Position;
			v.y += 6 * Constants.In2M;
			clone.Position = v;
			mHRows.Add(clone);
            AppRoot.Scene.SelectedModel = clone as Model;
			return mHRows.Count - 1;
		}

		return -1;
	}

	public void RemoveRow(int index)
	{
		if (index >= 0 && index < mHRows.Count)
		{
			mHRows[index].Dispose();
			mHRows.RemoveAt(index);
		}
	}

	public bool AddDoor(int studIndex, bool leftSide, float doorSize)
	{
		if (studIndex >= 0 && studIndex < mVStuds.Count)
		{
            if (Id == PorchWallId.None)
            {
                return false;
            }

            Door36 door = AppRoot.Scene.CreateModel(Door36.ModelName) as Door36;
            mDoor = door;
            Model currentStud = mVStuds[studIndex] as Model;

            // Constants.PorchHCornerHeight - width of stud
            // Constants.PorchVCornerSize - door stud width

            // at first check current stud, and reposition it if needs
            MoveCurrentStudToFitDoor(leftSide, studIndex, currentStud, doorSize);

            // check near stud and move it
            MoveNearStudToFitDoor(leftSide, doorSize, studIndex, currentStud);

            // not set position and rotation to door
            SetDoorPositionAndRotation(currentStud, leftSide, doorSize);

			return true;
		}

		return false;
	}

	public static float GetRowScale(float porchSize)
	{
		return (porchSize - Constants.PorchWallLengthOffset) / (1 - Constants.PorchWallLengthOffset);
	}

	#endregion

	#region Implementation

    ///////////////////////////////////////////////////////////////////////////
    #region MoveNearStudToFitDoor

    private void MoveNearStudToFitDoor(bool leftSide, float doorSize, int studIndex, Model currentStud)
    {
        float studsShift = doorSize + Constants.PorchVCornerSize * 2 + Constants.PorchHCornerHeight;
        if (mVStuds.Count >= 2 && studIndex > 0 && studIndex < mVStuds.Count - 1)
        {
            switch (Id)
            {
                case PorchWallId.Front:
                    {
                        if (leftSide)
                        {
                            // get next stud index 
                            int nextIndex = studIndex - 1;
                            Model prevStud = mVStuds[nextIndex] as Model;

                            Vector3 nStudPos = prevStud.Position;
                            nStudPos.x = currentStud.Position.x - studsShift;
                            prevStud.Position = nStudPos;
                        }
                        else
                        {
                            // get next stud index 
                            int nextIndex = studIndex + 1;
                            Model prevStud = mVStuds[nextIndex] as Model;

                            Vector3 nStudPos = prevStud.Position;
                            nStudPos.x = currentStud.Position.x + studsShift;
                            prevStud.Position = nStudPos;
                        }

                        break;
                    }
                case PorchWallId.Left:
                    {
                        if (leftSide)
                        {
                            // get next stud index 
                            int nextIndex = studIndex - 1;
                            Model prevStud = mVStuds[nextIndex] as Model;

                            Vector3 nStudPos = prevStud.Position;
                            nStudPos.z = currentStud.Position.z + studsShift;
                            prevStud.Position = nStudPos;
                        }
                        else
                        {
                            // get next stud index 
                            int nextIndex = studIndex + 1;
                            Model prevStud = mVStuds[nextIndex] as Model;

                            Vector3 nStudPos = prevStud.Position;
                            nStudPos.z = currentStud.Position.z - studsShift;
                            prevStud.Position = nStudPos;
                        }

                        break;
                    }
                case PorchWallId.Right:
                    {
                        if (leftSide)
                        {
                            // get next stud index 
                            int nextIndex = studIndex + 1;
                            Model prevStud = mVStuds[nextIndex] as Model;

                            Vector3 nStudPos = prevStud.Position;
                            nStudPos.z = currentStud.Position.z - studsShift;
                            prevStud.Position = nStudPos;
                        }
                        else
                        {
                            // get next stud index 
                            int nextIndex = studIndex - 1;
                            Model prevStud = mVStuds[nextIndex] as Model;

                            Vector3 nStudPos = prevStud.Position;
                            nStudPos.z = currentStud.Position.z + studsShift;
                            prevStud.Position = nStudPos;
                        }

                        break;
                    }
            }
        }

    }

    #endregion
    ///////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////
    #region MoveCurrentStudToFitDoor

    private void MoveCurrentStudToFitDoor(bool leftSide, int studIndex, Model currentStud, float doorSize)
    {
        //
        float fullDoorWidth = doorSize + Constants.PorchVCornerSize * 2;

        switch (Id)
        {
            case PorchWallId.Front:
                {
                    if (leftSide)
                    {
                        if (studIndex == 0)
                        {
                            Vector3 nStudPos = currentStud.Position;
                            nStudPos.x = mPivot.x + fullDoorWidth + Constants.PorchHCornerHeight / 2;
                            currentStud.Position = nStudPos;
                        }
                    }
                    else
                    {
                        if (studIndex == mVStuds.Count - 1)
                        {
                            Vector3 nStudPos = currentStud.Position;
                            nStudPos.x = mPivot.x + mLength - fullDoorWidth - Constants.PorchHCornerHeight / 2;
                            currentStud.Position = nStudPos;
                        }
                    }

                    break;
                }
            case PorchWallId.Left:
                {
                    if (leftSide)
                    {
                        if (studIndex == 0)
                        {
                            Vector3 nStudPos = currentStud.Position;
                            nStudPos.z = mPivot.z - fullDoorWidth - Constants.PorchHCornerHeight / 2;
                            currentStud.Position = nStudPos;
                        }
                    }
                    else
                    {
                        if (studIndex == mVStuds.Count - 1)
                        {
                            Vector3 nStudPos = currentStud.Position;
                            nStudPos.z = mPivot.z - mLength + fullDoorWidth + Constants.PorchHCornerHeight / 2;
                            currentStud.Position = nStudPos;
                        }
                    }
                    break;
                }
            case PorchWallId.Right:
                {
                    if (leftSide)
                    {
                        if (studIndex == mVStuds.Count - 1)
                        {
                            Vector3 nStudPos = currentStud.Position;
                            nStudPos.z = mPivot.z - mLength + fullDoorWidth + Constants.PorchHCornerHeight / 2;
                            currentStud.Position = nStudPos;
                        }
                    }
                    else
                    {
                        if (studIndex == 0)
                        {
                            Vector3 nStudPos = currentStud.Position;
                            nStudPos.z = mPivot.z - fullDoorWidth - Constants.PorchHCornerHeight / 2;
                            currentStud.Position = nStudPos;
                        }
                    }

                    break;
                }
        }
    }

    #endregion
    ///////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////
    #region SetDoorPositionAndRotation
    private void SetDoorPositionAndRotation(Model currentStud, bool leftSide, float doorSize)
    {
        Vector3 nDoorPos = currentStud.Position;
        Quaternion nDoorQuatRot = Quaternion.identity;
        float doorOffset = doorSize / 2 + Constants.PorchHCornerHeight / 2 + Constants.PorchVCornerSize;
        switch (Id)
        {
            case PorchWallId.Front:
                {
                    if (leftSide)
                    {
                        nDoorPos.x = currentStud.Position.x - doorOffset;
                    }
                    else
                    {
                        nDoorPos.x = currentStud.Position.x + doorOffset;
                    }

                    break;
                }
            case PorchWallId.Left:
                {
                    if (leftSide)
                    {
                        nDoorPos.z = currentStud.Position.z + doorOffset;
                    }
                    else
                    {
                        nDoorPos.z = currentStud.Position.z - doorOffset;
                    }

                    nDoorQuatRot = Quaternion.Euler(0, 90.0f, 0);

                    break;
                }
            case PorchWallId.Right:
                {
                    if (leftSide)
                    {
                        nDoorPos.z = currentStud.Position.z - doorOffset;
                    }
                    else
                    {
                        nDoorPos.z = currentStud.Position.z + doorOffset;
                    }

                    nDoorQuatRot = Quaternion.Euler(0, -90.0f, 0);

                    break;
                }
        }

        mDoor.Position = nDoorPos;
        mDoor.Rotation = nDoorQuatRot;
    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////
    

	#endregion
}

public class PorchWallExt : PorchWall
{
	#region Public data
	#endregion

	#region Private data

	private Transform[] mLengthScale;
	private Transform[] mLengthOffset;

	#endregion

	#region Interface

	public PorchWallExt(PorchWallId id, Porch parent)
		: base(id, parent)
	{
	}

	public void SetTransforms(Transform[] scale, Transform[] offset)
	{
		mLengthScale = scale;
		mLengthOffset = offset;
	}

	public void SetLength(float length, Vector3 size)
	{
		if (Id == PorchWallId.Front)
		{
			mPivot.x = Constants.PorchCornerOffset + Constants.PorchVCornerSize;
			mPivot.y = Constants.PorchFloorHeight + Constants.PorchHCornerHeight;
			mPivot.z = -(size.z - Constants.PorchCornerOffset - Constants.PorchVCornerSize * 0.5f);

			CenterTransform.position = new Vector3(size.x * 0.5f, size.y * 0.5f, mPivot.z);
			CenterTransform.rotation = Quaternion.LookRotation(-Vector3.forward, Vector3.up);
		}
		else if (Id == PorchWallId.Left)
		{
			mPivot.x = Constants.PorchCornerOffset + Constants.PorchVCornerSize * 0.5f;
			mPivot.y = Constants.PorchFloorHeight + Constants.PorchHCornerHeight;
			mPivot.z = -(Constants.PorchCornerOffset + Constants.PorchVCornerSize);

			CenterTransform.position = new Vector3(mPivot.x, size.y * 0.5f, -size.z * 0.5f);
			CenterTransform.rotation = Quaternion.LookRotation(-Vector3.right, Vector3.up);
		}
		else if (Id == PorchWallId.Right)
		{
			mPivot.x = size.x - Constants.PorchCornerOffset - Constants.PorchVCornerSize * 0.5f;
			mPivot.y = Constants.PorchFloorHeight + Constants.PorchHCornerHeight;
			mPivot.z = -(Constants.PorchCornerOffset + Constants.PorchVCornerSize);

			CenterTransform.position = new Vector3(mPivot.x, size.y * 0.5f, -size.z * 0.5f);
			CenterTransform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
		}
		mLength = length;
	}

	public void SetLengthConstrained(float length)
	{
		if (Enabled && Id != PorchWallId.Front)
		{
			if (length > MaxLength)
			{
				length = MaxLength;
			}
			else if (length < MinLength)
			{
				length = MinLength;
			}

			Vector3 v;
			Vector3 size = Porch.Size;
			float scale = length / (1 - Constants.PorchWallLengthOffset);
			float off = size.z - length - Constants.PorchCornerOffset - Constants.PorchVCornerSize * 1.5f;

			foreach (Transform t in mLengthScale)
			{
				v = t.localScale;
				v.y = scale;
				t.localScale = v;

				v = t.localPosition;
				v.z = -(off + Constants.PorchVCornerSize * 0.5f);
				t.localPosition = v;
			}
			foreach (Transform t in mLengthOffset)
			{
				v = t.localPosition;
				v.z = -off;
				t.localPosition = v;
			}

			mPivot.z = -(size.z - length - Constants.PorchCornerOffset - Constants.PorchVCornerSize);
			mLength = length;

			//UpdateCenterTransform();
			UpdateVStuds();
			UpdateHRows();
		}
	}

	public void SetVStudsCount(int count)
	{
		if (Enabled && mVStuds.Count != count)
		{
			if (mVStuds.Count > count)
			{
				for (int i = count; i < mVStuds.Count; ++i)
				{
					mVStuds[i].Dispose();
				}

				mVStuds.RemoveRange(count, mVStuds.Count - count);
			}
			for (int i = 0; i < count; ++i)
			{
				if (i >= mVStuds.Count)
				{
					mVStuds.Add(AppRoot.Scene.CreateModel(Stud15.ModelName));
				}
			}

			UpdateVStuds();
		}
	}

	public void SetHRowsCount(int count)
	{
		if (Enabled && mHRows.Count != count)
		{
			if (mHRows.Count > count)
			{
				for (int i = count; i < mHRows.Count; ++i)
				{
					mHRows[i].Dispose();
				}

				mHRows.RemoveRange(count, mHRows.Count - count);
			}
			for (int i = 0; i < count; ++i)
			{
				if (i >= mHRows.Count)
				{
					mHRows.Add(AppRoot.Scene.CreateModel(StudHoriz.ModelName));
				}
			}

			UpdateHRows();
		}
	}

	public void SetDefaultVStudsCount()
	{
		int c = (int)(mLength * Constants.M2Ft * 0.5f - 1);
		SetVStudsCount(c);
	}

	public void SetDefaultHRowsCount()
	{
		int c = (int)(Height * Constants.M2Ft * 0.5f - 1);
		SetHRowsCount(c);
	}

	#endregion

	#region Implementation

	private void UpdateVStuds()
	{
		IModel m;
		Vector3 v, s;
		float spacing = mLength / (mVStuds.Count + 1);

		v.x = v.z = 0;
		v.y = -Constants.PorchHCornerHeight;

		s.x = s.z = 1;
		s.y = Porch.Size.y;

		for (int i = 0; i < mVStuds.Count; ++i)
		{
			m = mVStuds[i];

			if (Id != PorchWallId.Front)
			{
				v.z = -spacing * (i + 1);

				if (Id == PorchWallId.Right)
				{
					m.Rotation = Quaternion.Euler(0, -90, 0);
				}
				else
				{
					m.Rotation = Quaternion.Euler(0, 90, 0);
				}
			}
			else
			{
				v.x = spacing * (i + 1);
			}

			m.Scale = s;
			m.Position = mPivot + v;
		}
	}

	private void UpdateHRows()
	{
		const float firstRowHeight = 36 * Constants.In2M;

		IModel m;
		Vector3 v, s;
		float spacing = (Height - firstRowHeight) / mHRows.Count;

		v.x = v.z = 0;
		s.y = s.z = 1;
		s.x = mLength;

		for (int i = 0; i < mHRows.Count; ++i)
		{
			m = mHRows[i];

			v.y = firstRowHeight + spacing * i;

			if (Id != PorchWallId.Front)
			{
				if (Id == PorchWallId.Right)
				{
					m.Rotation = Quaternion.Euler(180, 90, 0);
				}
				else
				{
					m.Rotation = Quaternion.Euler(0, 90, 0);
				}
			}

			m.Scale = s;
			m.Position = mPivot + v;
		}
	}

	private void UpdateCenterTransform()
	{
		CenterTransform.position = new Vector3(mPivot.x, Height * 0.5f, mPivot.z - mLength * 0.5f);
	}

	#endregion
}