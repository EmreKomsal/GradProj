using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField]
    private AvatarTrackHandler avatarController;

    [Header("Visual")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string trigger_talk = "Talk";
    [SerializeField]
    private string trigger_cheer = "Cheer";
    [SerializeField]
    private string trigger_stop = "Stop";
    [SerializeField]
    private string trigger_movie = "Movie";

    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioList;
    [SerializeField]
    private int id_welcome = 0;
    [SerializeField]
    private int id_cheer = 1;
    [SerializeField]
    private int id_tools = 2;
    [SerializeField]
    private int id_movie = 3;


    [Header("States")]
    public bool playedWelcome = false;
    public bool playedTools = false;
    public bool playedSelected = false;
    public bool playedMovie = false;
    public bool playedAll = false;
    // Start is called before the first frame update
    
    private void PlayNext()
    {
        if (playedSelected & !playedTools)
        {
            avatarController.SelectPlayed();
        }
        if (playedTools & !playedMovie)
        {
            avatarController.ToolsPlayed();
        }
        if (playedMovie & !playedAll)
        {
            avatarController.MoviePlayed();
            playedAll = true;
        }
        if (playedAll)
        {
            return;
        }
    }

    private void Play(int soundID, string triggerID)
    {
        audioSource.clip = audioList[soundID];
        audioSource.Play();
        animator.SetTrigger(triggerID);
        Debug.Log("Playing: " + audioList[soundID].name);
    }

    public void PlayWelcome()
    {
        Debug.Log("Play Welcome");
        Play(id_welcome, trigger_talk);
        StartCoroutine(StopTalking(8.124f, state =>  playedWelcome = state));
    }

    public void PlaySelected()
    {
        Debug.Log("Play Selected");
        Play(id_cheer, trigger_cheer);
        StartCoroutine(StartTalking(1.167f));
        StartCoroutine(StopTalking(7.7f, state => playedSelected = state));
    }

    public void PlayTools()
    {
        Play(id_tools, trigger_talk);
        Debug.Log("Play Tools");
        StartCoroutine(StopTalking(8f, state => playedTools = state));
    }

    public void PlayMovie()
    {
        Play(id_movie, trigger_talk);
        Debug.Log("Play Movie");
        StartCoroutine(StopTalking(10f, state => playedMovie = state)); 
    }

    IEnumerator StartTalking(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Debug.Log("Change To Talk");
        animator.SetTrigger(trigger_talk);
    }

    IEnumerator StopTalking(float seconds, System.Action<bool> state)
    {
        yield return new WaitForSeconds(seconds);

        Debug.Log("Current Sound Stopped");
        animator.SetTrigger(trigger_stop);
        state(true);
        PlayNext();
    }

    public void StopTalking()
    {
        Debug.Log("Current Sound Stopped__");
        animator.SetTrigger(trigger_stop);
        audioSource.Stop();
    }
}
