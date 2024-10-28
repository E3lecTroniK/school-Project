using UnityEngine;

public class MyAudioMannager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip Main;

    private void start()
    {
        MusicSource.clip = Main;
        MusicSource.Play();

    }

}
