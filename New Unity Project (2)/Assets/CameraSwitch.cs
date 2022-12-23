using UnityEngine;
using Vuforia;

public class CameraSwitch : MonoBehaviour
{
    private bool cameraMode = false;

    public void OnCameraChangeMode()
    {
        CameraDevice.CameraDirection currentDir = CameraDevice.Instance.GetCameraDirection();
        if (!cameraMode)
        {
            RestartCamera(CameraDevice.CameraDirection.CAMERA_FRONT);
            cameraMode = true;
            Debug.Log("Back Camera");
        }
        else
        {
            RestartCamera(CameraDevice.CameraDirection.CAMERA_BACK);
            cameraMode = false;
            Debug.Log("Front Camera");
        }
    }

    private void RestartCamera(CameraDevice.CameraDirection newDir)
    {
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        CameraDevice.Instance.Init(newDir);
        CameraDevice.Instance.Start();
    }
}
