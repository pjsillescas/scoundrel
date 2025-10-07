using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckDefinition", menuName = "Scriptable Objects/DeckDefinition")]
public class DeckDefinition : ScriptableObject
{
	/*
	[Serializable]
	public class CardData
	{
		[SerializeField]
		public Card.Suit suit;
		public int value;
		public Texture texture;
	}
	*/
	//[field: SerializeField]
	public List<CardData> Data;

	[SerializeField]
	public Texture backTexture;
}
