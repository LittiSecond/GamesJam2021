using UnityEngine;
using System;
using UnityEngine.Events;


namespace Gamekit2D
{
    public class TimeJumpSkill : MonoBehaviour, IDataPersister
    {
        #region PrivateData

        [Serializable] public class ChargeChangeEvent : UnityEvent<TimeJumpSkill> { };
        [Serializable] public class UseSkillEvent : UnityEvent{ };

        #endregion


        #region Fields

        [SerializeField] private ChargeChangeEvent _chargeChangeEvent;
        [SerializeField] private UseSkillEvent _useSkillEvent;
        [SerializeField] private UITimejumpTimerIndicator _timerIndicator;

        [SerializeField] private float _anomalousDuration = 10.0f;
        [SerializeField] private float _reloadDuration = 5.0f;
        [SerializeField] private int _maxCharges = 5;

        private float _timeCounter;

        private int _currentCharges;
        private TimeState _timeState;

        private bool _isRedy;
        private bool _isEnabled;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            //PersistentDataManager.RegisterPersister(this);

            _chargeChangeEvent.Invoke(this);

        }

        private void Update()
        {
            if (_isEnabled)
            {
                //if (_isSecondUseEnabled && PlayerInput.Instance.TimeJump.Down)

                if (_timeState == TimeState.Normal)
                {
                    if (_isRedy)
                    {
                        if (PlayerInput.Instance.TimeJump.Down)
                        {
                            if (SpendCharge())
                            {
                                _useSkillEvent.Invoke();
                                _timeState = TimeState.Anomalous;
                                _timeCounter = _anomalousDuration;
                            }
                        }
                    }
                    else
                    {
                        _timeCounter += Time.deltaTime;
                        UpdateUi();
                        if (_timeCounter >= _reloadDuration)
                        {
                            _isRedy = true;
                            _timeCounter = _reloadDuration;
                        }
                    }
                }
                else
                {
                    _timeCounter -= Time.deltaTime;
                    UpdateUi();
                    if ( (_timeCounter <= 0.0) || PlayerInput.Instance.TimeJump.Down)
                    {
                        _timeCounter = 0.0f;
                        _timeState = TimeState.Normal;
                        _isRedy = false;
                    }
                }

            }
        }

        #endregion

        #region Methods

        private bool SpendCharge()
        {
            if (_currentCharges <= 0)
            {
                return false;
            }

            _currentCharges--;
            _chargeChangeEvent.Invoke(this);

            return true;
        }

        private void UpdateUi()
        {
            float value = (_timeState == TimeState.Normal) ? 
                _timeCounter / _reloadDuration : _timeCounter / _anomalousDuration;
            _timerIndicator.SetValue(value);
        }

        #endregion


        #region IDataPersister

        public DataSettings GetDataSettings()
        {
            throw new System.NotImplementedException();
        }

        public void LoadData(Data data)
        {
            throw new System.NotImplementedException();
        }

        public Data SaveData()
        {
            throw new System.NotImplementedException();
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
