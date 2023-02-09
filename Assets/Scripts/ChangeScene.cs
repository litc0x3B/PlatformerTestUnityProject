using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(_sceneName); 
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }

}
