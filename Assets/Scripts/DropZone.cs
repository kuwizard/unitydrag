using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public delegate void Entered(DropZone dropZone);
	public static event Entered PointerEntered;
	public static event Entered PointerExited;

	private bool DraggingInProgress = false;

	public void OnEnable(){
		Draggable.DraggingStarted += EnableListening;
		Draggable.DraggingFinished += DisableListening;
	}

	public void OnDisable(){
		Draggable.DraggingStarted -= EnableListening;
		Draggable.DraggingFinished += DisableListening;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if(DraggingInProgress)
			PointerEntered (this);
	}

	public void OnPointerExit(PointerEventData eventData) {
		if(DraggingInProgress)
			PointerExited (this);
	}

	private void EnableListening() {
		Debug.Log ("Dropzone "+ this.name + " started to listen");
		DraggingInProgress = true;
	}

	private void DisableListening() {
		Debug.Log ("Dropzone "+ this.name + " stopped to listen");
		DraggingInProgress = false;
	}
}
