using JetBrains.Annotations;
using System;
using RDCT.Item;
using UnityEngine;

    [CreateAssetMenu(fileName = "Flashlight", menuName ="Items", order = 1)]
    public class FlashlightBaseAttribute : BaseItem
    {
        [Header("Base Attribute")]
        [SerializeField] private float _flashlightStrengh;
        [SerializeField] private float _flashlightRange;
        [SerializeField] private bool _flashlightEnabled;


        [Header("Battery Attribute")]
        [SerializeField] private float _batteryLevel;
        [SerializeField] private float _maxBatteryLevel;
        [SerializeField] private bool _hasBatteryLevel;


        #region Get Flashlight General attribute
        public float getFlashlightStrengh()
        {
            return _flashlightStrengh;
        }

        public float getFlashlightRange()
        {
            return _flashlightRange;
        }

        public bool isFlashlightEnabled([CanBeNull] bool isEventInterupt, [CanBeNull] bool isEnabled)
        {
            if (isEventInterupt)
                return isEnabled;

            if (_hasBatteryLevel)
                return true;
            else
                return false;
        }

        #endregion

        #region Get Battery Attribute

        public float getBatteryLevel()
        {
            if (_batteryLevel > _maxBatteryLevel)
            {
                Math.Clamp(_batteryLevel, 0, _maxBatteryLevel);
            }

            return _batteryLevel;
        }

        public float getMaxBatteryLevel()
        {
            return _maxBatteryLevel;
        }

        public bool hasBatteryLevel()
        {
            if (_batteryLevel > 0)
                return true;
            else
                return false;
        }
        #endregion

    }
