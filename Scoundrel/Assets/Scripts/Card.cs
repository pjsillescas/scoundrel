using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
	public enum Suit { Diamonds, Hearts, Clubs, Spades, }

	[SerializeField]
	private MeshRenderer front;
	[SerializeField]
	private MeshRenderer back;

	private Material frontMaterial;
	private Material backMaterial;

	private Suit suit;
	private int value;

	private void Awake()
	{
		frontMaterial = front.material;
		backMaterial = back.material;
	}

	public Card Load(CardData data, Texture backTexture)
	{
		suit = data.suit;
		value = data.value;
		frontMaterial.SetTexture("_MainTex", data.texture);
		backMaterial.SetTexture("_MainTex", backTexture);

		return this;
	}

	public void Flip()
	{
		StartCoroutine(FlipCoroutine());
	}

	private IEnumerator FlipCoroutine()
	{
		var goForward = Mathf.Abs(transform.rotation.z) <= 0.01f;
		var zTarget = goForward ? 180 : 0;
		var angle = goForward ? 9 : -9;
		angle *= 2;
		var currentAngle = goForward ? 0 : 180;
		while (Mathf.Abs(currentAngle - zTarget) > 0)
		{
			//Debug.Log("rotate");
			transform.Rotate(0, 0, angle);
			currentAngle += angle;
			yield return new WaitForSeconds(0.1f);
		}
		
		yield return null;
	}



	public Suit GetSuit() => suit;
	public int GetValue() => value;

	public int GetAttackValue()
	{
		return value == 1 ? 14 : value;
	}
}
