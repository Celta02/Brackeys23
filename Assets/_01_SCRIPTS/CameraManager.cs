using System;
using Cinemachine;
using UnityEngine;

namespace CeltaGames
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _targetCamera;
        [SerializeField] CinemachineVirtualCamera _fixedCamera;
        void OnEnable()
        {
            GamePlayManager.Instance.EnteredToTheWaterEvent += ChangeToTargetCamera;
            GamePlayManager.Instance.ArrivedToSurfaceEvent += ChangeToFixedCamera;
        }

        void OnDisable()
        {
            GamePlayManager.Instance.EnteredToTheWaterEvent -= ChangeToTargetCamera;
            GamePlayManager.Instance.ArrivedToSurfaceEvent -= ChangeToFixedCamera;
        }

        void Start() => ChangeToFixedCamera();

        void ChangeToTargetCamera()
        {
            _targetCamera.Priority = 10;
            _fixedCamera.Priority = 1;
        }
        void ChangeToFixedCamera()
        {
            _targetCamera.Priority = 1;
            _fixedCamera.Priority = 10;
        }
    }
}