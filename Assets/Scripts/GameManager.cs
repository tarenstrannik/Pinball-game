using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ballsText;
    public int ballsNumber=3;
    private int score = 0;

    [SerializeField] GameObject spawnPosition;
    [SerializeField] private float minForce = 8f;
    [SerializeField] private float maxForce = 10f;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] float repeatDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
        ballsText.text = "Balls: " + ballsNumber;

        Time.timeScale = 1f;
        StartCoroutine(WaitForPooling());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitForPooling()
    {
        while(true)
        {
            if (ObjectPooler.SharedInstance.isPoolingFinished)
            {
                StartRound();
                yield break;
            }
            yield return null;
        }
    }
    public void StartRound()
    {
        if(!isGameOver&& ballsNumber>0)
        {

            Invoke("SpawnBall", repeatDelay);
        }
        else
        {
            isGameOver = true;
            GameOver();
        }
    }
    public void AddToScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
    }
    private void SpawnBall()
    {
        ballsNumber--;
        ballsText.text = "Balls: " + ballsNumber;
        GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
           
            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.position = spawnPosition.transform.position; // position it at player
            float randomForce = Random.Range(minForce, maxForce);
            Rigidbody objectRb = pooledProjectile.GetComponent<Rigidbody>();
            objectRb.velocity = Vector3.zero;
            objectRb.angularVelocity = Vector3.zero;
            objectRb.AddForce(Vector3.up * randomForce, ForceMode.Impulse);
        }

    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

}
