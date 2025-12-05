using UnityEngine;
//UnityEngine.SceneManagement måste användas när man hanterar scener.
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Main");
    }
}