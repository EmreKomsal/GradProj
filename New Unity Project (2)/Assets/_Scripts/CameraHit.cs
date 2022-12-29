using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraHit : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private AvatarTrackHandler avatarController;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(touch.position), out hit))
            {
                Avatar selectedAvatar = hit.collider.GetComponent<Avatar>();
                if (selectedAvatar)
                {
                    if (!avatarController.isAvatarSelected)
                    {
                        avatarController.ChangeSelectedAvatar(selectedAvatar);
                    }
                }
            }
        }

        //PC Debug

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Avatar selectedAvatar = hit.collider.GetComponent<Avatar>();
                if (selectedAvatar)
                {
                    if (!avatarController.isAvatarSelected)
                    {
                        avatarController.ChangeSelectedAvatar(selectedAvatar);
                    }
                }
            }
        }
    }
}
