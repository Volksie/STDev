using UnityEngine;
using System;
using System.Collections;

public class StudHoriz : Model
{
	#region Public data

	public const string ModelName = "stud-horiz";

	#endregion

	#region Private data

	#endregion

	#region Interface

	public StudHoriz()
		: this(true)
	{
	}

	public StudHoriz(bool createNewInstance)
		: base(ModelName, createNewInstance)
	{
	}

	public StudHoriz(GameObject go, bool createNewInstance)
		: base(go, createNewInstance)
	{
	}

	#endregion

	#region Implementation of ICloneable

	public override object Clone()
	{
		return new StudHoriz(Go, true);
	}

	#endregion

	#region Implementation
	#endregion
}
