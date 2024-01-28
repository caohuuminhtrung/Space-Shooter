using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = -3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, speed, 0);
    }
}
