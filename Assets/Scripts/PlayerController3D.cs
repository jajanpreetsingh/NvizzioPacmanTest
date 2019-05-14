using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : Singleton<PlayerController3D>
{
    public List<Sprite> Sprites;

    private Rigidbody pacBody;

    private float animTime = 1.5f;

    private float speed = 2f;

    public SpriteRenderer Sprite;

    public bool Moving { get; private set; }

    // Use this for initialization
    private void Start()
    {
        pacBody = GetComponent<Rigidbody>();

        Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 dir = GetDirection();
        pacBody.velocity = (speed) * transform.right;
        pacBody.angularVelocity = Vector3.zero;
    }

    private Vector3 GetDirection()
    {
        switch (InputControllor.GetKeyDownCode())
        {
            case KeyCode.UpArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                FixXOnly();
                Moving = true;
                return new Vector3(0, 0, 1);

            case KeyCode.DownArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                FixXOnly();
                Moving = true;
                return new Vector3(0, 0, -1);

            case KeyCode.RightArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                FixZOnly();
                Moving = true;
                return new Vector3(-1, 0, 0);

            case KeyCode.LeftArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                FixZOnly();
                Moving = true;
                return new Vector3(-1, 0, 0);
        }

        if (Moving)
        {
            StartCoroutine(StartChewing());
        }

        return pacBody.velocity.normalized;
    }

    private IEnumerator StartChewing()
    {
        float temp = 0;
        int iterator = 1;

        int count = 0;

        while (true)
        {
            yield return new WaitForEndOfFrame();

            temp += Time.deltaTime;

            if (temp >= animTime / Sprites.Count)
            {
                if (count >= Sprites.Count - 1)
                {
                    iterator = -1;
                    count = Sprites.Count - 1;
                }
                else if (count <= 0)
                {
                    iterator = 1;
                    count = 0;
                }

                count += iterator;

                if (count >= 0 && count <= Sprites.Count - 1)
                    Sprite.sprite = Sprites[count];

                temp = 0;
            }
        }
    }

    private void FixXOnly()
    {
        pacBody.constraints = RigidbodyConstraints.FreezePositionX
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixZOnly()
    {
        pacBody.constraints = RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();

        GameManager.Instance.OnPlayerDead();
    }
}