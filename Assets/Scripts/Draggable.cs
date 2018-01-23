using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
	public delegate void Dragging(String name);
	public static event Dragging DraggingStarted;
	public static event Dragging DraggingFinished;
	private Parent _parent;
	private Coords _coords;

	private Vector2 _delta;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (DraggingStarted != null) DraggingStarted(name);
		
		_parent = new Parent(transform.parent);
		transform.SetParent (_parent.Get().parent);
		_delta = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		DropZone.PointerEntered += _parent.SetPotential;
		DropZone.PointerExited += _parent.Reset;
		DropZone.XCoords += _parent.SetXCoords;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position + _delta;
		
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.SetParent (_parent.Get());

		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		if (DraggingFinished != null) DraggingFinished(name);
		DropZone.PointerEntered -= _parent.SetPotential;
		DropZone.PointerExited -= _parent.Reset;
	}
}
