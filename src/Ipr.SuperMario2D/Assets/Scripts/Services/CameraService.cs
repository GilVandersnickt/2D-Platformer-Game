using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class CameraService : ICameraService
    {
        public void MoveCamera(GameObject camera)
        {
            var player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            float x = Mathf.Clamp(player.transform.position.x, Constants.Maps.XMin, Constants.Maps.XMax);
            float y = Mathf.Clamp(player.transform.position.y, Constants.Maps.YMin, Constants.Maps.YMax);
            camera.transform.position = new Vector3(x, y, camera.transform.position.z);
        }
    }
}
