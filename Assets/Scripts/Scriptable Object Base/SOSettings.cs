using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class SOSettings : ScriptableObject 
{
    [Header("Gameplay Settings")]
    [Tooltip("Sensitivity setting. 0: Not move at all, 5: 1mm = hell of rotations in character")]
    public float sensitivity;

    [Header("Video Settings")]
    [Tooltip("Display Mode setting. 0: Fullscreen, 1: Windowed Fullscreen, 2: Window")]
    public int displayMode;
    [Tooltip("Resolution setting. 0: 1280x720, 1: 1366x768, 2: 1600x900, 3: 1920x1080")]
    public int resolution;

    [Header("Audio Settings")]
    [Range(-80f, 0f)]
    [Tooltip("Master volume setting. -80: Dead silent, 0: Very Loud")]
    public float masterVolume;
    [Range(-80f, 0f)]
    [Tooltip("BGM volume setting. -80: Dead silent, 0: Very Loud")]
    public float BGMVolume;
    [Range(-80f, 0f)]
    [Tooltip("SFX and Voice Line volume setting. -80: Dead silent, 0: Very Loud")]
    public float SFXVolume;

    [Header("Graphics Settings")]
    [Tooltip("Graphics quality setting. 0: Low, 1: Normal, 2: High, 3: Ultra")]
    public int quality;
    [Tooltip("Anti aliasing setting. 0: disabled, 1: 2x MSAA, 2: 4x MSAA, 3: 8x MSAA, 4: 16x MSAA")]
    public int antiAliasing;
    [Tooltip("SSO setting. (WIP)")]
    public int SSO;
    [Tooltip("Post processing setting. (WIP)")]
    public int postProcessing;
    [Tooltip("Max FPS setting. 0: Uncapped, 1: 30, 2: 60, 3: 90, 4: 120, 5: 144, 6: 165, 7: 240")]
    public int maxFPS;
}