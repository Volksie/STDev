using UnityEngine;
using System;
using System.Collections.Generic;

public enum PorchWallLayoutId
{
	Undefined, 
	OneWall, 
	TwoWallLeft, 
	TwoWallRight, 
	ThreeWall
}

public enum PorchWallId
{
	None, 
	Front, 
	Left, 
	Right
}

public class Porch : Model
{
	#region Public data

	public const string ModelName = "porch";

	public PorchWallLayoutId Layout
	{
		get
		{
			return mLayout;
		}
		set
		{
			if (mLayout != value)
			{
				ApplyLayout(value);
				mLayout = value;
			}
		}
	}

	public Vector3 Size
	{
		get
		{
			return mSize;
		}
		set
		{
			ApplySize(value);
			mSize = value;
		}
	}

	public float BoundsSize
	{
		get
		{
			return Math.Max(Math.Max(mSize.x, mSize.y), mSize.z);
		}
	}

	public bool WallIconsVisible
	{
		get
		{
			return mWallIconsVisible;
		}
		set
		{
			if (mWallIconsVisible != value)
			{
				ShowWallIcons(value);
				mWallIconsVisible = value;
			}
		}
	}

	public bool StudRowSelectionEnabled
	{
		get
		{
			return mStuRowSelectionEnabled;
		}
		set
		{
			if (mStuRowSelectionEnabled != value)
			{
				// TODO: 
				mStuRowSelectionEnabled = value;
			}
		}
	}

	#endregion

	#region Private data

	private Vector3 mSize = new Vector3(Constants.PorchWidthMin, Constants.PorchHeightMin, Constants.PorchDepthMin);
	private PorchWallLayoutId mLayout = PorchWallLayoutId.Undefined;
	private PorchWallExt[] mWalls = new PorchWallExt[4];
	private bool mWallIconsVisible = false;
	private bool mStuRowSelectionEnabled = false;

	private Transform[] mHeightScale;
	private Transform[] mHeightOffset;
	private Transform[] mWidthScale;
	private Transform[] mWidthOffset;
	private Transform[] mDepthScale;
	private Transform[] mDepthOffset;
	private Transform mFloor;
	private Transform mRoof;
	private Transform mArrowHeight;
	private Transform mArrowWidth;
	private Transform mArrowDepth;

	#endregion

	#region Interface

	public Porch()
		: this(true)
	{
	}

	public Porch(bool createNewInstance)
		: base(ModelName, createNewInstance)
	{
		if (createNewInstance)
		{
			Initialize();
			ApplySize(mSize);
		}
	}

	public Porch(GameObject go, bool createNewInstance)
		: base(go, createNewInstance)
	{
		if (createNewInstance)
		{
			Initialize();
			ApplySize(mSize);
		}
	}

	public PorchWall GetWall(PorchWallId id)
	{
		return mWalls[(int)id];
	}

	public Transform GetWallCenterTransform(PorchWallId id)
	{
		return mWalls[(int)id].CenterTransform;
	}

	public PorchElement GetElement(GameObject go)
	{
		foreach (PorchWall w in mWalls)
		{
			if (w != null)
			{
				// studs
				for (int i = 0; i < w.VStudsCount; ++i)
				{
					IModel m = w.GetVStud(i);

					if (m.Go == go)
					{
						return new PorchStud(w, i);
					}
				}

				// rows
				for (int i = 0; i < w.HRowsCount; ++i)
				{
					IModel m = w.GetRow(i);

					if (m.Go == go)
					{
						return new PorchRow(w, i);
					}
				}
			}
		}

		return null;
	}

	public void SetWallLength(PorchWallId id, float length)
	{
		if (id == PorchWallId.Left || id == PorchWallId.Right)
		{
			mWalls[(int)id].SetLengthConstrained(length);
		}
	}

	public void SetWallVStudsCount(PorchWallId id, int count)
	{
		mWalls[(int)id].SetVStudsCount(count);
	}

	public void SetWallHRowsCount(PorchWallId id, int count)
	{
		mWalls[(int)id].SetHRowsCount(count);
		//mWalls[(int)PorchWallId.Front].SetHRowsCount(count);
		//mWalls[(int)PorchWallId.Left].SetHRowsCount(count);
		//mWalls[(int)PorchWallId.Right].SetHRowsCount(count);
	}

	public void AutoResetWallStudsAndRows()
	{
		mWalls[(int)PorchWallId.Front].SetDefaultVStudsCount();
		mWalls[(int)PorchWallId.Front].SetDefaultHRowsCount();
		mWalls[(int)PorchWallId.Left].SetDefaultVStudsCount();
		mWalls[(int)PorchWallId.Left].SetDefaultHRowsCount();
		mWalls[(int)PorchWallId.Right].SetDefaultVStudsCount();
		mWalls[(int)PorchWallId.Right].SetDefaultHRowsCount();
	}

	#endregion

	#region Implementation on Model

	protected override bool IsAffordanceVisible(string name, bool baseVisible)
	{
		if (baseVisible && mLayout == PorchWallLayoutId.OneWall)
		{
			if (name.Contains(Constants.ModelPartTag_AffordanceDepth))
			{
				return false;
			}
		}

		return baseVisible;
	}

	#endregion

	#region Implementation of IEnumerable

	public override IEnumerator<IModel> GetEnumerator()
	{
		foreach (PorchWall w in mWalls)
		{
			if (w != null)
			{
				foreach (IModel m in w.VStuds)
				{
					yield return m;
				}
				foreach (IModel m in w.HRows)
				{
					yield return m;
				}
			}
		}
	}

	#endregion

	#region Implementation of ICloneable

	public override object Clone()
	{
		return new Porch(Go, true);
	}

	#endregion

	#region Implementation of IDisposable

	public override void Dispose()
	{
		base.Dispose();

		for (int i = 0; i < mWalls.Length; ++i)
		{
			mWalls[i] = null;
		}
	}

	#endregion

	#region Implementation

	private void Initialize()
	{
		List<Transform> hScale = new List<Transform>();
		List<Transform> hOffset = new List<Transform>();
		List<Transform> wScale = new List<Transform>();
		List<Transform> wOffset = new List<Transform>();
		List<Transform> dScale = new List<Transform>();
		List<Transform> dOffset = new List<Transform>();

		List<Transform> wlScale = new List<Transform>();
		List<Transform> wlOffset = new List<Transform>();
		List<Transform> wrScale = new List<Transform>();
		List<Transform> wrOffset = new List<Transform>();

		wlScale.Add(Go.transform.Find("plate-side-a-top"));
		wlScale.Add(Go.transform.Find("plate-side-btm-a"));
		wlOffset.Add(Go.transform.Find("PostCorner_backa"));

		wrScale.Add(Go.transform.Find("plate-side-b-top"));
		wrScale.Add(Go.transform.Find("plate-side-b-btm"));
		wrOffset.Add(Go.transform.Find("PostCorner_Backb"));

		// FastTrack
		//hScale.Add(Go.transform.Find("FastTrack-h00"));
		hOffset.Add(Go.transform.Find("FastTrack-h06"));
		hOffset.Add(Go.transform.Find("FastTrack-h07"));
		hOffset.Add(Go.transform.Find("FastTrack-h10"));
		wScale.Add(Go.transform.Find("FastTrack-h10"));
		wScale.Add(Go.transform.Find("FastTrack-h11"));
		wOffset.Add(Go.transform.Find("FastTrack-h06"));
		wOffset.Add(Go.transform.Find("FastTrack-h08"));
		wOffset.Add(Go.transform.Find("FastTrack-v01"));
		wOffset.Add(Go.transform.Find("FastTrack-v04"));
		wOffset.Add(Go.transform.Find("FastTrack-v05"));
		dScale.Add(Go.transform.Find("FastTrack-h06"));
		dScale.Add(Go.transform.Find("FastTrack-h07"));
		dScale.Add(Go.transform.Find("FastTrack-h08"));
		dScale.Add(Go.transform.Find("FastTrack-h09"));
		dOffset.Add(Go.transform.Find("FastTrack-h10"));
		dOffset.Add(Go.transform.Find("FastTrack-h11"));
		dOffset.Add(Go.transform.Find("FastTrack-v00"));
		dOffset.Add(Go.transform.Find("FastTrack-v03"));
		dOffset.Add(Go.transform.Find("FastTrack-v04"));
		dOffset.Add(Go.transform.Find("FastTrack-v05"));
		wlScale.Add(Go.transform.Find("FastTrack-h07"));
		wlScale.Add(Go.transform.Find("FastTrack-h09"));
		wlOffset.Add(Go.transform.Find("FastTrack-v02"));
		wrScale.Add(Go.transform.Find("FastTrack-h06"));
		wrScale.Add(Go.transform.Find("FastTrack-h08"));
		wrOffset.Add(Go.transform.Find("FastTrack-v01"));

		// MiniTrack
		//hScale.Add(Go.transform.Find("MiniTrack-h00"));
		hOffset.Add(Go.transform.Find("MiniTrack-h06"));
		hOffset.Add(Go.transform.Find("MiniTrack-h07"));
		hOffset.Add(Go.transform.Find("MiniTrack-h10"));
		wScale.Add(Go.transform.Find("MiniTrack-h10"));
		wScale.Add(Go.transform.Find("MiniTrack-h11"));
		wOffset.Add(Go.transform.Find("MiniTrack-h07"));
		wOffset.Add(Go.transform.Find("MiniTrack-h09"));
		wOffset.Add(Go.transform.Find("MiniTrack-v01"));
		wOffset.Add(Go.transform.Find("MiniTrack-v04"));
		wOffset.Add(Go.transform.Find("MiniTrack-v05"));
		dScale.Add(Go.transform.Find("MiniTrack-h06"));
		dScale.Add(Go.transform.Find("MiniTrack-h07"));
		dScale.Add(Go.transform.Find("MiniTrack-h08"));
		dScale.Add(Go.transform.Find("MiniTrack-h09"));
		dOffset.Add(Go.transform.Find("MiniTrack-h10"));
		dOffset.Add(Go.transform.Find("MiniTrack-h11"));
		dOffset.Add(Go.transform.Find("MiniTrack-v00"));
		dOffset.Add(Go.transform.Find("MiniTrack-v03"));
		dOffset.Add(Go.transform.Find("MiniTrack-v04"));
		dOffset.Add(Go.transform.Find("MiniTrack-v05"));
		wlScale.Add(Go.transform.Find("MiniTrack-h06"));
		wlScale.Add(Go.transform.Find("MiniTrack-h08"));
		wlOffset.Add(Go.transform.Find("MiniTrack-v02"));
		wrScale.Add(Go.transform.Find("MiniTrack-h07"));
		wrScale.Add(Go.transform.Find("MiniTrack-h09"));
		wrOffset.Add(Go.transform.Find("MiniTrack-v01"));

		// ScreenTight
		//hScale.Add(Go.transform.Find("ScreenTight-15-h00"));
		hOffset.Add(Go.transform.Find("ScreenTight-15-h00"));
		hOffset.Add(Go.transform.Find("ScreenTight-15-h01"));
		hOffset.Add(Go.transform.Find("ScreenTight-15-h04"));
		wScale.Add(Go.transform.Find("ScreenTight-15-h04"));
		wScale.Add(Go.transform.Find("ScreenTight-15-h05"));
		wOffset.Add(Go.transform.Find("ScreenTight-15-h01"));
		wOffset.Add(Go.transform.Find("ScreenTight-15-h03"));
		wOffset.Add(Go.transform.Find("ScreenTight-35-v07"));
		wOffset.Add(Go.transform.Find("ScreenTight-35-v09"));
		wOffset.Add(Go.transform.Find("ScreenTight-35-v10"));
		dScale.Add(Go.transform.Find("ScreenTight-15-h00"));
		dScale.Add(Go.transform.Find("ScreenTight-15-h01"));
		dScale.Add(Go.transform.Find("ScreenTight-15-h02"));
		dScale.Add(Go.transform.Find("ScreenTight-15-h03"));
		dOffset.Add(Go.transform.Find("ScreenTight-15-h04"));
		dOffset.Add(Go.transform.Find("ScreenTight-15-h05"));
		dOffset.Add(Go.transform.Find("ScreenTight-35-v06"));
		dOffset.Add(Go.transform.Find("ScreenTight-35-v07"));
		dOffset.Add(Go.transform.Find("ScreenTight-35-v08"));
		dOffset.Add(Go.transform.Find("ScreenTight-35-v09"));
		wlScale.Add(Go.transform.Find("ScreenTight-15-h00"));
		wlScale.Add(Go.transform.Find("ScreenTight-15-h02"));
		wlOffset.Add(Go.transform.Find("ScreenTight-35-v11"));
		wrScale.Add(Go.transform.Find("ScreenTight-15-h01"));
		wrScale.Add(Go.transform.Find("ScreenTight-15-h03"));
		wrOffset.Add(Go.transform.Find("ScreenTight-35-v10"));

		foreach (Transform t in Go.transform)
		{
			if (t.name.Contains("PorchBase"))
			{
				mFloor = t;
			}
			else if (t.name.Contains("PorchRoof"))
			{
				mRoof = t;
			}
			else if (t.name.Contains("PostCorner"))
			{
				if (t.name.Contains("Backb") || t.name.Contains("frontb"))
				{
					wOffset.Add(t);
				}
				if (t.name.Contains("front"))
				{
					dOffset.Add(t);
				}

				hScale.Add(t);
			}
			else if (t.name.Contains("-v"))
			{
				// for FastTrack/MiniTrack/ScreenTight
				hScale.Add(t);
			}
			else if (t.name.Contains("plate"))
			{
				if (t.name.Contains("top"))
				{
					hOffset.Add(t);
				}
				if (t.name.Contains("front"))
				{
					dOffset.Add(t);
					wScale.Add(t);
				}
				if (t.name.Contains("side-b-"))
				{
					wOffset.Add(t);
				}
				if (t.name.Contains("side"))
				{
					dScale.Add(t);
				}
			}
			else if (t.name.Contains(Constants.ModelPartTag_Affordance))
			{
				if (t.name.Contains("depth"))
				{
					mArrowDepth = t;
				}
				else if (t.name.Contains("width"))
				{
					mArrowWidth = t;
				}
				else if (t.name.Contains("height"))
				{
					mArrowHeight = t;
				}
			}
		}

		mHeightScale = hScale.ToArray();
		mHeightOffset = hOffset.ToArray();
		mWidthScale = wScale.ToArray();
		mWidthOffset = wOffset.ToArray();
		mDepthScale = dScale.ToArray();
		mDepthOffset = dOffset.ToArray();

		// walls
		mWalls[(int)PorchWallId.None] = null;
		mWalls[(int)PorchWallId.Front] = new PorchWallExt(PorchWallId.Front, this);
		mWalls[(int)PorchWallId.Front].SetTransforms(null, null);
		mWalls[(int)PorchWallId.Left] = new PorchWallExt(PorchWallId.Left, this);
		mWalls[(int)PorchWallId.Left].SetTransforms(wlScale.ToArray(), wlOffset.ToArray());
		mWalls[(int)PorchWallId.Right] = new PorchWallExt(PorchWallId.Right, this);
		mWalls[(int)PorchWallId.Right].SetTransforms(wrScale.ToArray(), wrOffset.ToArray());

		// set default position
		Position = new Vector3(0, Constants.PorchFloorHeight, 0);
	}

	private void ApplySize(Vector3 size)
	{
		Vector3 s, v;
		s.x = PorchWall.GetRowScale(size.x);
		s.z = PorchWall.GetRowScale(size.z);

		// 1. Width
		foreach (Transform t in mWidthScale)
		{
			v = t.localScale;
			v.x = s.x;
			t.localScale = v;
		}
		foreach (Transform t in mWidthOffset)
		{
			v = t.localPosition;
			v.x = size.x - Constants.PorchCornerOffset - Constants.PorchVCornerSize * 0.5f;
			t.localPosition = v;
		}

		// 2. Height
		foreach (Transform t in mHeightScale)
		{
			v = t.localScale;
			v.z = size.y;
			t.localScale = v;
		}
		foreach (Transform t in mHeightOffset)
		{
			v = t.localPosition;
			v.y = size.y - Constants.PorchHCornerHeight * 0.5f;
			t.localPosition = v;
		}
		v = mRoof.localPosition;
		v.y = size.y - 1;
		mRoof.localPosition = v;

		// 3. Depth
		foreach (Transform t in mDepthScale)
		{
			v = t.localScale;
			v.y = s.z;
			t.localScale = v;
		}
		foreach (Transform t in mDepthOffset)
		{
			v = t.localPosition;
			v.z = -(size.z - Constants.PorchCornerOffset - Constants.PorchVCornerSize * 0.5f);
			t.localPosition = v;
		}

		// 4. Floor/roof scaling
		v = mFloor.localScale;
		v.x = size.x;
		v.y = size.z;
		mFloor.localScale = v;
		mRoof.localScale = v;

		// 5. Affordance positioning
		v = mArrowDepth.localPosition;
		v.x = size.x - Constants.PorchArrowOffset;
		v.z = -(size.z + Constants.PorchArrowOffset);
		mArrowDepth.localPosition = v;
		v = mArrowWidth.localPosition;
		v.x = size.x + Constants.PorchArrowOffset;
		v.z = -(size.z - Constants.PorchArrowOffset);
		mArrowWidth.localPosition = v;
		v = mArrowHeight.localPosition;
		v.x = size.x + Constants.PorchArrowOffset;
		v.z = -(size.z + Constants.PorchArrowOffset);
		mArrowHeight.localPosition = v;

		// 6. Wall lengths
		mWalls[(int)PorchWallId.Front].SetLength(size.x - Constants.PorchWallLengthOffset, size);
		mWalls[(int)PorchWallId.Left].SetLength(size.z - Constants.PorchWallLengthOffset, size);
		mWalls[(int)PorchWallId.Right].SetLength(size.z - Constants.PorchWallLengthOffset, size);

		// 7. Wall positions
		// don't need this right now
	}

	private void ApplyLayout(PorchWallLayoutId layout)
	{
		if (layout == PorchWallLayoutId.OneWall)
		{
			GetWall(PorchWallId.Front).Enabled = true;
			GetWall(PorchWallId.Left).Enabled = false;
			GetWall(PorchWallId.Right).Enabled = false;
		}
		else if (layout == PorchWallLayoutId.TwoWallLeft)
		{
			GetWall(PorchWallId.Front).Enabled = true;
			GetWall(PorchWallId.Left).Enabled = true;
			GetWall(PorchWallId.Right).Enabled = false;
		}
		else if (layout == PorchWallLayoutId.TwoWallRight)
		{
			GetWall(PorchWallId.Front).Enabled = true;
			GetWall(PorchWallId.Left).Enabled = false;
			GetWall(PorchWallId.Right).Enabled = true;
		}
		else if (layout == PorchWallLayoutId.ThreeWall)
		{
			GetWall(PorchWallId.Front).Enabled = true;
			GetWall(PorchWallId.Left).Enabled = true;
			GetWall(PorchWallId.Right).Enabled = true;
		}
		else
		{
			GetWall(PorchWallId.Front).Enabled = false;
			GetWall(PorchWallId.Left).Enabled = false;
			GetWall(PorchWallId.Right).Enabled = false;
		}
	}

	private void ShowWallIcons(bool show)
	{
		GameObject main = AppRoot.Scene.WallMainIcon;
		GameObject side1 = AppRoot.Scene.WallSideIcon1;
		GameObject side2 = AppRoot.Scene.WallSideIcon2;

		if (show)
		{

			main.transform.position = new Vector3(mSize.x * 0.5f, Constants.PorchFloorHeight, -mSize.z - Constants.PorchWallIconOffset);
			main.active = true;

			if (mLayout == PorchWallLayoutId.OneWall)
			{
				side1.active = false;
				side2.active = false;
			}
			else if (mLayout == PorchWallLayoutId.TwoWallLeft)
			{
				side1.transform.position = new Vector3(-Constants.PorchWallIconOffset, Constants.PorchFloorHeight, -mSize.z * 0.5f);

				side1.active = true;
				side2.active = false;
			}
			else if (mLayout == PorchWallLayoutId.TwoWallRight)
			{
				side2.transform.position = new Vector3(mSize.x, Constants.PorchFloorHeight, -mSize.z * 0.5f);

				side1.active = false;
				side2.active = true;
			}
			else //if (mLayout == PorchWallLayoutId.ThreeWall)
			{
				side1.transform.position = new Vector3(-Constants.PorchWallIconOffset, Constants.PorchFloorHeight, -mSize.z * 0.5f);
				side1.active = true;

				side2.transform.position = new Vector3(mSize.x + Constants.PorchWallIconOffset, Constants.PorchFloorHeight, -mSize.z * 0.5f);
				side2.active = true;
			}
		}
		else
		{
			main.active = false;
			side1.active = false;
			side2.active = false;
		}
	}

	#endregion
}
