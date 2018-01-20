using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	public delegate void Dragging();
	public static event Dragging DraggingStarted;
	public static event Dragging DraggingFinished;
	private Parent _parent;

	private Vector2 _delta;

	public void OnBeginDrag(PointerEventData eventData)
	{
		_parent = new Parent(transform.parent);
		transform.SetParent (transform.parent.parent);
		_delta = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		if (DraggingStarted != null) DraggingStarted();
		Debug.Log ("Draggable " + name + " is listening to events");
		DropZone.PointerEntered += _parent.SetPotential;
		DropZone.PointerExited += _parent.Reset;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position + _delta;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent (_parent.Get());

		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		if (DraggingFinished != null) DraggingFinished();
		Debug.Log ("Fuck this");
		DropZone.PointerEntered -= _parent.SetPotential;
		DropZone.PointerExited -= _parent.Reset;
	}
}
