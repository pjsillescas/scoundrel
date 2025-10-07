using System;
using UnityEngine;

[Serializable]
public struct CardData
{
	[SerializeField]
	public Card.Suit suit;
	public int value;
	[SerializeField]
	public Texture texture;
}
