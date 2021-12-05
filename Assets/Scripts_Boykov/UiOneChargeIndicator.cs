using UnityEngine;
using UnityEngine.UI;


namespace Gamekit2D
{
    public class UiOneChargeIndicator : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _empty;
        [SerializeField] private Sprite _full;

        #endregion


        #region Methods

        public void Set(bool isFull)
        {
            if (isFull)
            {
                _image.sprite = _full;
            }
            else
            {
                _image.sprite = _empty;
            }
        }

        #endregion
    }
}