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
    private HeadTrackHandler headTrackHandler;



    [SerializeField]
    private bool stateSelect = false;
    [SerializeField]
    private bool stateTools = false;
    [SerializeField]
    private bool stateMovie = false;


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
        selectedAvatar.PlayTools(); //Call avatar object function
        stateSelect = true; //Change State value
        tools.SetActive(true); //Activate Object From Object Pool
        _trackableEventHandler.OnTargetFound.RemoveListener(selectedAvatar.PlaySelected); //Remove this function from event call
        _trackableEventHandler.OnTargetFound.AddListener(selectedAvatar.PlayTools); //Addd this function to event call
    }

    public void ToolsPlayed()
    {
        _trackableEventHandler.OnTargetFound.RemoveListener(selectedAvatar.PlayTools);
        stateTools = true;
        tools.SetActive(false);
        _trackableEventHandler.OnTargetFound.AddListener(selectedAvatar.PlayMovie);
        selectedAvatar.PlayMovie();
    }

    public void MoviePlayed()
    {
        Debug.Log("Movie Played Test");
        _trackableEventHandler.OnTargetFound.RemoveListener(selectedAvatar.PlayMovie);
        stateMovie = true;
        //StartMovie();
        //headTrackHandler.IsTrackable(true);
        //_trackableEventHandler.OnTargetFound.AddListener(StartMovie);
        //_trackableEventHandler.OnTargetLost.AddListener(StopMovie);
    }


}
