using UnityEngine;

public class FollowController : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    float kameraY = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        pos.x = target.transform.position.x + kameraY;
        //transform.position är objektets nuvarande position
        transform.position = pos;

    }
}
