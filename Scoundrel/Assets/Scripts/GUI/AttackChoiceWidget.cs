using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackChoiceWidget: MonoBehaviour
{
	public enum AttackType
	{
		Melee, Weapon,
	}

	public GameObject WidgetPanel;
	public Button MeleeButton;
	public Button WeaponButton;

	private AttackType? choice = null; // null = no choice yet

	void Awake()
	{
		WidgetPanel.SetActive(false);
		MeleeButton.onClick.AddListener(() => MakeChoice(AttackType.Melee));
		WeaponButton.onClick.AddListener(() => MakeChoice(AttackType.Weapon));
	}

	void MakeChoice(AttackType type)
	{
		choice = type;
		WidgetPanel.SetActive(false);
	}

	public IEnumerator ChooseAttackType()
	{
		choice = null;
		WidgetPanel.SetActive(true);

		// Wait until player makes a choice
		yield return new WaitUntil(() => choice.HasValue);
	}

	public AttackType GetChoice()
	{
		return choice ?? AttackType.Melee;
	}

	public IEnumerator ChooseAttackType(Action<AttackType> onComplete)
	{
		choice = null;
		WidgetPanel.SetActive(true);
		yield return new WaitUntil(() => choice.HasValue);
		onComplete(choice ?? AttackType.Melee);
	}
}
