using UnityEngine;

public class BoomController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Förstör spelobjekt efter 0.3 sec
        Destroy(this.gameObject, 0.3f);
    }

   
}
