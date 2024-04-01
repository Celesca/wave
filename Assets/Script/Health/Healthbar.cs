using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	[SerializeField] private Health playerHealth;
	[SerializeField] private Image totalhealthBar;
	[SerializeField] private Image currenthealthBar;
    [SerializeField] private float a ;

	private void Start()
	{
		totalhealthBar.fillAmount = playerHealth.currentHealth / a;
	}
	
	private void Update()
	{
		currenthealthBar.fillAmount = playerHealth.currentHealth / a;
	}
}
