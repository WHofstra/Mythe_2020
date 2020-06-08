using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scene", menuName = "Cutscene")]
public class Cutscene : DialogueScene
{
    [SerializeField] protected Sprite[] _images;
    public Sprite[] Images { get { return _images; } }

    [SerializeField] protected float[] _imageDuration;
    public float[] ImgDuration { get { return _imageDuration; } }

    [SerializeField] protected float _imageFadeDuration;
    public float FadeDuration { get { return _imageFadeDuration; } }
}
