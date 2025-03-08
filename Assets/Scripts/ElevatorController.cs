using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform lift;
    public float normalSpeed = 0.7f;
    public float aboveNormalSpeed = 2f;
    public float highSpeed = 3.5f;
    public float startY;
    public float maxY;
    public AudioSource elevatorSound;
    public float basePitch = 1f; 
    public float pitchMultiplier = 0.2f; 

    private float currentSpeed;
    private bool isMoving = false;
    private bool isStopped = false;
    private bool reachedTop = false;
    private bool reachedBottom = true;
    private Transform player;

    void Start()
    {
        currentSpeed = normalSpeed;
        startY = lift.position.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            player.SetParent(lift);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetParent(null);
            player = null;
        }
    }

    public void MoveUp()
    {
        if (!isMoving && !reachedTop)
        {
            isStopped = false;
            reachedBottom = false;
            StartCoroutine(MoveElevator(true));
        }
    }

    public void MoveDown()
    {
        if (!isMoving && !reachedBottom)
        {
            isStopped = false;
            reachedTop = false;
            StartCoroutine(MoveElevator(false));
        }
    }

    public void StopElevator()
    {
        isStopped = true;
        isMoving = false;
        StopAllCoroutines();
        StopElevatorSound();
    }

    public void SetNormalSpeed()
    {
        currentSpeed = normalSpeed;
        AdjustSoundPitch();
    }

    public void SetAboveNormalSpeed()
    {
        currentSpeed = aboveNormalSpeed;
        AdjustSoundPitch();
    }

    public void SetHighSpeed()
    {
        currentSpeed = highSpeed;
        AdjustSoundPitch();
    }

    IEnumerator MoveElevator(bool movingUp)
    {
        isMoving = true;
        PlayElevatorSound();

        while (!isStopped)
        {
            float targetY = movingUp ? maxY : startY;

            lift.position = Vector3.MoveTowards(
                lift.position,
                new Vector3(lift.position.x, targetY, lift.position.z),
                currentSpeed * Time.deltaTime
            );

            if (Mathf.Abs(lift.position.y - targetY) < 0.01f)
            {
                lift.position = new Vector3(lift.position.x, targetY, lift.position.z);
                isMoving = false;
                StopElevatorSound();

                if (movingUp)
                    reachedTop = true;
                else
                    reachedBottom = true;

                yield break;
            }

            yield return null;
        }
    }

    private void PlayElevatorSound()
    {
        if (elevatorSound != null && !elevatorSound.isPlaying)
        {
            elevatorSound.loop = true;
            AdjustSoundPitch();
            elevatorSound.Play();
        }
    }

    private void StopElevatorSound()
    {
        if (elevatorSound != null && elevatorSound.isPlaying)
        {
            elevatorSound.Stop();
        }
    }

    private void AdjustSoundPitch()
    {
        if (elevatorSound != null)
        {
            elevatorSound.pitch = basePitch + (currentSpeed * pitchMultiplier);
        }
    }
}
