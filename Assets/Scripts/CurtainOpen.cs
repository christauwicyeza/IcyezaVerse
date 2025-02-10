using UnityEngine;

public class CurtainOpen : MonoBehaviour
{
    public Animator curtain1Animator; 
    public Animator curtain2Animator;  

    public void OpenCurtains()
    {
        curtain1Animator.Play("open1");  
        curtain2Animator.Play("open2");  
    }
}
