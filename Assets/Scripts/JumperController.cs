using UnityEngine;
//Viktigt om man ska använda unitys Scen system. I detta fall för att transporteras till RealPlatformer
using UnityEngine.SceneManagement;
//Viktigt att skriva om man använder UI moduler i just denna skript.
using UnityEngine.UI;
using TMPro;
public class JumperController : MonoBehaviour
{
    //HP
    [SerializeField]
    TMP_Text HPcounter;
    //HP
    float nuvarandeHP = 3;
    //HP
    [SerializeField]
    float maxHP = 3;

    [SerializeField]
    float jumpForce = 2;

    [SerializeField]
    float speed = 3;

    [SerializeField]
    GameObject skottPrefab;

    //Kollar efter mark under gubber. Används när man ska hoppa
    [SerializeField]
    GameObject groundChecker;

    //Länkar mark Layer med script.
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    float jumpDelay = 1f;

    float timeSinceLastJump = 0;

    //cooldown för att skjuta
    float tidSedanSenasteSkott = 0;

    [SerializeField]
    float tidMellanSkott = 0.5f;

    //Direction = vilket håll gubben kollar. Jag har satt värder 1 som Vänster och 0 till höger
    int direction = 1;

    //Hoppsystem nedan
    //Fixed update kör 30 gånger per sekund. Samma som fysiksystemet. Vanlig update updaterar för ofta och då blir fysiksystemet konstigt.

    void Start()
    {
        nuvarandeHP = maxHP;
    }
    void FixedUpdate()
    {
        HPcounter.text = "HP: " + nuvarandeHP;

        float inputX = Input.GetAxisRaw("Horizontal");
        Vector2 movement = Vector2.right * inputX;
        transform.Translate(movement * speed * Time.deltaTime);

        if (inputX > 0)
        {
            direction = 1;
            //Hämtar sprite render komponenter och flippar den.
            GetComponent<SpriteRenderer>().flipX = false;


        }
        else if (inputX < 0)
        {
            direction = 0;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        //Cooldown för skott
        tidSedanSenasteSkott += Time.deltaTime;

        //Spawnar skott
        if (Input.GetAxisRaw("Fire1") > 0 && tidSedanSenasteSkott > tidMellanSkott)
        {
            // Lagra referens till skottet som skapas i variabeln "skottet"
            GameObject skottet = Instantiate(skottPrefab, transform.position, Quaternion.identity);

            // Ändra skottscriptets direction till samma som det här objektets
            skottet.GetComponent<skottcontroller>().direction = direction;
            tidSedanSenasteSkott = 0;
        }
        //HP, game over
        if (nuvarandeHP <= 0)
        {
            print("GAME OVER");
            SceneManager.LoadScene("GameOver");
        }

        //boolen kollar om närliggande objekt har lagermasken ground.
        //".1f bestämmer hur nära marken du måste vara för att hoppa.
        bool jordad = Physics2D.OverlapCircle(
            groundChecker.transform.position,
            .1f,
            groundLayer
        );

        timeSinceLastJump += Time.deltaTime;
        if (jordad == false)
        {
            timeSinceLastJump = 0;
        }

        if (Input.GetAxisRaw("Jump") > 0 && jordad == true && timeSinceLastJump > jumpDelay)
        {
            Rigidbody2D fysik = GetComponent<Rigidbody2D>();

            fysik.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            GetComponent<AudioSource>().Play();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "triggerbox")
        {
            print("Flyttar till Nästa bana");
            SceneManager.LoadScene("GameWon");
        }
        //Felsökningsverktyg
        //Test om gubbe kolliderar med något den inte ska. Tagga objektet du vill testa om det kolliderar med "Collisiontag"
        if (collision.gameObject.tag == "Collisiontag")
        {
            print("tag");
        }
        if (collision.gameObject.tag == "enemy")
        {
            nuvarandeHP -= 1; 
        }
        ;
    }
    //Gör så att andra objekt kan ändra jumper controllers hp variabel
    public void Skada()
    {
        nuvarandeHP -= 1;
        //hpSlider.value = nuvarandeHP; -- byt hpslider mot något annat

        HPcounter.text = "HP: " + nuvarandeHP;
        if (nuvarandeHP <= 0)
        {
            print("GAME OVER");
            SceneManager.LoadScene("GameOver");
        }
    }
}
