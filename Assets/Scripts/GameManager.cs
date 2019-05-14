using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private int score = 0;
    private int hs = 0;

    public Text Score;
    public Text HighScore;

    public GameObject PlayerPrefab;
    public GameObject GhostPrefab;

    public bool isPowerPaletActive { get; private set; }

    private int lives = 3;

    private List<Ghost> Ghosts;

    private int killedGhost = 0;

    private Vector3 playerStartPos;
    public Vector3 GhostStartPos;

    public List<Image> Lives;

    private void Start()
    {
        playerStartPos = PlayerController3D.Instance.gameObject.transform.position;

        Score.text = score.ToString();

        if (PlayerPrefs.HasKey("High Score"))
        {
            hs = PlayerPrefs.GetInt("High Score");
        }

        HighScore.text = hs.ToString();

        InstantiateGhost();
    }

    private void InstantiateGhost()
    {
        if (Ghosts != null && Ghosts.Count >= 3)
            return;

        GameObject g = Instantiate(GhostPrefab, GhostStartPos, Quaternion.identity);

        if (Ghosts == null)
            Ghosts = new List<Ghost>();

        Ghosts.Add(g.GetComponent<Ghost>());

        StartCoroutine(RespawnAnotherGhost());
    }

    private IEnumerator RespawnAnotherGhost()
    {
        yield return new WaitForSeconds(5);

        InstantiateGhost();
    }

    public void OnPlayerDead()
    {
        --lives;

        if (lives <= 0)
        {
            SceneManager.LoadScene(0);
            return;
        }

        for (int i = 0; i < lives; i++)
        {
            Lives[i].gameObject.SetActive(i <= lives - 1);
        }

        ContinueAfterDead();
    }

    private void ContinueAfterDead()
    {
        Instantiate(PlayerPrefab, playerStartPos, Quaternion.identity);
    }

    public void OnCollisionWithPalet(bool isPowerPalet)
    {
        int inc = isPowerPalet ? 50 : 1;

        IncrementScore(inc);

        if (isPowerPalet)
        {
            isPowerPaletActive = true;

            if (Ghosts != null)
                Ghosts.ForEach(x => x.ChangeSpriteToFear());

            StartCoroutine(DeactivatePowerPalet());
        }
    }

    private void IncrementScore(int inc)
    {
        score += inc;

        Score.text = score.ToString();

        if (score > hs)
        {
            hs = score;
            HighScore.text = hs.ToString();
        }
    }

    public void OnGhostDead(Ghost g)
    {
        ++killedGhost;

        IncrementScore(killedGhost * 200);

        Ghosts.Remove(g);

        Destroy(g.gameObject);

        StartCoroutine(RespawnAnotherGhost());
    }

    private IEnumerator DeactivatePowerPalet()
    {
        yield return new WaitForSeconds(7);

        isPowerPaletActive = false;

        killedGhost = 0;

        if (Ghosts != null)
            Ghosts.ForEach(x => x.ChangeSpriteToNormal());
    }
}