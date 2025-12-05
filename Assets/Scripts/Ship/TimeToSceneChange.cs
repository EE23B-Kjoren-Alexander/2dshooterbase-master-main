using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeToSceneChange : MonoBehaviour
{
    [SerializeField]
    float timeToSurvive = 5;

    float timePassed = 0;

    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed > timeToSurvive)
        {
            print("Startar platform sektion");
            SceneManager.LoadScene("Platform");
        }
    
    }

}
