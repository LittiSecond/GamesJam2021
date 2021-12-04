using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;


namespace Gamekit2D
{
    public sealed class TimeJumpSceneManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BaseTimeDifferentObject[] _timeDifferentObjects;
        [SerializeField] private PostProcessVolume _postProcess;
        [SerializeField] private PostProcessProfile _normalProfile;
        [SerializeField] private PostProcessProfile _anomalusProfile;

        private TimeState _timeState;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            SwitchTimeDifferentObjects(TimeState.Normal);
            _normalProfile = _postProcess.profile;
        }

        #endregion


        #region Methods

        public void JumpThroughTime()
        {
            if (_timeState == TimeState.Normal)
            {
                SwitchTimeDifferentObjects(TimeState.Anomalous);
            }
            else
            {
                SwitchTimeDifferentObjects(TimeState.Normal);
            }

            SwithPostprocessing(_timeState);
        }

        private void SwitchTimeDifferentObjects(TimeState newState)
        {
            _timeState = newState;
            if (_timeDifferentObjects.Length > 0)
            {
                for (int i = 0; i < _timeDifferentObjects.Length; i++)
                {
                    _timeDifferentObjects[i].SwitchTimeState(_timeState);
                }
            }
        }

        private void SwithPostprocessing(TimeState newState)
        {
            if (_postProcess != null)
            {
                if (_timeState == TimeState.Normal)
                {
                    _postProcess.profile = _normalProfile;
                }
                else
                {
                    _postProcess.profile = _anomalusProfile;
                }
            }
        }


        #endregion
    }
}