using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Source
{
	public class AudioManager : MonoBehaviour
	{
		public AudioMixerGroup mixerGroup;

		public Sound[] sounds;

		private void Start()
		{
			Play("Theme", out _);
		}

		void Awake()
		{

			foreach (Sound s in sounds)
			{
				s.source = gameObject.AddComponent<AudioSource>();
				s.source.clip = s.clip;
				s.source.loop = s.loop;

				s.source.outputAudioMixerGroup = mixerGroup;
			}
		}

		public void Play(string sound, out float length)
		{
			length = 0;
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}

			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

			s.source.Play();

			length = s.source.time;
		}

		public void Stop(string sound)
		{
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}
			s.source.Stop();
		}

	}
}
