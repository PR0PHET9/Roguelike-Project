using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 public void PlayGame()
 {
    SceneManager.LoadScene("Test Level 1"); // Loading Test Level 1 atm
 }

public void QuitGame()
{
   Application.Quit();
}
}
