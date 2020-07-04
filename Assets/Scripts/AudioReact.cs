using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReact : MonoBehaviour
{
    public int _band;
    float _start_Position;
    float _max_Distance;
    float _start_Scale = 1f;
    public float _scaleMultiplier = 10f;
    public float _maximalPosition = -3f;
    public bool _useBuffer;

    // Start is called before the first frame update
    void Start()
    {
       _start_Scale = transform.localScale.y;

        _start_Position = transform.localPosition.y;
        _max_Distance = _maximalPosition - _start_Position;
        Debug.Log(_max_Distance);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            //transform.localScale = new Vector3(transform.localScale.x, (AudioProcessing._bandBuffer[_band] * _scaleMultiplier) + _start_Scale, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, _start_Position + (AudioProcessing._audioBandBuffer[_band] * _max_Distance), transform.localPosition.z);
            


        } else
        {
            //transform.localScale = new Vector3(transform.localScale.x, (AudioProcessing._freqBand[_band] * _scaleMultiplier) + _start_Scale, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, _start_Position + (AudioProcessing._audioBand[_band] * _max_Distance), transform.localPosition.z);        
        }
    }
}
