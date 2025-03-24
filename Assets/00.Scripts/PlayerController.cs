using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    public GameObject powerupIndicator;
    public GameObject mineIndicator;

    public GameObject minePrefab;
    public bool isMineExist;


    public float speed = 5.0f;

    public bool hasPowerup;

    private float powerupStrength = 15.0f;

    private void OnEnable()
    {
        Mine.OnMineReady += MineIsReady;   
    }

    private void OnDisable()
    {
        Mine.OnMineReady -= MineIsReady;
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        MineIsReady();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if(Input.GetKeyDown(KeyCode.Space) && !isMineExist)
        {
            GameObject obj = Instantiate(minePrefab, transform.position, minePrefab.transform.rotation);
            isMineExist = true;
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

    public void MineIsReady()
    {
        isMineExist = false;
        mineIndicator.SetActive(true);
    }
}
