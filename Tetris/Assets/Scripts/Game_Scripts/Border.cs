using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Border : MonoBehaviour
{
    private Moving_block moving_Block;
    [Header("Down")]
    public float raycastDistance = 1f;
    public LayerMask layerMask_Stop;

    [Header("Left Right")]
    public float raycastDistance_left_right = 1f;

    void Start()
    {
        moving_Block = GetComponent<Moving_block>();
    }

    public void time_delay_to_stop()
    {
        if (!moving_Block.stop)
            foreach (GameObject block in moving_Block.block_junior)
                if (Physics2D.Raycast(block.transform.position, Vector2.down, raycastDistance, layerMask_Stop) && !moving_Block.stop)
                {
                    moving_Block.stop = true;
                    foreach (GameObject jun in moving_Block.block_junior)
                        jun.gameObject.layer = 3;
                    moving_Block.Invoke("spawn", 1);
                    break;
                }

        if (!moving_Block.stop)
            moving_Block.move_Down_Slow();   
    }    

    public void check_border_block ()
    {
        foreach (GameObject block in moving_Block.block_junior)
        {
            if (block.transform.position.x <= -6)
                transform.Translate(Vector2.right * moving_Block.speed_move, Space.World);
            else if (block.transform.position.x >= 2)
                transform.Translate(Vector2.left * moving_Block.speed_move, Space.World);
        }
    }

    public bool check_block_Down()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.down, raycastDistance * 3, layerMask_Stop))
                return true;

        return false;
    }

    public bool check_block_L()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right, layerMask_Stop))
                return false;
        return true;
    }
    public bool check_block_R()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.right, raycastDistance_left_right, layerMask_Stop))
                return false;
        return true;
    }    
}
