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
    public float updateSpeed = 15f;
    public int maxHealth;
    public int currentBarValue;
    public void Awake()
    {
        Debug.Log("Healthbar Awake");
		slider.maxValue = maxHealth;
		health.value = maxHealth;
        currentBarValue = health.value;
        UpdateHealthBar();
    }//End Awake

    public void UpdateHealthBar()
    {
        if (currentBarValue < 0)
        {
            currentBarValue = 0;
        }

        RunHealthEvents();
        currentBarValue = health.value;
        RunUpdateSlider();
        Debug.Log("Healthbar Update");
    }//End UpdateHealth

    private void RunHealthEvents()
    {
        if (health.value <= 0) //health is depleted
        {
            Debug.Log("Healthbar On Death");
            onDepleted.Invoke();
        }
        else if (currentBarValue < health.value) //health increased
        {
            Debug.Log("Healthbar On Health");
            onHeal.Invoke();
        }
        else if (currentBarValue > health.value) //health is decreased
        {
            Debug.Log("Healthbar On Damage");
            onDamage.Invoke();
        } 
    }//End RunHelathEvents

    public void RunUpdateSlider()
    {
        StartCoroutine(UpdateSlider());
    }//End RunUpdateSlider

	private IEnumerator UpdateSlider()
	{

        while (Mathf.Abs(slider.value - health.value) > 0.01f)
        {
            slider.value = Mathf.MoveTowards(slider.value, health.value, Time.deltaTime * updateSpeed);
            yield return null;
        }//end while

        Debug.Log("it worked");
    }//end UpdateSlider
}//end class