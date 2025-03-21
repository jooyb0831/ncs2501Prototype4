using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    public GameObject powerupIndicator;

    public GameObject minePrefab;


    public float speed = 5.0f;

    public bool hasPowerup;

    private float powerupStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(minePrefab, transform);
            obj.transform.SetParent(null);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("POWERUP"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(nameof(PowerupCountdownRoutine));
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ENEMY") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log($"Collided with {collision.gameObject.name} with powerup set to {hasPowerup}");

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7f);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

}
