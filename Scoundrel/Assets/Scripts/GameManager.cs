using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Transform RoomCardPosition1;
	[SerializeField] 
	private Transform RoomCardPosition2;
	[SerializeField] 
	private Transform RoomCardPosition3;
	[SerializeField] 
	private Transform RoomCardPosition4;

	private RoomCardPosition roomPosition1;
	private RoomCardPosition roomPosition2;
	private RoomCardPosition roomPosition3;
	private RoomCardPosition roomPosition4;

	private DeckManager deckManager;

	private List<Card> deck;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		roomPosition1 = new RoomCardPosition(RoomCardPosition1);
		roomPosition2 = new RoomCardPosition(RoomCardPosition2);
		roomPosition3 = new RoomCardPosition(RoomCardPosition3);
		roomPosition4 = new RoomCardPosition(RoomCardPosition4);

		deckManager = FindFirstObjectByType<DeckManager>();

		StartGame();
	}

	private	void StartGame()
	{
		deck = deckManager.LoadDeck();

		MoveCard(roomPosition1);
		MoveCard(roomPosition2);
		MoveCard(roomPosition3);
		MoveCard(roomPosition4);
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

		int k = Random.Range(0, deck.Count + 1);
		var card = deck[k];
		deck.Remove(card);

		return card;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
