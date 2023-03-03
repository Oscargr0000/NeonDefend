using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    public void PlaySound(GameObject entity, AudioClip sounds)
    {
        entity.GetComponent<AudioSource>().PlayOneShot(sounds);
    }
}
