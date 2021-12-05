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

        [HideInInspector] public DataSettings _dataSettings;

        private float _doublePressDelay = 0.2f;
        private float _timeCounter;

        private int _currentCharges;
        private TimeState _timeState;

        private bool _isRedy;
        private bool _isEnabled;

        #endregion


        #region Properties

        public int CurrentCharges
        {
            get => _currentCharges;
        }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            PersistentDataManager.RegisterPersister(this);

            _chargeChangeEvent.Invoke(this);

            _timeCounter = _reloadDuration;
            _timeState = TimeState.Normal;
            _isRedy = true;
            _isEnabled = true;
            UpdateUi();
        }

        private void Update()
        {
            if (_isEnabled)
            {
                if (_timeState == TimeState.Normal)
                {
                    if (_isRedy)
                    {
                        if (PlayerInput.Instance.TimeJump.Down)
                        {
                            if (SpendCharge())
                            {
                                _timeState = TimeState.Anomalous;
                                _timeCounter = _anomalousDuration;
                                _useSkillEvent.Invoke();
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

                    if ( _timeCounter < _anomalousDuration - _doublePressDelay  )
                    {
                        if ((_timeCounter <= 0.0) || PlayerInput.Instance.TimeJump.Down)
                        {
                            _timeCounter = 0.0f;
                            _timeState = TimeState.Normal;
                            _isRedy = false;
                            _useSkillEvent.Invoke();
                        }
                    }
                }

            }
        }

        #endregion

        #region Methods

        public void Rechage()
        {
            _currentCharges = _maxCharges;
            _chargeChangeEvent.Invoke(this);
        }

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
            float value;

            if (_timeState == TimeState.Normal)
            {
                value = _timeCounter / _reloadDuration; 
            }
            else
            {
                value = _timeCounter / _anomalousDuration;
            }

            _timerIndicator?.SetValue(value);
        }

        #endregion


        #region IDataPersister

        public DataSettings GetDataSettings()
        {
            return _dataSettings;
        }

        public void LoadData(Data data)
        {
            Data<int> chargeData = (Data<int>)data;
            _currentCharges = chargeData.value;
        }

        public Data SaveData()
        {
            return new Data<int>(_currentCharges);
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            _dataSettings.dataTag = dataTag;
            _dataSettings.persistenceType = persistenceType;
        }

        #endregion
    }
}
