using UnityEngine;

public class Tunnel : MonoBehaviour
{
    //5.18, 0.36
    // Use this for initialization

    public Transform SpawnPosition;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.position = SpawnPosition.position;
    }
}