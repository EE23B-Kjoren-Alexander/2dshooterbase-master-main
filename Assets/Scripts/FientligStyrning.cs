using UnityEngine;
public class FientligStyrning : MonoBehaviour
{
    [SerializeField]
    float maxEnemyHP = 2;
    float enemyHP = 1;
    
    [SerializeField]
    float hastighet = 2;

    [SerializeField]
    GameObject fSkott;
    
    //timer OBS, kan vilja bytas till random framöver
    [SerializeField]
    float nyRikt = 3f;

    float timerRikt = 1f;

    float rikt = 1;

    //Väljer håll gubbe ska gå åt
    int leftRight = 0;

    [SerializeField]
    float tidTillSkott = 1;
    float tidMellanSkott = 0;
    void Start()
    {
        enemyHP = maxEnemyHP;
    }

    void Update()
    {
        //Startar timer för att byta riktning
        timerRikt += Time.deltaTime;
        tidMellanSkott += Time.deltaTime;

        if (enemyHP == 0)
        {Destroy(this.gameObject);}

        //Skjut skript
        if (tidMellanSkott > tidTillSkott)
        {
            //GetComponent<AudioSource>() Hämtar Audiosourcen från unity
            AudioSource speaker = GetComponent<AudioSource>();
            //"Play()" spelar variabeln speaker som inehåller ett ljud.
            speaker.Play();
            //de 2 raderna ovan kan kombineras till GetComponent<AudioSource>().Play();

            Instantiate(fSkott, transform.position, Quaternion.identity);

            // Ändra skottscriptets direction till samma som det här objektets
            fSkott.GetComponent<fskottcontroller>().leftRight = leftRight;
            tidMellanSkott = 0;
        }
        



        if (nyRikt < timerRikt)
        {
            //Genererar nummer som bestämmer vilket håll gubben ska gå åt
            leftRight = Random.Range(0, 2);
            print(leftRight);
            if (leftRight == 0)
            {
                //print("1");
                rikt = 1;
            }
            else if (leftRight == 1)
            {
                //print("-1");
                rikt = -1;
            }
            timerRikt = 0;
        }


        Vector2 rorelse = Vector2.right * hastighet * rikt;
        transform.Translate(rorelse * Time.deltaTime);

    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rikt")
        {
            print("Rikt");
            if (rikt == 1)
            {
                rikt = -1;
            }
            else if (rikt == -1)
            {
                rikt = 1;
            }
        }
        //Felsökningsverktyg
        //Test om gubbe kolliderar med något den inte ska. Tagga objektet du vill testa om det kolliderar med "Collisiontag"
        if (collision.gameObject.tag == "Collisiontag")
        {
            print("tag");
            

        }
        if (collision.gameObject.tag == "Skott")
        {
            enemyHP = enemyHP - 1;
            print(enemyHP);
            print("Nice shot!");
        }
        
        //scannar efter gameobjektet flicka16
        //GameObject player = GameObject.Find("flicka16");
        //Kallar metoden Skada från gameobjectet ship
        //metoden hurt tar bort 1 hp från spelaren
        //player.GetComponent<JumperController>().Skada();
        //Destroy(this.gameObject);
    }
    
    
}
