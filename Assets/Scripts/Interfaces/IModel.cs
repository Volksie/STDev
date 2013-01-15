using UnityEngine;
using System;
using System.Collections.Generic;

public enum ModelDisplayModeId
{
	Default,
	ScreenTight,
	FastTrack,
	MiniTrack
}

public enum ModelAffordanceModeId
{
	All, 
	Move, 
	Add
}

public interface IModel : IEnumerable<IModel>, ICloneable, IDisposable
{
	/// <summary>
	/// 
	/// </summary>
	GameObject Go { get; }

	/// <summary>
	/// 
	/// </summary>
	Vector3 Position { get; set; }

	/// <summary>
	/// 
	/// </summary>
	Quaternion Rotation { get; set; }

	/// <summary>
	/// 
	/// </summary>
	Vector3 Scale { get; set; }

	/// <summary>
	/// 
	/// </summary>
	bool Visible { get; set; }

	/// <summary>
	/// 
	/// </summary>
	ModelDisplayModeId DisplayMode { get; set; }

	/// <summary>
	/// 
	/// </summary>
	ModelAffordanceModeId AffordanceMode { get; set; }

	/// <summary>
	/// 
	/// </summary>
	bool DisplayAffordances { get; set; }
}
