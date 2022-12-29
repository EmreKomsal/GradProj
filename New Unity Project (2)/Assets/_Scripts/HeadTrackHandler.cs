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

    [SerializeField]
    private GameObject MovieSceneObj;
    [SerializeField]
    private GameObject PlayButtonObj;


    [SerializeField]
    private HeadTrackAvatar avatar;

    [SerializeField]
    private bool isCalibrated = false;


    // Start is called before the first frame update
    void Start()
    {
        _oldPos = gameObject.transform.position;
        _trackableEventHandler.OnTargetFound.AddListener(avatar.PlayInfo);
        _trackableEventHandler.OnTargetLost.AddListener(avatar.StopInfo);

        _trackableEventHandler.OnTargetFound.AddListener(SwitchTrack);
        _trackableEventHandler.OnTargetLost.AddListener(SwitchTrack);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_trackable && trackFound && isCalibrated)
        {
            float curr_move = Vector3.Magnitude(gameObject.transform.position - _oldPos);

            Debug.Log("Curr_move" + curr_move);

            if (curr_move > moveCap)
            {
                Debug.LogWarning("Your Head Moved");
                if (videoPlayer.isPlaying)
                {
                    StartCoroutine(StopMoving(3f));
                }
            }

            _oldPos = gameObject.transform.position;
        }

        if (avatar.GetHeadCalibrated() != isCalibrated && !isCalibrated)
        {
            isCalibrated = avatar.GetHeadCalibrated();
            StartMovie();
            _oldPos = gameObject.transform.position;
            _trackableEventHandler.OnTargetFound.RemoveListener(avatar.PlayInfo);
            _trackableEventHandler.OnTargetLost.RemoveListener(avatar.StopInfo);
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
        videoPlayer.Pause();
        yield return new WaitForSeconds(seconds);

        Debug.Log("Current Sound Stopped");
        warningText.SetActive(false);
        videoPlayer.Play();
    }


    private void StartMovie()
    {
        if (!MovieSceneObj.activeInHierarchy)
        {
            Debug.Log("Scene Ready");
            MovieSceneObj.SetActive(true);
            PlayButtonObj.SetActive(true);
        }
    }
}
