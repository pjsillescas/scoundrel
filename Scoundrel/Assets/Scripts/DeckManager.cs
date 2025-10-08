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

	private List<Card> deck;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		LoadDeck();
	}

	private void LoadDeck()
	{
		deck = new();

		deckData.Data.Select(data => (Instantiate(CardPrefab).GetComponent<Card>()).Load(data, deckData.backTexture)).ToList().ForEach(deck.Add);

		var displacement = Vector3.zero;
		deck.ForEach(card => {
			displacement += new Vector3(10,0,0);
			card.transform.position = displacement;
			card.transform.Rotate(90, 0, 0);
		});

		StartCoroutine(WaitAndFlipDeck());
	}

	private IEnumerator WaitAndFlipDeck()
	{
		yield return new WaitForSeconds(2f);

		deck.ForEach(card => card.Flip());
		yield return null;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public List<Card> GetDeck() => deck;
}
