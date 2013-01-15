using System;
using System.Collections.Generic;

public class AppState_Default : AppState
{
	#region Public data
	#endregion

	#region Private data
	#endregion

	#region Interface

	public AppState_Default()
		: base(AppStateId.Default)
	{
	}

	#endregion

	#region Implementation of AppState

	public override void OnActivate(object param)
	{
		base.OnActivate(param);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();
	}

	#endregion
}
