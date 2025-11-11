/*
Originial Coder: Owynn A.
Recent Coder: Zackery E.
Recent Changes: Replaced this with a health value behavior, and to object
behavior to detect it and damage it. This script is is kept in case you want
to keep it.
Last date worked on: 11/3/2025
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
		ResetObjectBehaviour resetBehaviour = other.GetComponent<ResetObjectBehaviour>();
	if(resetBehaviour == null)
	{
		Debug.Log("no behaviour found");
	}
	else
	{
		Debug.Log("we cool");
	}
		if (this.gameObject.CompareTag("Ghost"))
		{
			if(objectBehaviour.thrown)
			{
				damage = objectBehaviour.damage;
                damage *= multiplier;
                health.value -= damage;
                OnDamage.Invoke();
				resetBehaviour.Reset();
			}
			else
            {
                
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
                //block.OnReturn();
            } // end of if}
            else
            {
                damage = objectBehaviour.damage;
                damage *= multiplier;
                health.value -= damage;
                OnDamage.Invoke();
				resetBehaviour.Reset();
            } // end of else
        } // end of CompareTag
    } // end of OnTriggerEnter
} //end of