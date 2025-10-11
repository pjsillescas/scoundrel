using UnityEngine;

public class RoomCardPosition
{
	private Transform position;
	private bool isBusy;

	public RoomCardPosition(Transform position)
	{
		this.position = position;
		isBusy = false;
	}

	public bool IsBusy() => isBusy;

	public void SetCard(Card card)
	{
		card.transform.position = position.position;
		isBusy = true;
	}

	public void Free()
	{
		isBusy = false;
	}
}
