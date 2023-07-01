using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_blocks : MonoBehaviour
{
    public GameObject[] Block;
    private int number_block;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivateRandomObject();
        }
    }

    private void ActivateRandomObject()
    {

        if (Block.Length > 0)
        {
            Block[number_block].SetActive(false);
            number_block = Random.Range(0, Block.Length);

            GameObject randomObject = Block[number_block];
            randomObject.SetActive(true);
        }
    }
}
