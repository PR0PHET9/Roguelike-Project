using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

  public GameObject pauseUI;

  public void OnGameResumePress(){
   pauseUI.SetActive(false);

  } 
   public void OnGameSettingsPress () {


   }
      public void OnGameExitPress () {
      Application.Quit();
    
   }
     public void OnEnterPausePress () {
      pauseUI.SetActive(true);
    
   }
   void Update () {
if (Input.GetKeyDown(KeyCode.Escape))
{
   pauseUI.gameObject.SetActive(!pauseUI.gameObject.activeSelf);
}

   }
}
