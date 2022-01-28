using UnityEngine.SceneManagement;

public class SceneService : Singleton<SceneService>
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}