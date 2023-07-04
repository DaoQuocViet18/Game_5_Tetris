using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[RequireComponent(typeof(Border))] 
public class Moving_block : MonoBehaviour
{

    [Header("Move down")]
    public float speed_move = 0.5f;
    public float speed_Down = 1;

    [Header("Stop")]
    public bool stop = false;
    public GameObject[] block_junior;
    private Border border;
    private Spawn_block spawn_Block;
    private Display_blocks display_Blocks;
    void Start()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>();
        block_junior = new GameObject[childTransforms.Length - 1];
        for (int i = 1; i < childTransforms.Length; i++)
        {
            block_junior[i-1] = childTransforms[i].gameObject;
        }
        border = GetComponent<Border>();
        spawn_Block = GameObject.Find("Spawn_block").GetComponent <Spawn_block>();
        display_Blocks = GameObject.Find("Display_board").GetComponent<Display_blocks>();
        InvokeRepeating("move_Down_Slow", 0.5f, 1);
    }

    void Update()
    {    
        if (!stop)
            move();
    }

    public  void spawn_modosl()
    {
        Invoke("spawn", 1);
        CancelInvoke("move_Down_Slow");
    }    

    private void spawn()
    {
            spawn_Block.SpawnObject();
            display_Blocks.ActivateRandomObject();    
    }    
    private void move_Down_Slow()
    {
        transform.Translate(Vector2.down * speed_Down, Space.World);
    }    

    private void move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, transform.position.z + 90f);
            border.check_block();
        }    

        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -3.5f)
            transform.Translate(Vector2.down * speed_Down, Space.World); 

        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -6)
        { 
            transform.Translate(Vector2.left * speed_move, Space.World);
            border.check_block();
        }    
        else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 2)
        {     
            transform.Translate(Vector2.right * speed_move, Space.World);
            border.check_block();
        }
    transform.position = new Vector2(Mathf.Floor(transform.position.x / 0.1f) * 0.1f, transform.position.y);
    }

}
