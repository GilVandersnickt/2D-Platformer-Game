using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class CameraService : ICameraService
    {
        private float xMin = 0;
        private float xMax = Constants.Maps.XMax;
        private float yMin = 0;
        private float yMax = 0;

        public void MoveCamera(GameObject camera)
        {
            var player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
            camera.transform.position = new Vector3(x, y, camera.transform.position.z);
        }
    }
}
