using System;
using System.Collections.Generic;

public class PorchElement
{
	#region Public data

	public virtual float MinOffset
	{
		get
		{
			return 0;
		}
	}

	public virtual float MaxOffset
	{
		get
		{
			return 0;
		}
	}

	public virtual float Offset
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual bool IsHorizontal
	{
		get
		{
			return false;
		}
	}

	#endregion

	#region Private data
	#endregion

	#region Interface

	public PorchElement()
	{
	}

	public virtual void Edit(float newValue)
	{
		Offset = newValue;
	}

	public virtual PorchElement Clone(bool leftSide)
	{
		return null;
	}

	public virtual void Delete()
	{
		AppRoot.Scene.SelectedWallElement = null;
	}

	#endregion

	#region Implementation
	#endregion
}
