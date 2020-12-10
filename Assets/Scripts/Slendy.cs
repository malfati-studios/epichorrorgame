using UnityEngine;

public class Slendy : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    private Vector3 originalPosition = new Vector3(8f, 0f, 50f);
    private Animator animator;

    private bool walking;
    private Vector3 startWalkingPos;
    private Vector3 endWalkingPos;

    public void Reset()
    {
        transform.position = originalPosition;
    }

    public void StartWalking(Vector3 startPos, Vector3 endPos)
    {
        animator.SetBool("Walk", true);
        walking = true;
        startWalkingPos = startPos;
        endWalkingPos = endPos;
        transform.position = startWalkingPos;
        transform.LookAt(endPos);
    }

    void Start()
    {
        transform.position = originalPosition;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            if (endWalkingPos.Equals(transform.position))
            {
                walking = false;
                animator.SetBool("Walk", false);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, endWalkingPos, walkSpeed * Time.deltaTime);
        }
    }
}