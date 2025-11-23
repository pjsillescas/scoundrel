using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RoomCardPosition : MonoBehaviour
{
	private const float MIN_SQRT_DISTANCE = 0.1f;

	[SerializeField]
	private float translationSpeed = 50.0f;

	private Card card;

	private void Awake()
	{
		card = null;
	}
	public bool IsBusy() => card != null;

	public virtual void SetCard(Card card)
	{
		this.card = card;

		var direction = (transform.position - card.transform.position).normalized;
		StartCoroutine(TranslateCard(direction));
	}

	private IEnumerator TranslateCard(Vector3 direction)
	{
		while((card.transform.position - transform.position).sqrMagnitude > MIN_SQRT_DISTANCE)
		{
			card.transform.position = card.transform.position + direction * translationSpeed * Time.deltaTime;
			yield return new WaitForNextFrameUnit();
		}

		card.transform.position = transform.position;
		yield return null;
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
