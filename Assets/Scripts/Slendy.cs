using UnityEngine;

public class Slendy : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    private Vector3 originalPosition = new Vector3(8f, 0f, 50f);
    private Animator animator;

    private bool walking;
    private bool screaming;
    private Vector3 startWalkingPos;
    private Vector3 endWalkingPos;
    private GameObject player;
    [SerializeField] private AudioSource screamSound;

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
        player = GameObject.FindGameObjectWithTag("Player");
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

        if (screaming)
        {
            transform.LookAt(player.transform.position);
        }
    }

    public void StartScreamSound()    
    {
        screamSound.Play();
        CameraShake.ShakeCamera(0.2f, .9f);
    }

    public void Scream(Vector3 position)
    {
        transform.position = position;
        transform.LookAt(player.transform.position);
        animator.SetBool("Scream", true);
    }

    private void StopScreaming()
    {
        screaming = false;
        animator.SetBool("Scream", false);
        screamSound.Stop();
        Reset();
    }
}