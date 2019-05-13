using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControllor : Singleton<PlayerControllor>
{
    private List<SpriteRenderer> sprites;

    private Rigidbody2D pacBody;

    private float animTime = .5f;
    private int spriteCount;

    private float speed = 2f;

    // Use this for initialization
    private void Start()
    {
        pacBody = GetComponent<Rigidbody2D>();

        sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        spriteCount = sprites.Count;

        StartCoroutine(StartChewing());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 dir = GetDirection();
        pacBody.velocity = (speed) * dir;
        pacBody.angularVelocity = 0;
    }

    private Vector2 GetDirection()
    {
        switch (InputControllor.GetKeyDownCode())
        {
            case KeyCode.UpArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                FixXOnly();
                return new Vector2(0, 1);

            case KeyCode.DownArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                FixXOnly();
                return new Vector2(0, -1);

            case KeyCode.RightArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                FixYOnly();
                return new Vector2(1, 0);

            case KeyCode.LeftArrow:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                FixYOnly();
                return new Vector2(-1, 0);
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

            if (temp >= animTime / spriteCount)
            {
                if (count >= spriteCount - 1)
                {
                    iterator = -1;
                }
                else if (count <= 0)
                {
                    iterator = 1;
                }

                count += iterator;

                SpriteRenderer sp = sprites[count];
                sprites.ForEach(x => x.gameObject.SetActive(x == sp));

                temp = 0;
            }
        }
    }

    private void FixXOnly()
    {
        pacBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixYOnly()
    {
        pacBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
}