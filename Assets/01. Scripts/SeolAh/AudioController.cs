using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer = null; 
    [SerializeField] private List<Slider> audioSliders = new List<Slider>();

    public void MasterVolme() {
        audioMixer.SetFloat("Master", audioSliders[0].value);
    }
    public void BGMVolme() {
        audioMixer.SetFloat("BGM", audioSliders[1].value);
    }
    public void SFXVolme() {
        audioMixer.SetFloat("SFX", audioSliders[2].value);
    }
}