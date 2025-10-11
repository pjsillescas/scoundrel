using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	public class HealthData
	{
		public int health;
		public int damage;
	}

	private const int MAX_INTERACTIONS_PER_TURN = 3;
	private const int MAX_HEALTH = 20;
	public static event EventHandler<HealthData> OnHealthChanged;

	[SerializeField]
	private RoomCardPosition RoomCardPosition1;
	[SerializeField] 
	private RoomCardPosition RoomCardPosition2;
	[SerializeField] 
	private RoomCardPosition RoomCardPosition3;
	[SerializeField] 
	private RoomCardPosition RoomCardPosition4;

	[SerializeField]
	private WeaponPosition WeaponPosition;
	
	private DeckManager deckManager;
	private int numInteractions;
	private List<Card> deck;
	private int health;
	private bool usedPotion;

	private void OnHealthChangedDebug(object sender, HealthData data)
	{
		Debug.Log($"{data.health} ({data.damage})");
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		deckManager = FindFirstObjectByType<DeckManager>();

		InputManager.OnSelectRoomPosition += OnSelectRoomPosition;

		OnHealthChanged += OnHealthChangedDebug;
		StartGame();
	}

	private void OnDestroy()
	{
		InputManager.OnSelectRoomPosition -= OnSelectRoomPosition;
		OnHealthChanged -= OnHealthChangedDebug;
	}

	private	void StartGame()
	{
		RoomCardPosition1.FreeAndDestroyCard();
		RoomCardPosition2.FreeAndDestroyCard();
		RoomCardPosition3.FreeAndDestroyCard();
		RoomCardPosition4.FreeAndDestroyCard();

		deck = deckManager.LoadDeck();
		health = MAX_HEALTH;

		InitTurn();
	}

	private void InitTurn()
	{
		Debug.Log($"init turn health: {health} remaining cards: {deck.Count}");

		if(deck.Count <= 0)
		{
			GameOver(true);
		}

		usedPotion = false;

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

		var card = deck[deck.Count - 1];
		deck.Remove(card);

		return card;
	}

	private void OnSelectRoomPosition(object sender, RoomCardPosition roomPosition)
	{
		if (!roomPosition.IsBusy())
		{
			return;
		}

		numInteractions--;
		HandleCard(roomPosition);
		
		Debug.Log($"numInteractions {numInteractions}");
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

		switch (card.GetSuit())
		{
			case Card.Suit.Diamonds:
				EquipWeapon(card);
				break;
			case Card.Suit.Hearts:
				DrinkPotion(card);
				break;
			case Card.Suit.Clubs:
			case Card.Suit.Spades:
			default:
				DefeatEnemy(card);
				break;
		}
	}

	private void EquipWeapon(Card card)
	{
		Debug.Log("setting weapon");
		WeaponPosition.SetCard(card);
	}

	private void DrinkPotion(Card card)
	{
		if(usedPotion)
		{
			Debug.Log("potion unused");
			Destroy(card.gameObject);
			return;
		}

		usedPotion = true;
		var addedHealth = card.GetValue();
		Destroy(card.gameObject);
		health = Mathf.Clamp(health + addedHealth, 0, MAX_HEALTH);
		OnHealthChanged?.Invoke(this, new HealthData() { health = health, damage = addedHealth });
	}

	private	int GetAttack(int value)
	{
		return value == 1 ? 14 : value;
	}
	private void DefeatEnemy(Card card)
	{
		var canUseWeapon = WeaponPosition.CanBeUsed(card);

		int defense = canUseWeapon ? WeaponPosition.GetDefense() : 0;
		int damage = Mathf.Clamp(GetAttack(card.GetValue()) - defense, 0, 15);
		health = Mathf.Clamp(health - damage, 0, MAX_HEALTH);
		OnHealthChanged?.Invoke(this, new HealthData() { health = health, damage = -damage });

		if (canUseWeapon)
		{
			WeaponPosition.SetLastVictim(card);
		}
		else
		{
			Destroy(card.gameObject);
		}

		if (health <= 0)
		{
			GameOver(false);
		}
	}

	private void GameOver(bool isWinGame)
	{
		Debug.Log(isWinGame ? "you won" : "you lost");
		StartGame();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
