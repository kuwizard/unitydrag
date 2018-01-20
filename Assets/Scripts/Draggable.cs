using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	public delegate void Dragging();
	public static event Dragging DraggingStarted;
	public static event Dragging DraggingFinished;

	private Transform startingParent = null;
	private Transform parentToReturn = null;
	private Vector2 delta;

	public void OnBeginDrag(PointerEventData eventData)
	{
		startingParent = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent);
		delta = new Vector2(this.transform.position.x - eventData.position.x, this.transform.position.y - eventData.position.y);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		DraggingStarted();
		Debug.Log ("Draggable " + this.name + " is listening to events");
		DropZone.PointerEntered += SetParentToReturn;
		DropZone.PointerExited += ResetParent;
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = eventData.position + delta;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (parentToReturn == null) {
			Debug.Log ("ParentToReturn was null :( ");
			parentToReturn = startingParent;
		}
		Debug.Log ("Returning to " + parentToReturn);
		this.transform.SetParent (parentToReturn);

		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		DraggingFinished();
		Debug.Log ("Fuck this");
		DropZone.PointerEntered -= SetParentToReturn;
		DropZone.PointerExited -= ResetParent;
	}

	void SetParentToReturn(DropZone parent)
	{
		if (parent == null) {
			Debug.Log ("parent has been reset");
			parentToReturn = null;
		} else {
			Debug.Log ("parentToReturn set to: " + parent.name);
			parentToReturn = parent.transform;
		}
	}

	void ResetParent(DropZone parent)
	{
		SetParentToReturn (null);
	}
}
