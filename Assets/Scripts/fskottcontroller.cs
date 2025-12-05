using UnityEngine;
public class fskottcontroller : MonoBehaviour
{
    [SerializeField]
    float skottHastighet = 1.5f;

    [SerializeField]
    float livstid = 5.5f;

    public int leftRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, livstid);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (leftRight == 1)
        {
            Vector2 movement = Vector2.left * skottHastighet;
            transform.Translate(movement * Time.deltaTime);
            //Riktar skotten
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (leftRight == 0)
        {
            Vector2 movement = Vector2.right * skottHastighet;
            transform.Translate(movement * Time.deltaTime);
            //Riktar skotten
            GetComponent<SpriteRenderer>().flipX = true;
        }  

    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == "Mark")
    //        {
    //        Destroy(this.gameObject);
    //        }
    //}
}
