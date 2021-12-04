using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Gamekit2D
{
    public class UITimejumpTimerIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Slider _slider;

        #endregion


        #region Methods

        public void SetValue(float value)
        {
            _slider.value = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        #endregion
    }
}