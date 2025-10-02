/*
Originial Coder: Zackery E.
Recent Coder: Owynn A.
Recent Changes: Sliding Healthbar Update
Last date worked on: 9/30/2025
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class HealthBarBehaviour : MonoBehaviour
{
    public IntData health;
    public Slider slider;
    public UnityEvent onDamage, onHeal, onDepleted;
    public int maxHealth;
    public int currentBarHealth;
    public void Awake()
    {
        Debug.Log("Healthbar Awake");
		slider.maxValue = maxHealth;
		health.value = maxHealth;
        currentBarHealth = health.value;
        UpdateSlider();
    }//End Awake

    public void UpdateHealthBar()
    {
        if (currentBarHealth < 0)
        {
            currentBarHealth = 0;
        }

        RunHealthEvents();
        currentBarHealth = health.value;
        UpdateSlider();
    }//End UpdateHealth

    private void RunHealthEvents()
    {
        if (health.value <= 0) //health is depleted
        {
            onDepleted.Invoke();
        }
        else if (currentBarHealth < health.value) //health increased
        {
            onHeal.Invoke();
        }
        else if (currentBarHealth > health.value) //health is decreased
        {
            onDamage.Invoke();
        } 
    }//End RunHelathEvents

    public void RunUpdateSlider()
    {
        StartCoroutine(UpdateSlider());
    }//End RunUpdateSlider

	private IEnumerator UpdateSlider()
	{
		float speed = 15f;

    	while (slider.value > health.value)
    	{
        	slider.value -= Time.deltaTime * speed;;
        	yield return null;
    	}//end while
	}//end UpdateSlider
}//end class