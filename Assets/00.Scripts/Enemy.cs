using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    [SerializeField] bool isTrapped;

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
            StartCoroutine(nameof(StopMove));
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
}
