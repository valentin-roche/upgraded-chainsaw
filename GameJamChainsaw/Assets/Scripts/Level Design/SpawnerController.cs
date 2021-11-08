using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private ProjectorManager projectorManager;

    public Wave[] waves;                                // Le tableau contenant toutes les vagues du niveau.
    private int currentWaveIndex = 0;                   // L'index de la vague actuel dans le tableau.
    public double startTimeBetweenWaves = 5;            // Le temps entre deux vagues
    private double timeBetweenWaves;                    // Le compteur permettant d'attendre le temps entre deux vagues.

    private GameObject[] spawnPositions;                // Le tableau contenant les positions où les ennemis peuvent spawn.

    public int maxIteration = 50;                       // Le nombre d'itération max pour trouver un ennemi à faire spawn.
    private int currentIteration = 0;                   // Le nombre actuel d'itération

    private Animator eyeAnimator;                       // Référence sur l'animator des yeux
    [SerializeField]
    private WaveNumberUpdater wnu;                      // Reference vers le composant charge de mettre a jouer le num de vague sur l'UI

    void Start()
    {
        projectorManager = GameObject.FindGameObjectWithTag("ProjectorManager").GetComponent<ProjectorManager>();
        eyeAnimator = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPosition");
        if(spawnPositions.Length == 0)
        {
            print("T'as oublié de mettre des spawn positions en fils de ton spawner bg");
            spawnPositions[0] = gameObject;         // J'aimerais que ce soit jamais call svp.
        }

        timeBetweenWaves = startTimeBetweenWaves;
        if (waves.Length <= 0)
        {
            Debug.Log("Faudra songer à mettre des vagues dans ton niveau zebi...");
        }
        else
        {
            GetNextWaveInfo();
        }
    }

    // Permet de récupérer les infos de la prochaine vague.
    private void GetNextWaveInfo()
    {
        if (currentWaveIndex <= waves.Length - 1)
        {
            eyeAnimator.SetBool("uwuEye", false);
            for (int indexEnemy = 0; indexEnemy < waves[currentWaveIndex].enemiesList.Length; indexEnemy++)
            {
                waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns = waves[currentWaveIndex].enemiesList[indexEnemy].startTimeBetweenSpawns;
                waves[currentWaveIndex].enemiesList[indexEnemy].leftToKill = waves[currentWaveIndex].enemiesList[indexEnemy].count;
                waves[currentWaveIndex].enemiesList[indexEnemy].leftToSpawn = waves[currentWaveIndex].enemiesList[indexEnemy].count;
                waves[currentWaveIndex].timeBetweenSpawns = waves[currentWaveIndex].startTimeBetweenSpawns;
            }

            wnu.SetWaveNumber(currentWaveIndex.ToString());
        }
        else
        {
            Debug.Log("Bravo ! Tu as battu toutes les vagues champion");
        }
    }

    private void Update()
    {
        // Si on est pas entre deux vagues, le décompte entre les vagues n'a pas encore commencer
        if (timeBetweenWaves == startTimeBetweenWaves)
        {
            if (currentWaveIndex < waves.Length)
            {
                bool shouldSpawn = false;

                // Voir si on doit faire spawn un ennemi
                if(waves[currentWaveIndex].timeBetweenSpawns == waves[currentWaveIndex].startTimeBetweenSpawns)
                {
                    shouldSpawn = true;
                    waves[currentWaveIndex].timeBetweenSpawns -= Time.deltaTime;
                }
                else
                {
                    waves[currentWaveIndex].timeBetweenSpawns -= Time.deltaTime;
                    if (waves[currentWaveIndex].timeBetweenSpawns <= 0)
                    {
                        waves[currentWaveIndex].timeBetweenSpawns = waves[currentWaveIndex].startTimeBetweenSpawns;
                    }
                }

                // Mettre à jour le timer de spawn de chaque ennemi
                for (int indexEnemy = 0; indexEnemy < waves[currentWaveIndex].enemiesList.Length; indexEnemy++)
                {
                    // Si le timer est lancé, alors on le continue
                    if (waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns < waves[currentWaveIndex].enemiesList[indexEnemy].startTimeBetweenSpawns)
                    {
                        waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns -= Time.deltaTime;
                        if (waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns <= 0)
                        {
                            waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns = waves[currentWaveIndex].enemiesList[indexEnemy].startTimeBetweenSpawns;
                        }
                    }
                }

                // Sélectionner l'ennemi qu'on va faire spawn (si on doit en faire spawn) en prenant un random avec un nombre max d'itération
                while (shouldSpawn && currentIteration < maxIteration)
                {
                    int randIndex = Random.Range(0, waves[currentWaveIndex].enemiesList.Length);
                    if (waves[currentWaveIndex].enemiesList[randIndex].timeBetweenSpawns == waves[currentWaveIndex].enemiesList[randIndex].startTimeBetweenSpawns && waves[currentWaveIndex].enemiesList[randIndex].leftToSpawn > 0)
                    {
                        SpawnNextEnnemy(randIndex);
                        waves[currentWaveIndex].enemiesList[randIndex].timeBetweenSpawns -= Time.deltaTime;
                        shouldSpawn = false;
                    }

                    currentIteration++;
                }
                currentIteration = 0;
            }
            //else
                //Debug.Log("Il n'y a plus de vague");
        }
        else
        {
            timeBetweenWaves -= Time.deltaTime;
            if (timeBetweenWaves <= 0)
            {
                GetNextWaveInfo();
                timeBetweenWaves = startTimeBetweenWaves;
            }
        }
    }

    // Fonction appelée à la mort d'un ennemi pour signaler qu'il y en a un de moins en vie.
    public void DeathOfEnnemy(int enemyId)
    {
        int indexEnemy = IndexOfEnemy(enemyId);
        if (indexEnemy >= 0)
        {
            waves[currentWaveIndex].enemiesList[indexEnemy].leftToKill--;
            if (!IsThereEnnemiesLeft())
            {
                EndOfWave();
            }
        }
        else
        {
            Debug.Log("Y'a un truc bizarre là poto, faut check la manière donc tu récupères");
        }
    }

    // Fonction renvoyant un booléen indiquant s'il reste des ennemis en vie
    public bool IsThereEnnemiesLeft()
    {
        for (int indexEnemy = 0; indexEnemy < waves[currentWaveIndex].enemiesList.Length; indexEnemy++)
        {
            if (waves[currentWaveIndex].enemiesList[indexEnemy].leftToKill > 0)
                return true;
        }
        return false;
    }

    // Fonction utilisée pour récupérer l'index de l'ennemi dans la liste des ennemis
    public int IndexOfEnemy(int enemyId)
    {
        for (int indexEnemy = 0; indexEnemy < waves[currentWaveIndex].enemiesList.Length; indexEnemy++)
        {
            if (waves[currentWaveIndex].enemiesList[indexEnemy].enemyPrefab.GetComponent<EnemyCollisionControlerGeneric>().enemyScriptable.id == enemyId)
                return indexEnemy;
        }
        return -1;
    }

    // Fonction utilisée pour faire spawn le prochain ennemi.
    private void SpawnNextEnnemy(int indexOfEnemy)
    {
        if (waves[currentWaveIndex].enemiesList[indexOfEnemy].leftToSpawn > 0)
        {
            // On choisit une position aléatoire parmis les positions où les ennemis peuvent spawn
            int randPos = Random.Range(0, spawnPositions.Length);

            Instantiate(waves[currentWaveIndex].enemiesList[indexOfEnemy].enemyPrefab, spawnPositions[randPos].transform.position, Quaternion.identity);
            waves[currentWaveIndex].enemiesList[indexOfEnemy].leftToSpawn--;
        }
        else
        {
            //Debug.Log("La vague est finie faut plus rien invoquer bg");

            //timeBetweenWaves -= Time.deltaTime;           // Si on veut que le décompte entre les vagues commence au moment ou on aurait invoquer un ennemi de plus
        }
    }

    // Fonction à appeler pour signaler la fin de la vague
    private void EndOfWave()
    {
        eyeAnimator.SetBool("uwuEye", true);
        currentWaveIndex++;
        timeBetweenWaves -= Time.deltaTime;
    }
}
