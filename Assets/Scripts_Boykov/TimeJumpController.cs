using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;


namespace Gamekit2D
{
    public sealed class TimeJumpController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BaseTimeDifferentObject[] _timeDifferentObjects;
        [SerializeField] private float _secondUseDelay = 0.5f;
        [SerializeField] private PostProcessVolume _postProcess;
        [SerializeField] private PostProcessProfile _normalProfile;
        [SerializeField] private PostProcessProfile _anomalusProfile;

        private float _timeCounter;

        private TimeState _timeState;

        private bool _isEnabled;
        private bool _isSecondUseEnabled;

        #endregion


        #region UnityMethods

        private void Update()
        {
            if (_isEnabled)
            {
                if (_isSecondUseEnabled && PlayerInput.Instance.TimeJump.Down)
                {
                    JumpThroughTime();

                    _timeCounter = _secondUseDelay;
                    _isSecondUseEnabled = false;
                }
                else
                {
                    _timeCounter -= Time.deltaTime;
                    if (_timeCounter <= 0.0f)
                    {
                        _isSecondUseEnabled = true;
                        _timeCounter = 0.0f;
                    }
                }

            }
        }

        private void OnEnable()
        {
            _isEnabled = true;
            _isSecondUseEnabled = true;

            SwitchTimeDifferentObjects(TimeState.Normal);
            _normalProfile = _postProcess.profile;
        }

        #endregion


        #region Methods

        private void JumpThroughTime()
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