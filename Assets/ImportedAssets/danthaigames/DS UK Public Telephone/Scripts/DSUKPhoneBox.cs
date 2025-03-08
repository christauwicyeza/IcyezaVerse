using System.Collections;
using UnityEngine;

public class DSUKPhoneBox : MonoBehaviour
{
    private Animator _animator;
    private Coroutine closeCoroutine;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (_animator != null)
        {
            _animator.SetBool("isOpen", true);
            RestartCloseTimer(5f);
        }
    }

    public void Close()
    {
        if (_animator != null)
        {
            _animator.SetBool("isOpen", false);
            StopCloseTimer();
        }
    }

    public void ToggleDoor()
    {
        if (_animator != null)
        {
            bool isOpen = _animator.GetBool("isOpen");
            _animator.SetBool("isOpen", !isOpen);

            if (!isOpen)
            {
                RestartCloseTimer(6f);
            }
            else
            {
                StopCloseTimer();
            }
        }
    }

    private void RestartCloseTimer(float delay)
    {
        StopCloseTimer();
        closeCoroutine = StartCoroutine(CloseAfterDelay(delay));
    }

    private void StopCloseTimer()
    {
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
            closeCoroutine = null;
        }
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Close();
    }
}
