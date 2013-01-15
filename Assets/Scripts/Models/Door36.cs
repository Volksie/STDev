using UnityEngine;
using System;
using System.Collections;

public class Door36 : Model
{
	#region Public data

	public const string ModelName = "door36";

	#endregion

	#region Private data

    

	#endregion

	#region Interface

	public Door36()
		: this(true)
	{
	}

	public Door36(bool createNewInstance)
		: base(ModelName, createNewInstance)
	{
	}

	public Door36(GameObject go, bool createNewInstance)
		: base(go, createNewInstance)
	{
	}

	#endregion

	#region Implementation of ICloneable

	public override object Clone()
	{
		return new Door36(Go, true);
	}

	#endregion

	#region Implementation
	#endregion
}
