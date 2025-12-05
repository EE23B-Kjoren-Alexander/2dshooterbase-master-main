using UnityEngine;
//Viktigt att skriva om du använder UI moduler i just denna skript.
using UnityEngine.UI;
//Viktigt om du ska använda unitys Scen system. I detta fall för game over scenen
using UnityEngine.SceneManagement;
public class player_controller : MonoBehaviour
{
    //SerializeFIeld gör så att värdet under kan ändras i unity editorn
    //speed anger antal unityenheter som skeppet ska röra sig per sekund (0.02)
    [SerializeField]
    float speed = 0.02f;

    //Länkar mark Layer med script.
    [SerializeField]
    LayerMask wallLayer;

    //Gör så att du can länka Prefaben till Gameobjectet boltprefab i unity editor.
    [SerializeField]
    GameObject boltPrefab;

    float timeSinceLastShot = 0;

    [SerializeField]
    float timeBetweenShots = 0.5f;

    float currentHP = 0;
    [SerializeField]
    float maxHP = 3;

    [SerializeField]
    Slider hpSlider;

    void Start()
    {
        currentHP = maxHP;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
    }

    // FixedUpdate gör så att spel updateras i en stadig hastighet.
    void Update()
    {
        //Känner av knaptryck för att röra sig i X och Y led (WASD + Piltangenter etc)
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        //Använder de tidigar avläsna knaptrycken för att flytta saker (skeppet)
        Vector2 movement = Vector2.right * inputX
                         + Vector2.up * inputY;

        //Anväder movement variablen för att faktiskt röra skeppet
        //Deltatime är tiden i sekunder som förflutit sedan senaste frame.
        transform.Translate(movement * speed * Time.deltaTime);

        //====================================================================================
        //script för att skjuta nedan
        //------------------------------------------------------------------------------------


        //Linjen nedan betyder: timeSinceLastShot = timeSinceLastShot + Time.deltaTime
        timeSinceLastShot += Time.deltaTime;


        if (Input.GetAxisRaw("Fire1") > 0 && timeSinceLastShot > timeBetweenShots)
        //instatiate skapar en kopia av (i detta fall) boltprefab (skott)
        //transfor.position är vart skeppet beffiner sig. Detta gör att kopian av boltfifab spawnas där-
        //-skeppet är
        //Quaternion anger rotation vet ej hur den funkar. Men quaternion.identiy betyder ingen rotation.
        //Om man anger position är rotation ett krav.
        {
            //GetComponent<AudioSource>() Hämtar Audiosourcen från unity
            AudioSource speaker = GetComponent<AudioSource>();
            //"Play()" spelar variabeln speaker som inehåller ett ljud.
            speaker.Play();
            //de 2 raderna ovan kan kombineras till GetComponent<AudioSource>().Play();

            Instantiate(boltPrefab, transform.position, Quaternion.identity);
            timeSinceLastShot = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Hurt();

            //Kod för kollision vid kanten av kameran !!!EJ FÄRDIG!!!
            if (collision.gameObject.tag == "triggerbox")
            {
                print("Vägg");
            }
        }
    }

    //definerar metoden Hurt som används ovan.
    public void Hurt()
    {
        currentHP -= 1;
        hpSlider.value = currentHP;

        if (currentHP <= 0)
        {
            print("GAME OVER");
            SceneManager.LoadScene("GameOver");
        }
    }
}
