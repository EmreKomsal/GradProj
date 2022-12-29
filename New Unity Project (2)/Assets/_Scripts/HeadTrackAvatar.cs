using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrackAvatar : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField]
    private HeadTrackHandler avatarController;

    [Header("Visual")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string trigger_talk = "Talk";
    [SerializeField]
    private string trigger_stop = "Stop";


    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;


    [Header("States")]
    private bool headCalibrated = false;


    public bool GetHeadCalibrated()
    {
        return headCalibrated;
    }

    public void SetHeadCalibrated(bool state)
    {
        headCalibrated = state;

    }

    public void PlayInfo()
    {
        audioSource.Play();
        animator.SetTrigger(trigger_talk);
        Debug.Log("Playing: " + audioSource.clip.name);
        StartCoroutine(StopTalking(10f));
    }

    IEnumerator StopTalking(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Debug.Log("Current Sound Stopped");
        animator.SetTrigger(trigger_stop);
    }

    public void StopInfo()
    {
        Debug.Log("Current Sound Stopped__");
        animator.SetTrigger(trigger_stop);
        audioSource.Stop();
    }
}
