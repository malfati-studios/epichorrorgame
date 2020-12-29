using UnityEngine;

public class Slendy : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    private Vector3 originalPosition = new Vector3(8f, 0f, 50f);
    private Animator animator;

    private bool walking;
    private bool screaming;
    private bool running;

    private Vector3 startWalkingPos;
    private Vector3 endWalkingPos;
    private GameObject player;
    [SerializeField] private AudioSource screamSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject deathparticleSystem;

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

    public void StartChase(Vector3 startPos, Vector3 endPos)
    {
        animator.SetBool("Run", true);
        running = true;
        startWalkingPos = startPos;
        endWalkingPos = endPos;
        transform.position = startWalkingPos;
        screamSound.Play();
        transform.LookAt(endPos);
    }

    void Start()
    {
        transform.position = originalPosition;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (walking)
        {
            if (endWalkingPos.Equals(transform.position))
            {
                walking = false;
                animator.SetBool("Walk", false);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, endWalkingPos, walkSpeed * Time.deltaTime * 2);
        }

        if (running)
        {
            if (endWalkingPos.Equals(transform.position))
            {
                running = false;
                animator.SetBool("Run", false);
                screamSound.Stop();
                GameManager.instance.LostTheGame();
                return;
            }

            endWalkingPos = PlayerMovement.instance.GetFeetPosition();
            transform.position = Vector3.MoveTowards(transform.position, endWalkingPos, walkSpeed * Time.deltaTime * 2);
        }

        if (screaming)
        {
            transform.LookAt(player.transform.position);
        }
    }

    public void StartScreamSound()
    {
        screamSound.Play();
        PlayerLook.instance.ShakeCamera(0.1f, 1f);
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

    public void Die()
    {
        screamSound.Stop();
        deathSound.Play();
        animator.SetBool("Run", false);
        animator.SetBool("Die", true);
        PlayerLook.instance.ShakeCamera(0.2f, 4.5f);
        running = false;
        Invoke("PLayExplosion", 1f);
        Invoke("PLayExplosion", 2f);
        Invoke("PLayExplosion", 3f);
        Invoke("PLayExplosion", 4f);
        Invoke("FinallyDie", 4.5f);
        
    }
    
    private void FinallyDie()
    {
        deathSound.Stop();
        Reset();
        LightsController.instance.TurnOnAllLights();
        AudioController.instance.StopAmbienceSound();
    }

    private void PLayExplosion()
    {
        deathparticleSystem.GetComponent<ParticleSystem>().Play();
    }
}