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
            ReloadScene();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ResetPlayer1();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ResetPlayer2();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ResetPlayer3();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            ResetPlayer4();

        if (Input.GetKeyDown(KeyCode.Alpha0))
            ResetGlobalHealth();

        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    // Public methods for UI Buttons

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPlayer1()
    {
        pHealth1.value = 100;
        updateHealth.Invoke();
    }

    public void ResetPlayer2()
    {
        pHealth2.value = 100;
        updateHealth.Invoke();
    }

    public void ResetPlayer3()
    {
        pHealth3.value = 100;
        updateHealth.Invoke();
    }

    public void ResetPlayer4()
    {
        pHealth4.value = 100;
        updateHealth.Invoke();
    }

    public void ResetGlobalHealth()
    {
        gHealth.value = 100;
        updateHealth.Invoke();
    }
    // Pre-defines whether to quit application or stop play mode in editor using
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
