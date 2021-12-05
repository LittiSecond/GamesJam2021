using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;


namespace Gamekit2D
{
    public sealed class TimeJumpSceneManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BaseTimeDifferentObject[] _timeDifferentObjects;
        [SerializeField] private PostProcessVolume _postProcess;
        [SerializeField] private PostProcessProfile _normalProfile;
        [SerializeField] private PostProcessProfile _anomalusProfile;

        private static TimeJumpSceneManager _instance;

        private List<BaseTimeDifferentObject> _additionalObjects = new List<BaseTimeDifferentObject>();

        private TimeState _timeState;

        #endregion


        #region Properties

        public static TimeJumpSceneManager Instance
        {
            get { return _instance; }
        }

        #endregion



        #region UnityMethods

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        private void OnEnable()
        {
            SwitchTimeDifferentObjects(TimeState.Normal);
            _normalProfile = _postProcess.profile;
            _timeState = TimeState.Normal;
        }

        #endregion


        #region Methods

        public void JumpThroughTime()
        {
            if (_timeState == TimeState.Normal)
            {
                _timeState = TimeState.Anomalous;
            }
            else
            {
                _timeState = TimeState.Normal;
            }

            SwitchTimeDifferentObjects(_timeState);

            SwithPostprocessing(_timeState);
        }

        private void SwitchTimeDifferentObjects(TimeState newState)
        {
            if (_timeDifferentObjects.Length == 0)
            {
                _timeDifferentObjects = GameObject.FindObjectsOfType<BaseTimeDifferentObject>();
            }
            if (_timeDifferentObjects.Length > 0)
            {
                for (int i = 0; i < _timeDifferentObjects.Length; i++)
                {
                    _timeDifferentObjects[i].SwitchTimeState(newState);
                }

                for (int i = 0; i < _additionalObjects.Count; i++)
                {
                    _additionalObjects[i].SwitchTimeState(newState);
                }
            }
        }

        private void SwithPostprocessing(TimeState newState)
        {
            if (_postProcess != null)
            {
                if (newState == TimeState.Normal)
                {
                    _postProcess.profile = _normalProfile;
                }
                else
                {
                    _postProcess.profile = _anomalusProfile;
                }
            }
        }

        public void Add(BaseTimeDifferentObject newObject)
        {
            if (!_additionalObjects.Contains(newObject))
            {
                _additionalObjects.Add(newObject);
            }
        }

        public void Remove(BaseTimeDifferentObject obj)
        {
            _additionalObjects.Remove(obj);
        }

        #endregion
    }
}