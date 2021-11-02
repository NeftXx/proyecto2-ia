using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Controller
{
  public class MainMenu : MonoBehaviour
  {
    public void ToCrudSpacesScene()
    {
      SceneManager.LoadScene(Configuration.Scenes.SpacesMenu);
    }

    public void ToCameraScene()
    {
      SceneManager.LoadScene(Configuration.Scenes.Camera);
    }

    public void Exit()
    {
      Application.Quit();
    }
  }
}
