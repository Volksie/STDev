using UnityEngine;
using System;
using System.Collections;

public class Stud15 : Model
{
	#region Public data

	public const string ModelName = "stud15";

	#endregion

	#region Private data

	#endregion

	#region Interface

	public Stud15()
		: this(true)
	{
	}

	public Stud15(bool createNewInstance)
		: base(ModelName, createNewInstance)
	{
	}

	public Stud15(GameObject go, bool createNewInstance)
		: base(go, createNewInstance)
	{
	}

	#endregion

	#region Implementation of ICloneable

	public override object Clone()
	{
		return new Stud15(Go, true);
	}

	#endregion

	#region Implementation
	#endregion
}
