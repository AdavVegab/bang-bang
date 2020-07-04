using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioProcessing : MonoBehaviour
{
    //Parameter
    public float _minBufferDecrease = 0.005f;
    public float _bufferDecreaseSpeed = 1.2f;
    // Audio
    AudioSource _audioSource;  
    public float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    public static float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];
    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];




    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // Get the Spectrum and fill the Bands
        GetSpectrumAudioSource();
        MakeFrequencyBand();
        BandBuffer();
        CreateAudioBands();
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if(_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = _minBufferDecrease;
            }
            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;

            }
        }
    }

    void MakeFrequencyBand()
    {
        /*
         * 22050 / 512 = 43 per Sample
         * 
         * 20 - 60 Hz
         * 50 - 250 Hz
         * 250 - 500 Hz
         * 500 - 2000 Hz
         * 2000 - 4000 Hz
         * 4000 - 6000 Hz
         * 6000 - 200000 Hz
         * 
         * 0 -> 2   = 86 Hz    -> 0 - 87 Hz
         * 1 -> 4   = 172 Hz   -> 87 - 258 Hz
         * 2 -> 8   = 344 Hz   -> 259 - 602 Hz
         * 3 -> 16  = 688 Hz   -> 603 - 1290 Hz
         * 4 -> 32  = 1376 Hz  -> 1291 - 2666 Hz
         * 5 -> 64  = 2752 Hz  -> 2667 - 5418 Hz
         * 6 -> 128 = 5504 Hz  -> 5419 - 10922 Hz
         * 7 -> 256 = 11008 Hz -> 10923 - 21930 Hz
         * 
         */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);
            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;

            _freqBand[i] = average * 10;
        }

    }
}
