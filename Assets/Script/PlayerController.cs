using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject bullet;
    public Transform bulletPos;

    [SerializeField] private float xMin = -5.5f, xMax = 5.5f, yMin = -1, yMax = 13;
    public float maxTilt = 20, tiltFactor = 2;
    public float speed = 20, smoothTime = 0.1f;
    public float fireRate = 0.2f;

    private AudioSource audioSource;

    public Vector3 cursorPos;
    public Vector3 cursorToWorldPos;
    public Vector3 velocity = Vector3.zero;

    private bool allowFire = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && allowFire)
        {
            StartCoroutine(Fire());
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(startGame(rb));
    }

    public void MovingBehaviour(Rigidbody player)
    {
        cursorPos = Input.mousePosition;
        cursorPos.z = 15;

        cursorToWorldPos = Camera.main.ScreenToWorldPoint(cursorPos);
        cursorToWorldPos.x = Mathf.Clamp(cursorToWorldPos.x, xMin, xMax);
        cursorToWorldPos.y = Mathf.Clamp(cursorToWorldPos.y, yMin, yMax);

        player.position = Vector3.SmoothDamp(rb.position, cursorToWorldPos, ref velocity, smoothTime, speed);
        // player.position = Vector3.Lerp(rb.position, worldPos, speed * Time.deltaTime);
        TiltingBehaviour(player);
    }

    public void TiltingBehaviour(Rigidbody player)
    {
        var tilt = Mathf.Clamp(velocity.x * -tiltFactor, -maxTilt, maxTilt);
        player.transform.rotation = Quaternion.Euler(-90, tilt, 0);
    }

    IEnumerator Fire()
    {
        allowFire = false;
        Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        yield return new WaitForSeconds(fireRate);
        audioSource.Play();
        allowFire = true;
    }

    IEnumerator startGame(Rigidbody rb)
    {
        yield return new WaitForSeconds(2);
        MovingBehaviour(rb);
    }
}
