using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	private const int MAX_INTERACTIONS_PER_TURN = 3;
	
	[SerializeField]
	private RoomCardPosition RoomCardPosition1;
	[SerializeField] 
	private RoomCardPosition RoomCardPosition2;
	[SerializeField] 
	private RoomCardPosition RoomCardPosition3;
	[SerializeField] 
	private RoomCardPosition RoomCardPosition4;

	private DeckManager deckManager;
	private int numInteractions;
	private List<Card> deck;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		deckManager = FindFirstObjectByType<DeckManager>();

		InputManager.OnSelectRoomPosition += OnSelectRoomPosition;

		StartGame();
	}

	private void OnDestroy()
	{
		InputManager.OnSelectRoomPosition -= OnSelectRoomPosition;
	}

	private	void StartGame()
	{
		deck = deckManager.LoadDeck();

		InitTurn();
	}

	private void InitTurn()
	{
		Debug.Log("init turn");

		numInteractions = MAX_INTERACTIONS_PER_TURN;

		MoveCard(RoomCardPosition1);
		MoveCard(RoomCardPosition2);
		MoveCard(RoomCardPosition3);
		MoveCard(RoomCardPosition4);
	}

	private void MoveCard(RoomCardPosition roomPosition)
	{
		if (roomPosition.IsBusy())
		{
			return;
		}

		var card = PopCard();

		if (card != null)
		{
			card.Flip();
			roomPosition.SetCard(card);
		}
	}

	private Card PopCard()
	{
		if (deck.Count == 0)
		{
			return null;
		}

		int k = Random.Range(0, deck.Count);
		var card = deck[k];
		deck.Remove(card);

		return card;
	}

	private void OnSelectRoomPosition(object sender, RoomCardPosition roomPosition)
	{
		if (!roomPosition.IsBusy())
		{
			return;
		}

		HandleCard(roomPosition);
		numInteractions--;
		if (numInteractions == 0)
		{
			InitTurn();
		}
	}

	private void HandleCard(RoomCardPosition roomPosition)
	{
		var card = roomPosition.GetCard();
		Debug.Log($"clicked on {card.GetSuit()} {card.GetValue()}");
		roomPosition.Free();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
