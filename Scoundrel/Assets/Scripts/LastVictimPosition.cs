using UnityEngine;

public class LastVictimPosition : RoomCardPosition
{
	public override void SetCard(Card card)
	{
		FreeAndDestroyCard();
		base.SetCard(card);
	}
}
