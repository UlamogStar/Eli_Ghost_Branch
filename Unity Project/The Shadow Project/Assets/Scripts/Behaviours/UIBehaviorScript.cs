using UnityEngine;

public class UIBehaviorScript : MonoBehaviour
{
    public Transform follow;
    public float zoffset = 0;
    private GameObject indicator;

 public void Start()
 {
     indicator = gameObject;
 }
 
 public void Update()
 {
    indicator.transform.position= follow.transform.position + (follow.transform.forward * zoffset);
 }
}
