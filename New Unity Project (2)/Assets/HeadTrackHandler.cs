using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class HeadTrackHandler : MonoBehaviour
{
    [SerializeField]
    private DefaultTrackableEventHandler _trackableEventHandler;

    [SerializeField]
    private bool _trackable = false;

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private Vector3 _oldPos = Vector3.zero;

    [SerializeField]
    private float moveCap = 0.01f;

    [SerializeField]
    private bool trackFound = false;

    [SerializeField]
    private GameObject warningText;


    // Start is called before the first frame update
    void Start()
    {
        _oldPos = gameObject.transform.position;
        _trackableEventHandler.OnTargetFound.AddListener(SwitchTrack);
        _trackableEventHandler.OnTargetLost.AddListener(SwitchTrack);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_trackable && trackFound)
        {
            float curr_move = Vector3.Magnitude(gameObject.transform.position - _oldPos);

            Debug.Log("Curr_move" + curr_move);

            if (curr_move > moveCap)
            {
                Debug.LogWarning("Your Head Moved");
                videoPlayer.Stop();
                StartCoroutine(StopMoving(1f));
            }
            if (curr_move < moveCap)
            {
                videoPlayer.Play();
            }

            _oldPos = gameObject.transform.position;
        }
    }

    public void SwitchTrackable()
    {
        _trackable = !_trackable;
    }

    
    void SwitchTrack()
    {
        trackFound = !trackFound;
    }

    IEnumerator StopMoving(float seconds)
    {
        warningText.SetActive(true);
        yield return new WaitForSeconds(seconds);

        Debug.Log("Current Sound Stopped");
        warningText.SetActive(true);
    }


}
