using UnityEngine.SceneManagement;

public enum SceneName
{
    Logo,
    Lobby,
    Education_Check,
    CardCollection,
    Store,
    Assemble_Robot,
    Satellite
}

public class SceneService : Singleton<SceneService>
{
    public void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    public void LoadScene(GameType gameType)
    {
        SceneManager.LoadScene(gameType.ToString());
    }

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}