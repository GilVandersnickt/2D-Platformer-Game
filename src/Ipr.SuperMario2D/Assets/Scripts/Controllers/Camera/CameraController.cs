using Assets.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers.Camera
{
    public class CameraController : MonoBehaviour
    {
        private ICameraService _cameraService;

        [Inject]
        public void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        void LateUpdate()
        {
            _cameraService.MoveCamera(gameObject);
        }
    }
}
