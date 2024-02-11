using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CoinPoolingManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public int poolSize = 20;
    public float spawnDistance = 10f;

    private List<GameObject> coinPool = new List<GameObject>();

    //public Camera playerCamera;
    public GameObject player;
    private Transform playerTransform;
    private ScoreManager scoreManager;

    //private int score = 0;
    int layerMask = 1 << 8;

    void Start()
    {
        playerTransform = player.transform; // Assuming the camera follows the player.

        //Debug.Log($"PlayerTransform is set to: {playerTransform.name}");
        // Initialize the object pool.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coinPool.Add(coin);
        }
    }

    void Update()
    {
        // Deactivate coins out of range, fix later maybe inefficient
        foreach (var coin in coinPool)
        {
            if (coin.activeSelf && Vector3.Distance(playerTransform.position, coin.transform.position) > spawnDistance)
            {
                coin.SetActive(false);
            }
        }

        //Debug.Log("SpawnCoin");
        SpawnCoin();
    }

    void SpawnCoin()
    {
        //Debug.Log("SpawnCoin called");
        // new coin, may also be inefficient
        foreach (var coin in coinPool)
        {
            if (!coin.activeSelf)
            {
                //Debug.Log("Spawning coin");
                coin.transform.position = GetRandomSpawnPosition();
                coin.SetActive(true);
                break;
            }
            else
            {
                //Debug.Log("Coin already");
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float minX = playerTransform.position.x - spawnDistance;
        float maxX = playerTransform.position.x + spawnDistance;
        float minZ = playerTransform.position.z - spawnDistance;
        float maxZ = playerTransform.position.z + spawnDistance;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        float spawnY = playerTransform.position.y;

        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        RaycastHit hit;
        if (Physics.Raycast(spawnPosition + Vector3.up * 10f, Vector3.down, out hit, 100f, layerMask))
        {
            spawnPosition.y = hit.point.y;
            //Debug.LogError("good");
        }
        else
        {
            //Debug.Log("Did not hit terrain!");
        }

        return spawnPosition;
    }

    [System.Obsolete]
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("touched");
        if (other.CompareTag("Player"))
        {
            // Deactivate the coin and increment the score.
            gameObject.SetActive(false);
            //score++;
            //scoreManager.IncreaseScore(1);
            scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.IncreaseScore(1);
                Debug.Log("Score: " + scoreManager.score);
            }
            else
            {
                //Debug.Log("ScoreManager is null");
            }
            //Debug.Log("Score: " + score);
        }
    }
}
