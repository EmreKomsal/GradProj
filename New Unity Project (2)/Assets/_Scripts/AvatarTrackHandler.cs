using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class AvatarTrackHandler : MonoBehaviour
{
    [SerializeField]
    private DefaultTrackableEventHandler _trackableEventHandler;
    [SerializeField]
    private List<Avatar> avatars;
    [SerializeField]
    private Avatar selectedAvatar;
    [SerializeField]
    private GameObject tools;
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private HeadTrackHandler headTrackHandler;

    [Header("Order")]
    public bool isAvatarSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        selectedAvatar = avatars[0];
        _trackableEventHandler.OnTargetFound.AddListener(selectedAvatar.PlayWelcome);
        _trackableEventHandler.OnTargetLost.AddListener(selectedAvatar.StopTalking);
    }

    public void ChangeSelectedAvatar(Avatar avatar)
    {
        if (!isAvatarSelected)
        {
            if (avatar != selectedAvatar)
            {
                selectedAvatar = avatar;
            }
            isAvatarSelected = true;
            selectedAvatar.StopTalking();
            selectedAvatar.PlaySelected();
            _trackableEventHandler.OnTargetFound.RemoveListener(selectedAvatar.PlayWelcome);
            _trackableEventHandler.OnTargetFound.AddListener(selectedAvatar.PlaySelected);
        }
    }

    public void SelectPlayed()
    {
        selectedAvatar.PlayTools();
        //tools.SetActive(true);
        _trackableEventHandler.OnTargetFound.RemoveListener(selectedAvatar.PlaySelected);
        _trackableEventHandler.OnTargetFound.AddListener(selectedAvatar.PlayTools);
    }

    public void ToolsPlayed()
    {
        _trackableEventHandler.OnTargetFound.RemoveListener(selectedAvatar.PlayTools);
        //tools.SetActive(false);
        _trackableEventHandler.OnTargetFound.AddListener(selectedAvatar.PlayMovie);
        selectedAvatar.PlayMovie();
    }

    public void MoviePlayed()
    {
        _trackableEventHandler.OnTargetFound.RemoveListener(selectedAvatar.PlayMovie);
        videoPlayer.gameObject.SetActive(true);
        //headTrackHandler.IsTrackable(true);
        _trackableEventHandler.OnTargetFound.AddListener(StartMovie);
        _trackableEventHandler.OnTargetLost.AddListener(StopMovie);
    }

    private void StartMovie()
    {
        if (!videoPlayer.gameObject.activeSelf)
        {
            videoPlayer.gameObject.SetActive(true);
        }
        videoPlayer.Play();
    }

    private void StopMovie()
    {
        if (videoPlayer.gameObject.activeSelf)
        {
            videoPlayer.gameObject.SetActive(false);
        }
        videoPlayer.Pause();
    }
}
