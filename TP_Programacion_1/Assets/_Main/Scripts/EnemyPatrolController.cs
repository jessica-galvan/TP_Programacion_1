using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform detectionPoint;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDetectionDistance;
    [SerializeField] private LayerMask playerDetectionList;
    [SerializeField] private float playerDetectionDistance;
    private Rigidbody2D rb2d;
    private bool facingRight;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //maxDistance = transform.position.x + maxDistance;
        //minDistance = transform.position.x + minDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D hitPatrol = Physics2D.Raycast(transform.position, Vector2.down, groundDetectionDistance, groundDetectionList);

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, Vector2.right, playerDetectionDistance, playerDetectionList  );
        if (hitPlayer)
        {
            transform.position += hitPlayer.collider.transform.position;
        }
        
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
