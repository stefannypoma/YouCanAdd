using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public void OpenAprender()
    {
        SceneManager.LoadScene("Aprender");
    }

    public void OpenActivades1al10()
    {
        SceneManager.LoadScene("ActividadesPrincipal");
    }

    public void OpenActivades11al20()
    {
        SceneManager.LoadScene("ActividadesPrincipalOnce");
    }

    public void OpenPracticar()
    {
        SceneManager.LoadScene("PracticarNumeros1");
    }
}
