using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public static CinemachineConfiner2D currentConfiner;

        private CinemachineBrain brain;
        private CinemachineCamera cam;

        static BoxCollider2D currentSection;

        void Start()
        {
            brain = CinemachineBrain.GetActiveBrain(0);
            currentConfiner = GameObject.Find("CM").GetComponent<CinemachineConfiner2D>();
        }

        //metodo para  mudar o confiner da camera
        public static void ChangeSection(string sectionName)
        {
            //procura pelo objeto que contem p nome (sectionName)
            //pegar o colisor dele para ser o novo confiner 2d
            currentSection = GameObject.Find(sectionName).GetComponent<BoxCollider2D>();

            //se o objeto for encontrado e tiver o colisor
            if (currentSection ) 
            { 
                //faz com que  a camera limpe o cache do confiner anterior,esquecer o confiner anterior
                currentConfiner.InvalidateBoundingShapeCache();

                //define o novo confiner da camera
                currentConfiner.BoundingShape2D = currentSection;

                //reposicionar o right limiter, alinhado maximo x do confiner (direita do confiner)
                GameObject rightLimiter = GameObject.Find("Right");

                //vector3 (x, y, z)
                rightLimiter.transform.position = new Vector3 (
                    currentConfiner.BoundingShape2D.bounds.max.x, 
                    rightLimiter.transform.position.y
                    );

            }
        }

//nao usaremos
        void Update()
        {

        }
    }
}