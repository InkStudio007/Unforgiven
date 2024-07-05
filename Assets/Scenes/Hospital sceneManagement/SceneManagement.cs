using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instanse;
    public GameObject Player;
    private void Awake()
    {
        if(instanse == null)
        {
            instanse = this;
            DontDestroyOnLoad(Player);
        }
        else
        {
            Destroy(Player);
        }
    }
    public void NextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LastScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
