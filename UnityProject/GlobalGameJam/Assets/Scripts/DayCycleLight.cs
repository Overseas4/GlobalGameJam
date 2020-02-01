using UnityEngine;

public enum daytime
{
    earlyday,
    midday,
    lateday,
}

public class DayCycleLight : MonoBehaviour
{
    [SerializeField] private float _sunupintensity = 0.85f;
    [SerializeField] private float _sunriseintensity = 0.55f;
    [SerializeField] private float _nightintensity = 0.35f;
    [SerializeField] private float _daydurationinseconds = 120f;
    [SerializeField] private float _halfdaydurationinseconds = 60f;
    [SerializeField] private float _nightdurationinseconds = 20f;
    [SerializeField] private Color _colorsunrise = Color.red;
    [SerializeField] private Color _colorsunup = Color.yellow;
    [SerializeField] private Color _colorsundown = Color.blue;

    private Vector3 _rotationperseconddaytime;
    private Vector3 _rotationpersecondnighttime;
    private float _timer = 0f;
    private Light _light;
    private daytime _currentdaytime = daytime.earlyday;

    private float Ratio
    {
        get
        {
            float ratio = 0f;
            switch (_currentdaytime)
            {
                case daytime.earlyday:
                    ratio = 2f*(_timer / _halfdaydurationinseconds);
                    break;
                case daytime.midday:
                    ratio = ((_timer - _halfdaydurationinseconds) / _halfdaydurationinseconds);
                    break;
                case daytime.lateday:
                    ratio = 2f * ((_timer - _daydurationinseconds) / _nightdurationinseconds);
                    if(ratio > 1f)
                    {
                        ratio = 2f - ratio;
                    }
                    break;
            }
            return ratio;
        }
    }
    private Color LightColor
    {
        get
        {
            Color color = Color.white;
            switch (_currentdaytime)
            {
                case daytime.earlyday:
                    color = Color.Lerp(_colorsunrise, _colorsunup, Ratio);
                    break;
                case daytime.midday:
                    color = Color.Lerp(_colorsunup, _colorsunrise, Ratio);
                    break;
                case daytime.lateday:
                    color = Color.Lerp(_colorsunrise, _colorsundown, Ratio);
                    break;
            }
            return color;
        }
    }
    private float Intensity
    {
        get
        {
            float intensity = 1f;
            switch (_currentdaytime)
            {
                case daytime.earlyday:
                    intensity = Mathf.Lerp(_sunriseintensity, _sunupintensity, Ratio);
                    break;
                case daytime.midday:
                    intensity = Mathf.Lerp(_sunupintensity, _sunriseintensity, Ratio);
                    break;
                case daytime.lateday:
                    intensity = Mathf.Lerp(_sunriseintensity, _nightintensity, Ratio);
                    break;
            }
            return intensity;
        }
    }

    void Start()
    {
        initialize();
    }

    void Update()
    {
        if (_timer < _daydurationinseconds)
        {
            transform.Rotate(_rotationperseconddaytime * Time.deltaTime);
        }
        else
        {
            transform.Rotate(_rotationpersecondnighttime * Time.deltaTime);
        }
        if (_timer < _halfdaydurationinseconds)
        {
            _currentdaytime = daytime.earlyday;
        }
        else if (_timer < _daydurationinseconds)
        {
            _currentdaytime = daytime.midday;
        }
        else if (_timer < _daydurationinseconds + _nightdurationinseconds)
        {
            _currentdaytime = daytime.lateday;
        }
        else
        {
            float fulldayduration = _daydurationinseconds + _nightdurationinseconds;
            transform.rotation = Quaternion.identity;
            _timer -= fulldayduration;
        }
        _light.color = LightColor;
        _light.intensity = Intensity;
        _timer += Time.deltaTime;
    }


    public void initialize()
    {
        _timer = 0f;
        float daytimerotationpersecond = 180 / _daydurationinseconds;
        _rotationperseconddaytime = new Vector3(daytimerotationpersecond, 0f, 0f);

        _halfdaydurationinseconds = _daydurationinseconds * 0.5f;
        float nighttimerotationpersecond = 180 / _nightdurationinseconds;
        _rotationpersecondnighttime = new Vector3(nighttimerotationpersecond, 0f, 0f);
        transform.rotation = Quaternion.Euler(Vector3.zero);

        _light = GetComponent<Light>();
        _light.color = _colorsunrise;
        _light.intensity = _sunriseintensity;
    }

}
