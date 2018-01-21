using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public delegate void Entered(Transform dropZone);
	public static event Entered PointerEntered;
	public delegate void Exited();
	public static event Exited PointerExited;

	public Transform card;
	private bool _dragginInProgress;
    private PlaceHolder _placeHolder;

	public void OnEnable(){
		Draggable.DraggingStarted += EnableListening;
		Draggable.DraggingFinished += DisableListening;
	}

	public void OnDisable(){
		Draggable.DraggingStarted -= EnableListening;
		Draggable.DraggingFinished += DisableListening;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if(PointerEntered != null)
			PointerEntered (transform);
               
		if (_dragginInProgress)
		{
            var placeHolder = new PlaceHolder(card, transform);
        }
	}

	public void OnPointerExit(PointerEventData eventData) {
		if(PointerExited != null)
			PointerExited ();
		if(_placeHolder != null)
			Destroy(_placeHolder);
	}

	private void EnableListening(String name)
	{
		_dragginInProgress = true;
	}

	private void DisableListening(String name) {
		if(_placeHolder != null)
			Destroy(_placeHolder);
		_dragginInProgress = false;
	}
}
