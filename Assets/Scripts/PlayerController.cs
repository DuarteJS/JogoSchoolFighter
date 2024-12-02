using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //fisica
    private Rigidbody2D playerRigibody;

    //velocidade
    public float playerSpeed = 1f;
    public float currentSpeed;

    //direcao do player (X,Y)
    public Vector2 playerDirection;

    //animacao de andar
    private bool isWalking;

    private Animator playerAnimator;

    //Player olhando para a direita.
    private bool playerFacingRight = true;

    //variavel contadora (soco)
    public int punchCount;

    //tempo de ataque (f=float)
    private float timeCross = 1.20f;

    private bool comboControl;


    //Indicar se o player esta morto
    private bool isDead;

    void Start()
    {
        //obtem e inicializa as propriedades do RigidBody2D;
        playerRigibody = GetComponent<Rigidbody2D>();

        //obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();

        currentSpeed = playerSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        //para ser mais rapido por isso usamos o Update.
        UpdateAnimator();

        


        //jabe
        //keycode = saber qual tecla pressionou
        if (Input.GetKeyDown(KeyCode.X))
        {
            //if (isWalking == false)
            //{
                
                    
                if (punchCount < 2)
                {
                    //se o isWalking for falso,chame playerJab.
                    PlayerJab();
                    punchCount++;

                    if (!comboControl)
                    {
                        //iniciar o temporizador
                        StartCoroutine(CrossController());
                    }
                }
                else if (punchCount >= 2)
                {
                    PLayerCross();
                    punchCount = 0;
                }
            //}
        //parando o temporizador
        StopCoroutine(CrossController());
        }

        ////cross
        ////keycode = saber qual tecla pressionou
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    if (isWalking == false) {
        //        PLayerCross();
        //    }

        //}

    }

    //Fixed Update geralmente e utilizada para implementacao de fisica no jogo,
    //por ter uma execucao padronizada em diferentes dispositivos.
    private void FixedUpdate()
    {
        //Verificar se o player estara em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            //se torna verdadeiro
            isWalking = true;
        }
        else
        {
            //se torna falso
            isWalking = false;


        }
        //calculo para a movimentacao do player
        //playerRigibody.MovePosition(playerRigibody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);
        playerRigibody.MovePosition(playerRigibody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);

    }
    //mover o player
    void PlayerMove()
    {
        // pega a entrada do jogador,e cria um vector2 para usar o playerDirection;
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //se o vai se movimentar para a ESQUERDA e estava olhando para a DIREITA
        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }

        //Se o player vai para a DIREITA  e esta olhando para a ESQUERDA
        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }

    }

    void UpdateAnimator()
    {
        // definir o valor do parametro do animator, igual a propriedade isWalking.
        //"isWalking" = animacao da Unity.
        playerAnimator.SetBool("isWalking", isWalking);


    }
    void Flip()
    {
        //Vai girar o sprite player em 180 grau no eixo y

        //inverter o valor da variavel playerFacingRight
        playerFacingRight = !playerFacingRight;

        //girar o sprite do player em 180 graus no eixo y
        //x , y ,z (eixos para trocar na programacao)
        transform.Rotate(0, 180, 0);
    }
    void PlayerJab()
    {
        //acessa a animacao do player
        playerAnimator.SetTrigger("isJab");
    }

    //void vai te retornar oque tiver dentro
    void PLayerCross()
    {
        playerAnimator.SetTrigger("isCross");

    }


    //corrotina acontece paralelo
    IEnumerator CrossController()
    {
        comboControl = true;
    yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;
    }
        
    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }
} 

