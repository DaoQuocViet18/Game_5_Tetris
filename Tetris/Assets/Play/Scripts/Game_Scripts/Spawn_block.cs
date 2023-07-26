using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn_block : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject[] objectPrefab; // Prefab của đối tượng cần sinh ra
    [SerializeField] private Transform spawnPoint; // Vị trí spawn
    public int limit_Quantity_Spawn = 20;
    private int quantity_Spawn = 0;
    public float time_to_Stop_Present = 0.5f;

    [Header("Signal")]
    public Display_blocks display_Blocks; 

    void Start()
    {
        spawnPoint = GetComponent<Transform>();
        display_Blocks.ActivateRandomObject();
        Invoke("SpawnObject", 2f);
    }

    public  void SpawnObject()
    {
        // Sinh ra đối tượng tại vị trí spawnPoint
        Instantiate(objectPrefab[display_Blocks.number_block], spawnPoint.position, spawnPoint.rotation);
        if (quantity_Spawn == limit_Quantity_Spawn && time_to_Stop_Present > 0.5f/4)
        {
            quantity_Spawn = 0;
            time_to_Stop_Present /= 1.25f;
        }    
        quantity_Spawn++;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("StartScene");
    }
}

