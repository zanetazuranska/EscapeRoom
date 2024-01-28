using UnityEngine;

namespace ER
{
    public class RedLight : MonoBehaviour
    {
        [SerializeField] private Light _light;

        private float _intensity = 200;
        private bool _addOrSubstract = true; //true=add, false=substract

        void Update()
        {
            if (_intensity >= 200)
            {
                _addOrSubstract = false;
            }
            if (_intensity <= 50)
            {
                _addOrSubstract = true;
            }

            if (_addOrSubstract == false)
            {
                _intensity = _intensity - 0.5f;
            }
            else
            {
                _intensity = _intensity + 0.5f;
            }

            _light.intensity = _intensity;
        }
    }
}
