using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource drumsStart;
    public AudioSource drumsMid;
    public AudioSource drumsHigh;

    [Range(0,1f)]
    public float[] drumsStartVolumePerStar;
    [Range(0, 1f)]
    public float dStartLerpSpeed;
    [Space]
    [Range(0, 1f)]
    public float[] drumsMidVolumePerStar;
    [Range(0, 1f)]
    public float dMidLerpSpeed;
    [Space]
    [Range(0, 1f)]
    public float[] drumsHighVolumePerStar;
    [Range(0, 1f)]
    public float dHighLerpSpeed;
    [Space]


    public int starCount;
    
    void Update()
    {
        starCount = NpcCopManager.starLevel;

        if (starCount > 0)
        {
            drumsStart.volume = drumsStart.volume + (dStartLerpSpeed * (drumsStartVolumePerStar[starCount - 1] - drumsStart.volume));
            drumsMid.volume = drumsMid.volume + (dMidLerpSpeed * (drumsMidVolumePerStar[starCount - 1] - drumsMid.volume));
            drumsHigh.volume = drumsHigh.volume + (dHighLerpSpeed * (drumsHighVolumePerStar[starCount - 1] - drumsHigh.volume));
        }
        else {
            drumsStart.volume = drumsStart.volume * (1 - dStartLerpSpeed);
            drumsMid.volume = drumsMid.volume * (1 - dMidLerpSpeed);
            drumsHigh.volume = drumsHigh.volume * (1 - dHighLerpSpeed);


        }
    }
}
