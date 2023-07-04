using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_blocks : MonoBehaviour
{
    [SerializeField] private Transform[] childTransforms;
    [SerializeField] private GameObject[] Block;
    public int number_block;

    void Start()
    {
        childTransforms = GetComponentsInChildren<Transform>();
        Block = new GameObject[childTransforms.Length - 1];
        for (int i = 1; i < childTransforms.Length; i++)
        {
            Block[i - 1] = childTransforms[i].gameObject;
            childTransforms[i].gameObject.SetActive(false);
        }
    }

    public void ActivateRandomObject()
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
