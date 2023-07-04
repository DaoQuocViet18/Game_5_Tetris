using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_block : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject[] objectPrefab; // Prefab của đối tượng cần sinh ra
    [SerializeField] private Transform spawnPoint; // Vị trí spawn

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
    }
}

