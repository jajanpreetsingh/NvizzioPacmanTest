using UnityEngine;

public class Pacdot3D : MonoBehaviour
{
    public bool isPowerPalet = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.OnCollisionWithPalet(isPowerPalet);

            Destroy(gameObject);
        }
    }
}