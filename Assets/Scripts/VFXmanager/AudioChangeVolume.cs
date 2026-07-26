using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer Group;
    public string floatparam = "MyExposedParam";

    public void ChangeValue(float f)
    {
        Group.SetFloat(floatparam, f);
    }
}
