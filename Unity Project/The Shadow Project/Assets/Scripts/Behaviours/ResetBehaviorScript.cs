using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ResetBehaviorScript : MonoBehaviour
{
   
    public IntData pHealth1, pHealth2, pHealth3, pHealth4, gHealth;
    public UnityEvent updateHealth;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pHealth1.value = 100;
            updateHealth.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pHealth2.value = 100;
            updateHealth.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pHealth3.value = 100;
            updateHealth.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pHealth4.value = 100;
            updateHealth.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            gHealth.value = 100;
            updateHealth.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
