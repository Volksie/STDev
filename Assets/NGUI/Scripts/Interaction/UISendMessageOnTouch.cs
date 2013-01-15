using UnityEngine;

/// <summary>
/// When clicked, call the specified function on the target's attached scripts.
/// If no target was specified, it will use the game object this script is attached to.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Send Message (OnClick)")]
public class UISendMessageOnTouch : MonoBehaviour
{
	public GameObject target;
	public bool includeChildren = false;
	public string functionName = "OnSendMessage";

	void OnPress (bool pressed)
	{
			GameObject go = (target != null) ? target : gameObject;

			if (includeChildren)
			{
				Transform[] transforms = go.GetComponentsInChildren<Transform>();

				foreach (Transform t in transforms)
				{
					t.gameObject.SendMessage(functionName, pressed, SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				go.SendMessage(functionName, pressed, SendMessageOptions.DontRequireReceiver);
			}
		
	}
}