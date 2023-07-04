using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private Moving_block moving_Block;
    public float raycastDistance = 1f;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        moving_Block = GetComponent<Moving_block>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, layerMask) && !moving_Block.stop)
        {
            moving_Block.stop = true;
            foreach (GameObject jun in moving_Block.block_junior)
                jun.gameObject.layer = 3;
            moving_Block.spawn_modosl();
        }
        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);
    }

    public void check_block ()
    {
        foreach (GameObject block in moving_Block.block_junior)
        {
            if (block.transform.position.x <= -6)
                transform.Translate(Vector2.right * moving_Block.speed_move, Space.World);
            else if (block.transform.position.x >= 2)
                transform.Translate(Vector2.left * moving_Block.speed_move, Space.World);
        }    
    }    
}
