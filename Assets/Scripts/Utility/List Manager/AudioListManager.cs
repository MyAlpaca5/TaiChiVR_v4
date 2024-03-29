﻿namespace TaiChiVR.Utility.ListManager
{
    using System.Collections;
    using TaiChiVR.Utility.Data;
    using TaiChiVR.Utility.Scriptables;
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class AudioListManager : MonoBehaviour
    {
        public static AudioListManager Instance;

        [SerializeField] TerrainListScriptable terrainListScriptable = null;

        private AudioSource audioSource = null;
        private AudioClip[] terrainAudioList = null;
        private int totalClipNum;
        private int currentPlayingIndex = 0; // Index from 0 to (totalClipNum - 1) -> Terrain Audio; Index totalClipNum -> Default Audio

        void Awake() 
        {
            Instance = this;
        }

        public IEnumerable Initialization()
        {       
            audioSource = gameObject.GetComponent<AudioSource>();

            totalClipNum = terrainListScriptable.terrainList.Length;
            terrainAudioList = new AudioClip[totalClipNum + 1];

            for (int i = 0; i < totalClipNum; i++)
            {
                terrainAudioList[i] = terrainListScriptable.terrainList[i].backgroundMusic;
                yield return null;
            }
            terrainAudioList[totalClipNum] = audioSource.clip;
        }

        public void PlayAudio(int index)
        {
            if (currentPlayingIndex != index)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                audioSource.clip = terrainAudioList[index];
                audioSource.PlayDelayed(0.2f);
                currentPlayingIndex = index;
            }
        }

        public void PlayDefaultAudio()
        {           
            PlayAudio(totalClipNum);
        }

        public void SetVolume(float value)
        {
            audioSource.volume = value;
        }

        public float GetVolume()
        {
            return audioSource.volume;
        }
    }
}