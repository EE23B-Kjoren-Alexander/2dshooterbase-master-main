using UnityEngine;
public class skottcontroller : MonoBehaviour
{
    [SerializeField]
    float skottHastighet = 0.5f;

    public int direction;

    //OBS: Directionscripter är hämtad från JumperController
    //Måste implimenteraa möjlighet att skjuta skott åt rätt håll när de spanwnas

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //----------------LJUD----------------
        AudioSource pang = GetComponent<AudioSource>();
        pang.Play();
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (direction == 1)
        {
            Vector2 movement = Vector2.right * skottHastighet;
            transform.Translate(movement * Time.deltaTime);
            //Riktar skotten
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction == 0)
        {
            Vector2 movement = Vector2.left * skottHastighet;
            transform.Translate(movement * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //Lösning för att ta bort skott när åker ut ur skärmen. Kamrans position anges i unityenheter.
        //if (transform.position.y > 6)
        //{
        //    Destroy(this.gameObject);
        //}
        //alternativ lösning nedan
        //if (transform.position.y > Camera.main.orthographicSize + 1)
        //{
        //    Destroy(this.gameObject);
        // }

    } 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
            {
            Destroy(this.gameObject);
            }
    }
}
