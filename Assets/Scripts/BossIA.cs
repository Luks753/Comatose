using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossIA : MonoBehaviour
{
    public Transform[] teleportPoints;
    Rigidbody2D body;
    public Animator animator;
    public bool followPlayer = false;
    public float mMovementSpeed = 3.0f;
    public bool bIsGoingRight = true;
    public float mRaycastingDistance = 2f;
    private SpriteRenderer _mSpriteRenderer;
    float teleportInterval = 5f;
    public Transform castPoint;
    public static Transform target;
    Path path;
    Seeker seeker;
    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    float sightDistance = 2f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player").transform;
        seeker = GetComponent<Seeker>();
        body = GetComponent<Rigidbody2D>();
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = bIsGoingRight;
        InvokeRepeating("UpdatePath", 0f, .5f);
    }


    void Update()
    {
        if (body.velocity.x != 0)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if(SeePlayer(sightDistance)){
            followPlayer = true;
        }else{
            followPlayer = false;
        }


        if (followPlayer)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            body.AddForce(force);

            float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWayPointDistance)
            {
                currentWaypoint++;
            }

            _mSpriteRenderer.flipX = body.velocity.x >= 0.01f;
            bIsGoingRight = _mSpriteRenderer.flipX;
        }
        else
        {
            if (Time.time >= teleportInterval)
            {
                //Teleport();
                teleportInterval = Time.time + 5f;
            }

            Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
            directionTranslation *= Time.deltaTime * mMovementSpeed;

            transform.Translate(directionTranslation);


            CheckForWalls();
        }
        // if the ennemy is going right, get the vector pointing to its right

    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (bIsGoingRight) ? Vector3.right : Vector3.left;

        // Raycasting takes as parameters a Vector3 which is the point of origin, another Vector3 which gives the direction, and finally a float for the maximum distance of the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), raycastDirection, 0.075f);

        // if we hit something, check its tag and act accordingly
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Terrain")
            {
                bIsGoingRight = !bIsGoingRight;
                _mSpriteRenderer.flipX = bIsGoingRight;

            }
        }
    }

    void UpdatePath()
    {

        if (seeker.IsDone() && followPlayer)
            seeker.StartPath(body.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Teleport()
    {
        int point = Random.Range(0, teleportPoints.Length);

        transform.position = teleportPoints[point].transform.position;
    }

    bool SeePlayer(float distance){
        bool val = false;
        float castDist = distance;

        if(!bIsGoingRight){
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Raycast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if(hit.collider != null){
            if(hit.collider.gameObject.CompareTag("Player")){
                val = true;
            }else{
                val = false;
            }

            Debug.DrawLine(castPoint.position, hit.point, Color.red);
        }else{
            Debug.DrawLine(castPoint.position, endPos, Color.yellow);

        }


        return val;
    }

    static void FindPlayer(){
        
    }
}
