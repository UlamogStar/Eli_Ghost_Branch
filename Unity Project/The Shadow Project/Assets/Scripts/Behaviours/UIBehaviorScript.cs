/*
Originial Coder: Natalie R.
Recent Coder: Natalie R.
Recent Changes: Initial Coding
Last date worked on: 11/6/2025
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBehaviorScript : MonoBehaviour
{
    public Transform follow;
    [SerializeField] private float zoffset = 0;
    private GameObject indicator;
    private MeshRenderer meshRenderer;
    [SerializeField] private bool isAttacking = false;

 void Start()
 {
     indicator = gameObject;
     meshRenderer = indicator.GetComponent<MeshRenderer>();
 }
 public void StartIndicator()
 {
     print("hehe");
     isAttacking = true;
     StartCoroutine(AttackIndicator());
 }
 
 private IEnumerator AttackIndicator()
 {
     print("is running");
     indicator.transform.position= follow.transform.position + (follow.transform.forward * zoffset);

    
     while (isAttacking == true)
     {
         print("Attack");
         meshRenderer.enabled = true;
         yield return new WaitForSeconds(0.3f);
         meshRenderer.enabled = false;
         yield return new WaitForSeconds(0.3f);
         meshRenderer.enabled = true;
         yield return new WaitForSeconds(0.3f);
         meshRenderer.enabled = false;
         yield return new WaitForSeconds(0.3f);
         meshRenderer.enabled = true;
         yield return new WaitForSeconds(0.3f);
         meshRenderer.enabled = false;
         isAttacking = false;
     }
 }
}
