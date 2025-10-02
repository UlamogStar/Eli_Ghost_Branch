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
        if (other.CompareTag("Object"))
        {
            Debug.Log(other.name);
            ObjectBehaviour objectBehaviour = other.GetComponent<ObjectBehaviour>();
            if (objectBehaviour == null)
            {
                return;
            }

            if (isBody == false && objectBehaviour.returnable == true)
            {
                OnReturn.Invoke();
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