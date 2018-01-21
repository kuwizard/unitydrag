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

	public Transform Card;
	private Transform _placeholder;
	private bool _dragginInProgress;

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
			_placeholder = Instantiate(Card);
			for (int i = 0; i < _placeholder.childCount; i++)
			{
				Destroy(_placeholder.GetChild(i).gameObject);
			}

			Destroy(_placeholder.GetComponent<Image>());
			_placeholder.SetParent(transform);
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		if(PointerExited != null)
			PointerExited ();
		if(_placeholder != null)
			Destroy(_placeholder.gameObject);
	}

	private void EnableListening(String name)
	{
		_dragginInProgress = true;
	}

	private void DisableListening(String name) {
		if(_placeholder != null)
			Destroy(_placeholder.gameObject);
		_dragginInProgress = false;
	}
}
