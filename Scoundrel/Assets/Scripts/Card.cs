using UnityEngine;

public class Card : MonoBehaviour
{
	public enum Suit { Diamonds, Hearts, Clubs, Spades, }

	[SerializeField]
	private Texture texture;
	[SerializeField]
	private Suit suit;
	[SerializeField]
	private int value;
	[SerializeField]
	private Material frontMaterial;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
