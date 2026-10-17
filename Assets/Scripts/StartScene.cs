
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;


public class StartScene : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject PauseIcon;
    public static StartScene Instance;

   



    private void Start()
    {

        Time.timeScale = 1f;
        YG2.GameplayStop();
    }

    public void SceneStarter()
    {
        SceneManager.LoadScene("Game");
    }

    public void ScenePause()
    {
        UIScreen.SetActive(true);
        Time.timeScale = 0f;
        PauseIcon.SetActive(false);
        YG2.GameplayStop();
    }

    public void SceneResume()
    {
        UIScreen.SetActive(false);
        Time.timeScale = 1f;
        PauseIcon.SetActive(true);
        YG2.GameplayStart();
    }
}
