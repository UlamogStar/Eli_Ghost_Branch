/*
Originial Coder: Natalie R.
Recent Coder: Natalie R.
Recent Changes: Initial Coding
Last date worked on: 12/10/2025
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine.UI;

public class UIBehaviorScript : MonoBehaviour
{
    [HideInInspector] public Transform follow;
    [SerializeField] private float zoffset = 0;
    public GameObject indicatorSmall, indicatorMedium, indicatorLarge;
    private MeshRenderer meshRendererSmall, meshRendererMedium, meshRendererLarge;
    private TMP_Text textSmall, textMedium, textLarge;
    [SerializeField] private bool isAttacking = false;
    [HideInInspector] public GameObject throwable;
    [SerializeField] private float blinkTime = 0.2f;



    void Start()
    {
        meshRendererSmall = indicatorSmall.GetComponent<MeshRenderer>();
        meshRendererMedium = indicatorMedium.GetComponent<MeshRenderer>();
        meshRendererLarge = indicatorLarge.GetComponent<MeshRenderer>();
        textSmall = indicatorSmall.GetComponent<TMP_Text>();
        textMedium = indicatorMedium.GetComponent<TMP_Text>();
        textLarge = indicatorLarge.GetComponent<TMP_Text>();
    }

    public void StartIndicator()
    {
        print("hehe");
        isAttacking = true;
        StartCoroutine(AttackIndicator());
    }

    private IEnumerator AttackIndicator()
    {
        var throwObject = throwable.GetComponent<ObjectBehaviour>();
        var size = throwObject.size;
        
            if (size == ObjectSize.Small)
            {
                print("is small");
                indicatorSmall.transform.position = follow.transform.position + (follow.transform.forward * zoffset);
                while (isAttacking == true)
                {
                    print("Attack");
                    meshRendererSmall.enabled = true;
                    textSmall.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererSmall.enabled = false;
                    textSmall.enabled = false;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererSmall.enabled = true;
                    textSmall.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererSmall.enabled = false;
                    textSmall.enabled = false;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererSmall.enabled = true;
                    textSmall.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererSmall.enabled = false;
                    textSmall.enabled = false;
                    isAttacking = false;
                }
            }

            else if (size == ObjectSize.Medium)
            {
                print("is Medium");
                indicatorMedium.transform.position = follow.transform.position + (follow.transform.forward * zoffset);
                while (isAttacking == true)
                {
                    print("Attack");
                    meshRendererMedium.enabled = true;
                    textMedium.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererMedium.enabled = false;
                    textMedium.enabled = false;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererMedium.enabled = true;
                    textMedium.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererMedium.enabled = false;
                    textMedium.enabled = false;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererMedium.enabled = true;
                    textMedium.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererMedium.enabled = false;
                    textMedium.enabled = false;
                    isAttacking = false;
                }
            }
            
            else if (size == ObjectSize.Big) 
            {
                print("is Large");
                indicatorLarge.transform.position = follow.transform.position + (follow.transform.forward * zoffset);
                while (isAttacking == true)
                {
                    print("Attack");
                    meshRendererLarge.enabled = true;
                    textLarge.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererLarge.enabled = false;
                    textLarge.enabled = false;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererLarge.enabled = true;
                    textLarge.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererLarge.enabled = false;
                    textLarge.enabled = false;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererLarge.enabled = true;
                    textLarge.enabled = true;
                    yield return new WaitForSeconds(blinkTime);
                    meshRendererLarge.enabled = false;
                    textLarge.enabled = false;
                    isAttacking = false;
                }
            }
    }
}
