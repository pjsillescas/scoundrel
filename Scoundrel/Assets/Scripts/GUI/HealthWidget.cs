using TMPro;
using UnityEngine;

public class HealthWidget : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI HealthText;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		GameManager.OnHealthChanged += OnHealthChanged;
	}

	private void OnDestroy()
	{
		GameManager.OnHealthChanged -= OnHealthChanged;
	}

	private void OnHealthChanged(object sender, GameManager.HealthData healthData)
	{
		HealthText.text = healthData.health.ToString();
	}
}
