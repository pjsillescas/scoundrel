using UnityEngine;

public class RoomCardPosition : MonoBehaviour
{
	private Card card;

	private void Awake()
	{
		card = null;
	}
	public bool IsBusy() => card != null;

	public void SetCard(Card card)
	{
		card.transform.position = transform.position;
		this.card = card;
	}

	public void Free()
	{
		Destroy(card.gameObject);
		card= null;
	}

	public Card GetCard() => card;
}
