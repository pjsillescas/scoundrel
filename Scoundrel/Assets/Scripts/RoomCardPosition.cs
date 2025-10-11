using UnityEngine;

public class RoomCardPosition : MonoBehaviour
{
	private Card card;

	private void Awake()
	{
		card = null;
	}
	public bool IsBusy() => card != null;

	public virtual void SetCard(Card card)
	{
		card.transform.position = transform.position;
		this.card = card;
	}

	public virtual void Free()
	{
		card = null;
	}

	public virtual void FreeAndDestroyCard()
	{
		if (card != null)
		{
			Destroy(card.gameObject);
			Free();
		}
	}

	public Card GetCard() => card;
}
