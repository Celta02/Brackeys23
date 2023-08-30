using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static FMOD.Studio.STOP_MODE;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace CeltaGames
{
    public class AudioManager : SingletonPersistent<AudioManager>
    {
        [Header("Music")]
        [SerializeField] EventReference _mainTheme;
        [SerializeField] EventReference _mainThemeEnd;
        [SerializeField] EventReference _VictoryTheme;
        [SerializeField] EventReference _defeatTheme;
        [SerializeField] EventReference _drownTheme;
        
        [Header("Ambience")]
        [SerializeField] EventReference _exterior;
        [SerializeField] EventReference _underWater;
        // [SerializeField] StudioEventEmitter _attack1;

        [Header("3D Ambience Music Origin")] 
        [SerializeField] Transform _exteriorTransform;
        [SerializeField] Transform _underWaterTransform;
        [Tooltip("Height Distance where exterior ambience music will come from in Main Scene.")]
        [SerializeField] float _exteriorHeightDistance = 10f;

        EventInstance _mainThemeInstance;
        EventInstance _exteriorInstance;
        EventInstance _underWaterInstance;
        EventInstance _mainThemeEndInstance;
        EventInstance _victoryThemeInstance;
        EventInstance _defeatThemeInstance;
        EventInstance _drownThemeInstance;

        void Start()
        {
            SetupEventInstances();
            _exteriorInstance.start();
        }

        void OnEnable()
        {
            GamePlayManager.Instance.StartMainSceneEvent += SetupMainScene;
            GamePlayManager.Instance.ArrivedToSurfaceEvent += SetupWhenArrivedToSurface;
            GamePlayManager.Instance.StartVictorySceneEvent += SetupVictoryScene;
            GamePlayManager.Instance.StartDefeatSceneEvent += SetupDefeatScene;
            GamePlayManager.Instance.StartDrownSceneEvent += SetupDrownScene;
        }
        void OnDisable()
        {
            GamePlayManager.Instance.StartMainSceneEvent -= SetupMainScene;
            GamePlayManager.Instance.ArrivedToSurfaceEvent -= SetupWhenArrivedToSurface;
            GamePlayManager.Instance.StartVictorySceneEvent -= SetupVictoryScene;
            GamePlayManager.Instance.StartDefeatSceneEvent -= SetupDefeatScene;
            GamePlayManager.Instance.StartDrownSceneEvent -= SetupDrownScene;
        }

        void SetupEventInstances()
        {
            _mainThemeInstance = RuntimeManager.CreateInstance(_mainTheme);
            _mainThemeEndInstance = RuntimeManager.CreateInstance(_mainThemeEnd);
            _victoryThemeInstance = RuntimeManager.CreateInstance(_VictoryTheme);
            _defeatThemeInstance = RuntimeManager.CreateInstance(_defeatTheme);
            _drownThemeInstance = RuntimeManager.CreateInstance(_drownTheme);

            
            _exteriorInstance = RuntimeManager.CreateInstance(_exterior);
            _exteriorInstance.set3DAttributes(_exteriorTransform.To3DAttributes());
            
            _underWaterInstance = RuntimeManager.CreateInstance(_underWater);
            _underWaterInstance.set3DAttributes(_underWaterTransform.To3DAttributes());
        }
        void SetupMainScene()
        {
            _victoryThemeInstance.stop(IMMEDIATE);
            _defeatThemeInstance.stop(IMMEDIATE);
            _drownThemeInstance.stop(IMMEDIATE);
            
            _exteriorTransform.position = Vector3.up * _exteriorHeightDistance;
            _underWaterInstance.start();
            _mainThemeInstance.start();
        }
        void SetupWhenArrivedToSurface()
        {
            _underWaterInstance.stop(IMMEDIATE);
            _mainThemeInstance.stop(ALLOWFADEOUT);
            _mainThemeEndInstance.start();
        }
        void SetupVictoryScene() => _victoryThemeInstance.start();
        void SetupDefeatScene() => _defeatThemeInstance.start();
        void SetupDrownScene()
        {
            _underWaterInstance.stop(ALLOWFADEOUT);
            _mainThemeInstance.stop(ALLOWFADEOUT);
            _drownThemeInstance.start();
        }
    }
}