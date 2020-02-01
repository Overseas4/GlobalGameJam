//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Experimental.GlobalIllumination;

//public enum DayTime
//    {
//        EarlyDay,
//        MidDay,
//        LateDay,
//    }

//public class DayCicleLight : MonoBehaviour
//{
//    [SerializeField] private float _sunUpIntensity = 0.85f;
//    [SerializeField] private float _sunRiseIntensity = 0.55f;
//    [SerializeField] private float _nightIntensity = 0.35f;
//    [SerializeField] private float _dayDurationInSeconds = 180f;
//    [SerializeField] private float _halfDayDurationInSeconds = 90f;
//    [SerializeField] private float _nightDurationInSeconds = 20f;
//    [SerializeField] private Color _colorSunRise = Color.red;
//    [SerializeField] private Color _colorSunUp = Color.yellow;
//    [SerializeField] private Color _colorSunDown = Color.blue;

//    private Vector3 _rotationPerSecondDayTime;
//    private Vector3 _rotationPerSecondNightTime;
//    private float _timer = 0f;
//    //private Light _light;
//    private DayTime _currentDayTime = DayTime.EarlyDay;
//    void Start()
//    {
//        Initialize();
//    }

//    void Update()
//    {
//        //if (_timer < _dayDurationInSeconds)
//        //{
//            transform.Rotate(_rotationPerSecondDayTime * Time.deltaTime);
//        //}
//        //if (_timer < _halfDayDurationInSeconds)
//        //{
//        //    _currentDayTime = DayTime.EarlyDay;
//        //}
//        //else if (_timer < _dayDurationInSeconds)
//        //{
//        //    _currentDayTime = DayTime.MidDay;
//        //}
//        //else if (_timer < _nightDurationInSeconds + _dayDurationInSeconds)
//        //{
//        //    _currentDayTime = DayTime.LateDay;
//        //}
//        //else
//        //{
//        //    float fullDayDuration = _dayDurationInSeconds + _nightDurationInSeconds;
//        //    _timer -= fullDayDuration;
//        //}
//        //_timer += Time.deltaTime;
//    }

//    private float Ratio
//    {
//        get
//        {
//            float ratio = 0f;
//            switch (_currentDayTime)
//            {
//                case DayTime.EarlyDay:
//                    ratio = _timer / _halfDayDurationInSeconds;
//                    break;
//                case DayTime.MidDay:
//                    ratio = (_timer - _halfDayDurationInSeconds) / _halfDayDurationInSeconds;
//                    break;
//                case DayTime.LateDay:
//                    ratio = (_timer - _dayDurationInSeconds) / _nightDurationInSeconds;
//                    break;
//            }
//            return ratio;
//        }
//    }

//    private Color LightColor
//    {
//        get
//        {
//            Color color = Color.white;
//            switch (_currentDayTime)
//            {
//                case DayTime.EarlyDay:
//                    color = Color.Lerp(_colorSunRise, _colorSunUp, Ratio);
//                    break;
//                case DayTime.MidDay:
//                    color = Color.Lerp(_colorSunUp, _colorSunRise, Ratio);
//                    break;
//                case DayTime.LateDay:
//                    color = Color.Lerp(_colorSunRise, _colorSunDown, Ratio);
//                    break;
//            }
//            return color;
//        }
//    }

//    private float Intensity
//    {
//        get
//        {
//            float intensity = 1f;
//            switch (_currentDayTime)
//            {
//                case DayTime.EarlyDay:
//                    intensity = Mathf.Lerp(_sunRiseIntensity, _sunUpIntensity, Ratio);
//                    break;
//                case DayTime.MidDay:
//                    intensity = Mathf.Lerp(_sunUpIntensity, _sunRiseIntensity, Ratio);
//                    break;
//                case DayTime.LateDay:
//                    intensity = Mathf.Lerp(_sunRiseIntensity, _nightIntensity, Ratio);
//                    break;
//            }
//            return intensity;
//        }
//    }

//    void Initialize()
//    {
//        //_timer = 0f;
//        //float daytimeRotationPerSecond = 180 / _dayDurationInSeconds;
//        //_rotationPerSecondDayTime = new Vector3(daytimeRotationPerSecond, 0f, 0f);
//        //This code crashes
        
//        //_halfDayDurationInSeconds = _dayDurationInSeconds * 0.5f;
//        //float nighttimeRotationPerSecond = 180 / _nightDurationInSeconds;
//        //_rotationPerSecondNightTime = new Vector3(nighttimeRotationPerSecond, 0f, 0f);
//        //-----------------------------
//        //transform.rotation = Quaternion.Euler(Vector3.zero);
        
//        //_light = GetComponent<Light>();
//        //_light.color = _colorSunRise;
//        //_light.intensity = _sunRiseIntensity;
//    }
//}
