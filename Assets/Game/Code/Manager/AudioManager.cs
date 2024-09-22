using UnityEngine;

namespace Beyaka.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource sfx, bgm;
        [SerializeField] private AudioClip gameover, box;

        private bool isDone;

        private void OnEnable()
        {
            GameController.Instance.OnGameOver += SoundGameOver;
            GameController.Instance.OnScoring += SoundBox;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameOver -= SoundGameOver;
            GameController.Instance.OnScoring -= SoundBox;
        }

        private void SoundGameOver()
        {
            if(isDone) return;

            bgm.clip = gameover;
            bgm.loop = false;
            bgm.Play();

            isDone = true;
        }

        private void SoundBox()
        {
            PlaySFX(box);
        }

        private void PlaySFX(AudioClip clip)
        {
            sfx.PlayOneShot(clip);
        }
    }
}