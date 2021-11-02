using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

public class CameraButtonControllers : MonoBehaviour
{
    public void ToMainMenu()
    {
      SceneManager.LoadScene(Configuration.Scenes.MainMenu);
    }
}
