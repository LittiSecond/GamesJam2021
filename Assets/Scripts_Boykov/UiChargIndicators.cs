using UnityEngine;


namespace Gamekit2D
{
    public class UiChargIndicators : MonoBehaviour
    {
        #region Fields

        [SerializeField] private UiOneChargeIndicator[] _indicators;

        #endregion


        #region Methods

        public void ChangeCharges(TimeJumpSkill timeJumpSkill)
        {
            int charges = timeJumpSkill.CurrentCharges;

            if (_indicators != null)
            {
                for (int i = 0; i < _indicators.Length; i++)
                {
                    if (i < charges)
                    {
                        _indicators[i].Set(true);
                    }
                    else
                    {
                        _indicators[i].Set(false);
                    }
                }
            }
        }

        #endregion


    }
}