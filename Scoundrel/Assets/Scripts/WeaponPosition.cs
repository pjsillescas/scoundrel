using UnityEngine;

public class WeaponPosition : RoomCardPosition
{
	[SerializeField]
	private LastVictimPosition lastVictimPosition;

	public override void Free()
	{
		base.Free();
		lastVictimPosition.FreeAndDestroyCard();
	}

	public override void FreeAndDestroyCard()
	{
		base.FreeAndDestroyCard();
		lastVictimPosition.FreeAndDestroyCard();
	}

	public override void SetCard(Card card)
	{
		FreeAndDestroyCard();
		base.SetCard(card);
	}

	public int GetDefense()
	{
		var card = GetCard();
		return card != null ? card.GetValue() : 0;
	}

	public void SetLastVictim(Card lastVictim)
	{
		lastVictimPosition.SetCard(lastVictim);
	}

	public bool CanBeUsed(Card card)
	{
		var weaponCard = GetCard();
		if(weaponCard == null)
		{
			return false;
		}

		var victimCard = lastVictimPosition.GetCard();
		return victimCard == null || victimCard.GetAttackValue() >= card.GetValue();
	}
}
