using Assets.Scripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyArray;

    // numero de inimigos
    public int numberOfEnemies;
    private int currentEnemies;

    public float spawnTime;

    public string nextSection;


    // nao usaremos.
    void Start()
    {
        
    }

    void Update()
    {   //se atingir o mumero maximo de inimigos
        if (currentEnemies >= numberOfEnemies) 
        {
            //contar o numero de inimigos 
            int enemies = FindObjectsByType<EnemyMeleeController>(FindObjectsSortMode.None).Length;

            if (enemies <= 0)
            {
                //avanca de secao
                LevelManager.ChangeSection(nextSection);

                //Desabilitar o spawner
                this.gameObject.SetActive(false);
            }
        }
    }

    //spawnar inimigos
    void SpawnEnemy()
    {
        //Posicao de spawn de inimigos
        Vector2 spawnPosition;

        //limites y
        // 1.453 superior
        // 0.747 inferior
        spawnPosition.y = Random.Range(1.45f, -0.74f);

        // posicao x maximo (direita) do confiner da camera + 1 de distancia
        // pegar o rightBound (limite direita ) da section (confiner) como base
        float rightSectionBound = LevelManager.currentConfiner.BoundingShape2D.bounds.max.x;

        //Define o x do spawnPosition, igual ao ponto da DIREITA do confiner
        spawnPosition.x = rightSectionBound;

        //instancia ("Spawna") os inimigos
        //pega um inimigo aleatorio
        //spawna na posicao SpawnPosition
        //Quaternion e uma classe utilizada para trabalhar com rotacoes
        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPosition, Quaternion.identity).SetActive(true);

        //incrementa o contador de inimigos
        currentEnemies++;

        //se o jnumero de inimigos na cena for menor que o numero que o numero maximo de inimigos
        //invoca novamente(++)
        if (currentEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        { 
            //desative o colisor para indicar o spawning apenas uma vez
            //ATENCAO:Desabilita o collider, mas o objeto Spawner continua ativo

            this.GetComponent<BoxCollider2D>().enabled= false;

            //invoca pela primeira vez a funcao SpawnEnemy
            SpawnEnemy();
        }
    }
}
   