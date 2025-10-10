/*
Originial Coder: Owynn A.
Recent Coder: Owynn A.
Recent Changes: Initial Coding
Last date worked on: 9/23/2025
*/

using UnityEngine;
using UnityEngine.Events;
public class DamageBehaviour : MonoBehaviour
{ 
    public UnityEvent OnDamage, OnReturn;
    public bool isBody = true;
    public int damage;
    public int multiplier = 1;
    public IntData health;
    public void OnTriggerEnter(Collider other)
    {
		Debug.Log(other.name);
        ObjectBehaviour objectBehaviour = other.GetComponent<ObjectBehaviour>();
		if (this.gameObject.CompareTag("Ghost"))
		{
			if(objectBehaviour.thrown)
			{
				damage = objectBehaviour.damage.value;
                damage *= multiplier;
                health.value -= damage;
                OnDamage.Invoke();
			}
			else
			{
				objectBehaviour.thrown = true;	
			}
		}
        else if (other.CompareTag("Object"))
        {
            
            if (objectBehaviour == null)
            {
				Debug.Log("object not found");
                return;
            }

            if (isBody == false && objectBehaviour.returnable == true)
            {
				Debug.Log("Return");
                OnReturn.Invoke();
                ObjectBehaviour block = other.GetComponent<ObjectBehaviour>();
                block.OnReturn();
            } // end of if}
            else
            {
                damage = objectBehaviour.damage.value;
                damage *= multiplier;
                health.value -= damage;
                OnDamage.Invoke();
            } // end of else
        } // end of CompareTag
    } // end of OnTriggerEnter
} //end of