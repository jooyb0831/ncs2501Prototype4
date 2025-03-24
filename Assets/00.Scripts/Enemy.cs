using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    [SerializeField] bool isTrapped;
    [SerializeField] GameObject fierIndicator;

    private void OnEnable()
    {
        Mine.OnMineReady += FireOn;
    }

    private void OnDisable()
    {
        Mine.OnMineReady -= FireOn;
    }

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        if(!isTrapped)
        {
            enemyRb.AddForce(lookDirection * speed);
        }

    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MINE"))
        {
            enemyRb.Sleep();
            isTrapped = true;
            other.GetComponent<Mine>().AddCatch();
            //StartCoroutine(nameof(StopMove));
        }
    }

    IEnumerator StopMove()
    {
        float tempSpeed = speed;
        isTrapped = true;
        speed = 0f;
        enemyRb.Sleep();
        yield return new WaitForSeconds(3f);
        isTrapped = false;
    }

    private void FireOn()
    {
        fierIndicator.SetActive(true);
        StartCoroutine("FireOff");
    }

    IEnumerator FireOff()
    {
        yield return new WaitForSeconds(1.5f);
        fierIndicator.SetActive(false);
    }
}
