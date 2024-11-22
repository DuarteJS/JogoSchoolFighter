using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ////Se pressionar qualquer tecla ele vai mudar de Cena
        //if (Input.GetKeyDown(KeyCode.KeypadEnter )|| Input.GetKeyDown(KeyCode.Return) )
        //{
        //    //Mudar cena
        //    SceneManager.LoadScene("Fase1");
        //}
        //se pressionar qualquer tecla
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //mudarcena
            StartCoroutine(CarregarFase("Fase1"));

        }

        //corrotina - coroutine
        IEnumerator CarregarFase(string nomeFase)
        {
            //iniciar a animacao
            //ativar o gatilho "start"
            transition.SetTrigger("Start");

            //esperar o tempo de animacao.
            yield return new WaitForSeconds(transitionTime);

            //Carregar a cena
            SceneManager.LoadScene(nomeFase);
        }

    }
}
