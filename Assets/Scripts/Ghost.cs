using UnityEngine;
using UnityEngine.AI;

//[ExecuteInEditMode]
public class Ghost : MonoBehaviour
{
    private NavMeshAgent navAgent;

    public SpriteRenderer Sprite;

    public Sprite Normal;
    public Sprite Fear;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerController3D.Instance.Moving)
        {
            navAgent.SetDestination(PlayerController3D.Instance.gameObject.transform.position);
            navAgent.speed = 1.5f;
        }
        else
        {
            navAgent.SetDestination(gameObject.transform.position);
            navAgent.speed = 0;
        }

        Sprite.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x + 90,
            transform.rotation.y,
            transform.rotation.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.Instance.isPowerPaletActive)
            {

                GameManager.Instance.OnGhostDead(this);
            }
            else
            {
                Destroy(PlayerController3D.Instance.gameObject);
            }
        }
    }

    public void ChangeSpriteToFear()
    {
        Sprite.sprite = Fear;
    }
    public void ChangeSpriteToNormal()
    {
        Sprite.sprite = Normal;
    }

    private void OnDestroy()
    {
    }
}