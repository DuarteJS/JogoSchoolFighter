using UnityEngine;

public class MusicController : MonoBehaviour
{
    //classe responsavel por controlar qualquer tipo de audio
    private AudioSource audioSource;

    //audio click e o arquivo de audio que sera executado
    public AudioClip levelMusic;    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //ao iniciar o musicController,inicia a musica da fase
        PlayMusic(levelMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        //Define a musica que ira ser reproduzido
        audioSource.clip = music;

        //Reproduz o som 
        audioSource.Play();
    }
}
