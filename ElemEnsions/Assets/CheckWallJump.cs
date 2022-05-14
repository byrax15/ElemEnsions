using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckWallJump : MonoBehaviour
{
    private Vector3 contact;
    private Collider collidingWall;
    private float dist = 100.0f;
    [SerializeField] float wallDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
       // CheckWallCollision();
    }

    private void CheckWallCollision()
    {
        List<Collider> walls = Physics.OverlapSphere(transform.position, wallDist).Where(col => col.transform.CompareTag("Wall")).ToList();
         float dist = 100.0f;
        if (walls.Count == 0)
            collidingWall = null;
        foreach (Collider c in walls)
        {
            float distance = (c.transform.position - transform.position).magnitude;

            if (distance < dist)
            {
                collidingWall = c;
                dist = distance;
            }
        }
    }

    public Transform GetWall()
    {
        CheckWallCollision();
        if(collidingWall != null)
        {
            return collidingWall.transform;
        }
        return null;
    }
}
