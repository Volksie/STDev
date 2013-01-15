using UnityEngine;
using System.Collections;

public class ModelBehaviour : MonoBehaviour
{
	#region Public data

	public IModel Model
	{
		get
		{
			return mModel;
		}
	}

	#endregion

	#region Private data

	private IModel mModel;

	#endregion

	#region Interface

	public void SetModel(IModel m)
	{
		mModel = m;
	}

	#endregion

	#region Implementation of MonoBehaviour

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{

	}

	//protected virtual void OnFingerDown(TBFingerDown fd)
	//{
	//    AppRoot.Instance.SendMessage(Constants.MsgModelClicked, gameObject);
	//}

	//protected virtual void OnMouseUpAsButton()
	//{
	//    AppRoot.Instance.SendMessage(Constants.MsgModelClicked, gameObject);
	//}

	#endregion

	#region Implementation
	#endregion
}
