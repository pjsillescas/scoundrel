using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class DeckManager : MonoBehaviour
{
	[SerializeField]
	private DeckDefinition deckData;

	[SerializeField]
	private GameObject CardPrefab;

	[SerializeField]
	private Transform DeckPosition;

	private List<Card> deck;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		//LoadDeck();
	}

	public List<Card> LoadDeck()
	{
		var validSuites = new List<Card.Suit>() { Card.Suit.Spades, Card.Suit.Clubs };
		deck = new();

		deckData.Data.Where(data => validSuites.Contains(data.suit) || data.value > 10)
			.Select(data => (Instantiate(CardPrefab).GetComponent<Card>()).Load(data, deckData.backTexture)).ToList().ForEach(deck.Add);

		deck.Shuffle();
		var displacement = Vector3.zero;
		deck.ForEach(card => {
			displacement += new Vector3(0,0,-0.1f);
			card.transform.position = displacement + DeckPosition.position;
			card.transform.Rotate(90, 0, 0);
		});

		return deck;
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	public List<Card> GetDeck() => deck;
}
