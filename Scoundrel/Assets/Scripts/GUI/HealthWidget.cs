using System.Collections;
using TMPro;
using UnityEngine;

public class HealthWidget : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI HealthText;
	[SerializeField]
	private TextMeshProUGUI DamageText;
	[SerializeField]
	private Color DamageInitialColor;
	[SerializeField]
	private Color DamageFinalColor;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		GameManager.OnHealthChanged += OnHealthChanged;
		DamageText.color = DamageFinalColor;
	}

	private void OnDestroy()
	{
		GameManager.OnHealthChanged -= OnHealthChanged;
	}

	private void OnHealthChanged(object sender, GameManager.HealthData healthData)
	{
		HealthText.text = healthData.health.ToString();

		if (healthData.damage != 0)
		{
			var prefix = healthData.damage < 0 ? "-" : "+";
			DamageText.text = prefix + Mathf.Abs(healthData.damage).ToString();
			StartCoroutine(ShowDamageText());
		}
	}

	private IEnumerator ShowDamageText()
	{
		for(int i=0; i< 255;i+=20)
		{
			DamageText.color = Color.Lerp(DamageInitialColor, DamageFinalColor, i / 255f);
			yield return new WaitForSeconds(0.1f);
		}

		DamageText.color = DamageFinalColor;

		yield return null;
	}
}
