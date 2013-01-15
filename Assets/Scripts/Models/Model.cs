using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Model : IModel
{
    #region Public Data
    //public const string cFastTrackName = "FastTrack";
    //public const string cMiniTrackName = "MiniTrack";
    //public const string cScreenTightName = "ScreenTight";

    //public Bounds Size
    //{
    //    get
    //    {
    //        if (mBoundsCalculated)
    //        {
    //            return mBounds;
    //        }

    //        // calc bound for all go except named "FastTrack", "MiniTrack", "ScreenTight"
    //        mBounds = new Bounds();
    //        //Renderer[] renderers = this.Go.GetComponentsInChildren<Renderer>();
    //        //foreach (var r in renderers)
    //        //{
    //        //    if (!r.name.Contains(cFastTrackName) &&
    //        //        !r.name.Contains(cMiniTrackName) &&
    //        //        !r.name.Contains(cScreenTightName))
    //        //    {
    //        //        mBounds.Encapsulate(r.bounds);
    //        //    }
    //        //}

    //        Collider[] colliders = this.Go.GetComponentsInChildren<Collider>();
    //        foreach (var r in colliders)
    //        {
    //            if (!r.name.Contains(cFastTrackName) &&
    //                !r.name.Contains(cMiniTrackName) &&
    //                !r.name.Contains(cScreenTightName))
    //            {
    //                mBounds.Encapsulate(r.bounds);
    //            }
    //        }

    //        mBoundsCalculated = true;
    //        return mBounds;
    //    }
    //}
    #endregion

    #region Private data

    private GameObject mGo;
	private ModelDisplayModeId mTrackType = ModelDisplayModeId.Default;
	private ModelAffordanceModeId mAffordanceMode = ModelAffordanceModeId.Move;
	private bool mDisplayAffordances = false;

    private Bounds mBounds = new Bounds();
    private bool mBoundsCalculated = false;

	#endregion

	#region Interface

	protected Model()
	{
	}

	protected Model(string goName, bool createNew)
		: this(GameObject.Find(goName), createNew)
	{
	}

	protected Model(GameObject modelGo, bool createNew)
	{
		GameObject go = createNew ? GameObject.Instantiate(modelGo) as GameObject : modelGo;
		SetGameObject(go);
	}

	protected void SetGameObject(GameObject go)
	{
		if (go == null)
		{
			throw new ArgumentException();
		}

		ModelBehaviour b = go.GetComponent<ModelBehaviour>();

		if (b == null)
		{
			b = go.AddComponent<ModelBehaviour>();
		}

		b.SetModel(this);
		mGo = go;
		ApplyDisplayMode(mTrackType);
	}

	protected virtual bool IsAffordanceVisible(string name, bool baseVisible)
	{
		if (baseVisible)
		{
			if (mAffordanceMode == ModelAffordanceModeId.Move)
			{
				return name.Contains(Constants.ModelPartTag_AffordanceMove);
			}
			else if (mAffordanceMode == ModelAffordanceModeId.Add)
			{
				return name.Contains(Constants.ModelPartTag_AffordanceAdd);
			}
		}

		return baseVisible;
	}

	#endregion

	#region Implementation of IModel

	public GameObject Go
	{
		get
		{
			return mGo;
		}
	}

	public Vector3 Position
	{
		get
		{
			return mGo.transform.localPosition;
		}
		set
		{
			mGo.transform.localPosition = value;
		}
	}

	public Quaternion Rotation
	{
		get
		{
			return mGo.transform.localRotation;
		}
		set
		{
			mGo.transform.localRotation = value;
		}
	}

	public Vector3 Scale
	{
		get
		{
			return mGo.transform.localScale;
		}
		set
		{
			if (mGo.name.Contains(Stud15.ModelName) || mGo.name.Contains(StudHoriz.ModelName))
			{
				foreach (Transform t in mGo.transform)
				{
					if (t.name.Contains(Constants.ModelPartTag_Affordance))
					{
						Vector3 s = t.localScale;

						if (t.name == "affordance_trans_horiz")
						{
							s.x = 0.75f / value.y;
						}
						else if (t.name == "affordance_trans_veritical")
						{
							s.x = 0.75f / value.x;
						}
						else if (t.name == "affordance_connect_left")
						{
							s.x = 0.75f / value.y;
						}
						else if (t.name == "affordance_connect_right")
						{
							s.x = 0.75f / value.y;
						}

						t.localScale = s;
					}
				}
			}

			mGo.transform.localScale = value;
		}
	}

	public bool Visible
	{
		get
		{
			return mGo.active;
		}
		set
		{
			if (value)
			{
				mGo.active = true;
				ApplyDisplayMode(mTrackType);
			}
			else
			{
				mGo.SetActiveRecursively(false);
			}
		}
	}

	public ModelDisplayModeId DisplayMode
	{
		get
		{
			return mTrackType;
		}
		set
		{
			if (mTrackType != value)
			{
				ApplyDisplayMode(value);
				mTrackType = value;
			}
		}
	}

	public ModelAffordanceModeId AffordanceMode
	{
		get
		{
			return mAffordanceMode;
		}
		set
		{
			if (mAffordanceMode != value)
			{
				if (mDisplayAffordances)
				{
					foreach (Transform t in mGo.transform)
					{
						if (t.name.Contains(Constants.ModelPartTag_Affordance))
						{
							t.gameObject.active = IsAffordanceVisible(t.name, mDisplayAffordances);
						}
					}
				}

				mAffordanceMode = value;
			}
		}
	}

	public bool DisplayAffordances
	{
		get
		{
			return mDisplayAffordances;
		}
		set
		{
			if (mDisplayAffordances != value)
			{
				foreach (Transform t in mGo.transform)
				{
					if (t.name.Contains(Constants.ModelPartTag_Affordance))
					{
						t.gameObject.active = IsAffordanceVisible(t.name, value);
					}
				}

				mDisplayAffordances = value;
			}
		}
	}

	#endregion

	#region Implementation of IEnumerable

	public virtual IEnumerator<IModel> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}

	#endregion

	#region Implementation of ICloneable

	public virtual object Clone()
	{
		throw new NotImplementedException();
	}

	#endregion

	#region Implementation of IDisposable

	public virtual void Dispose()
	{
		if (mGo != null)
		{
			GameObject.Destroy(mGo);
			mGo = null;
		}
	}

	#endregion

	#region Implementation

	private void ApplyDisplayMode(ModelDisplayModeId displayMode)
	{
		foreach (Transform t in mGo.transform)
		{
			if (t.name.Contains(Constants.ModelPartTag_ScreenTight))
			{
				t.gameObject.active = (displayMode == ModelDisplayModeId.ScreenTight);
			}
			else if (t.name.Contains(Constants.ModelPartTag_FastTrack))
			{
				t.gameObject.active = (displayMode == ModelDisplayModeId.FastTrack);
			}
			else if (t.name.Contains(Constants.ModelPartTag_MiniTrack))
			{
				t.gameObject.active = (displayMode == ModelDisplayModeId.MiniTrack);
			}
			else if (t.name.Contains(Constants.ModelPartTag_Affordance))
			{
				t.gameObject.active = IsAffordanceVisible(t.name, mDisplayAffordances);
			}
			else
			{
				t.gameObject.active = mGo.active;
			}
		}
	}

	#endregion
}
