using UnityEngine;

public class Enemy_Controller_Script : MonoBehaviour
{
    [SerializeField]
    float enemyspeed = 2f;

    [SerializeField]
    GameObject boomPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 newpos = new();
        newpos.x = Random.Range(-7, 7);
        newpos.y = Camera.main.orthographicSize + 1;

        transform.position = newpos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rorelse = Vector2.down * enemyspeed;
        transform.Translate(rorelse * Time.deltaTime);

        if (transform.position.y < -Camera.main.orthographicSize - 1)
        {
            //scannar efter gameobjektet ship
            GameObject player = GameObject.Find("Ship");
            //Kallar metoden Hurt från gameobjectet ship
            //metoden hurt tar bort 1 hp från spelaren
            player.GetComponent<player_controller>().Hurt();
            Destroy(this.gameObject);

        }
    }

    //Ingen aning om vad OnTriggerEnter2D, Collider2D och collision gör eller hur de fungerar
    //Körs när Fienden nuddas av ett objekt.
    //OnTriggerEnter2D anropas om någor med en trigger nuddar en rigged body.
    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
