using UnityEngine;

public class boltcontroller : MonoBehaviour
{
    [SerializeField]
    float boltspeed = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Förstör skott efter 3sec
        //Destroy(this.gameObject, 3);


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.up * boltspeed;
        //Deltatime är tiden i sekunder som förflutit sedan senaste frame.
        transform.Translate(movement * Time.deltaTime);
        //Note to self. Fråga om ekvationen för tid.

        //Lösning för att ta bort skott när åker ut ur skärmen. Kamrans position anges i unityenheter.
        if (transform.position.y > 6)
        {
            Destroy(this.gameObject);
        }
        //alternativ lösning nedan
        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(this.gameObject);
        }
    } 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
            {
            Destroy(this.gameObject);
            }
    }
}
