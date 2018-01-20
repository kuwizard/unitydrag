using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public delegate void Entered(Transform dropZone);
	public static event Entered PointerEntered;
	public delegate void Exited();
	public static event Exited PointerExited;

	private bool _draggingInProgress;

	public void OnEnable(){
		Draggable.DraggingStarted += EnableListening;
		Draggable.DraggingFinished += DisableListening;
	}

	public void OnDisable(){
		Draggable.DraggingStarted -= EnableListening;
		Draggable.DraggingFinished += DisableListening;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if(_draggingInProgress && PointerEntered != null)
			PointerEntered (transform);
	}

	public void OnPointerExit(PointerEventData eventData) {
		if(_draggingInProgress && PointerExited != null)
			PointerExited ();
	}

	private void EnableListening() {
		Debug.Log ("Dropzone "+ this.name + " started to listen");
		_draggingInProgress = true;
	}

	private void DisableListening() {
		Debug.Log ("Dropzone "+ this.name + " stopped to listen");
		_draggingInProgress = false;
	}
}
