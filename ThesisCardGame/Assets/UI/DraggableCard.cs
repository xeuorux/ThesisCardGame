﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static DraggableCard cardBeingDragged;

	public Camera mainCamera;

	public Canvas canvas;
	public GameObject playerUIArea;
	private RectTransform thisRectTransform;
	private CanvasGroup thiscanvasGroup;

	public Card cardThisRenders;
	private ClientSideGameManager tcgGameManager;

	public Text cardName;
	public Text cardText;
	public Text cardCost;

	public void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		tcgGameManager = GameObject.FindObjectOfType<ClientSideGameManager>();
		thisRectTransform = GetComponent<RectTransform>();
		thiscanvasGroup = GetComponent<CanvasGroup>();
    }

	public void UpdateCardDisplay()
	{
		if (cardThisRenders == null)
		{
			Debug.LogError("Can't update card display when this draggable card has no reference to a card.");
		}
		cardName.text = cardThisRenders.BaseDefinition.CardName;
		cardText.text = "Not implemented yet.";
		cardCost.text = (cardThisRenders is SpellCard) ? (((SpellCard)cardThisRenders).BaseDefinition.CardName).ToString() : "";
    }

	public void OnBeginDrag(PointerEventData eventData)
	{
		cardBeingDragged = this;
		thisRectTransform.SetParent(canvas.transform);
		thiscanvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 mouseScreenPosition = Input.mousePosition;
		Vector3 mouseViewportPosition = mainCamera.ScreenToViewportPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0));

		thisRectTransform.anchorMin = new Vector2(mouseViewportPosition.x, mouseViewportPosition.y);
		thisRectTransform.anchorMax = new Vector2(mouseViewportPosition.x, mouseViewportPosition.y);
		thisRectTransform.anchoredPosition = Vector2.zero;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		thisRectTransform.SetParent(playerUIArea.transform);
		thiscanvasGroup.blocksRaycasts = true;
	}

	public void CardDroppedInPlayArea()
	{
		tcgGameManager.TryPlayCard(cardThisRenders);
		cardBeingDragged = null;
	}
}