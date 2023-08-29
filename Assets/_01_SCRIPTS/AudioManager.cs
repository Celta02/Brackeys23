using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace CeltaGames
{
    public class AudioManager : SingletonPersistent<AudioManager>
    {
        [SerializeField] EventReference _exteriorAmbience;
        [SerializeField] EventReference _underWaterAmbience;
        // [SerializeField] StudioEventEmitter _attack1;

        [Header("3D Ambience Music Origin")] 
        [SerializeField] Transform _exteriorTransform;
        [SerializeField] Transform _underWaterTransform;
        [Tooltip("Height Distance where exterior ambience music will come from in Main Scene.")]
        [SerializeField] float _exteriorHeightDistance = 10f;

        EventInstance _exterior;
        EventInstance _underWater;

        void Start()
        {
            SetupMainMenuScene();
            GamePlayManager.Instance.StartMainSceneEvent += SetupMainScene;
            GamePlayManager.Instance.ArrivedToSurfaceEvent += SetupWhenArrivedToSurface;
        }
        void OnDisable()
        {
            GamePlayManager.Instance.StartMainSceneEvent -= SetupMainScene;
            GamePlayManager.Instance.ArrivedToSurfaceEvent -= SetupWhenArrivedToSurface;
        }

        void SetupMainMenuScene()
        {
            _exterior = RuntimeManager.CreateInstance(_exteriorAmbience);
            _exterior.set3DAttributes(_exteriorTransform.To3DAttributes());
            _exterior.start();

            _underWater = RuntimeManager.CreateInstance(_underWaterAmbience);
            _underWater.set3DAttributes(_underWaterTransform.To3DAttributes());
        }
        void SetupMainScene()
        {
            _exteriorTransform.position = Vector3.up * _exteriorHeightDistance;
            _underWater.start();
        }
        void SetupWhenArrivedToSurface()
        {
            _underWater.stop(STOP_MODE.IMMEDIATE);
        }
        
        
    }
}