using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Wave[] waves;                                // Le tableau contenant toutes les vagues du niveau.
    public double startTimeBetweenWaves = 5;            // Le temps entre deux vagues
    private double timeBetweenWaves;                    // Le compteur permettant d'attendre le temps entre deux vagues.

    private int currentWaveIndex = 0;                   // L'index de la vague actuel dans le tableau.

    void Start()
    {
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
            for (int indexEnemy = 0; indexEnemy < waves[currentWaveIndex].enemiesList.Length; indexEnemy++)
            {
                waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns = waves[currentWaveIndex].enemiesList[indexEnemy].startTimeBetweenSpawns;
                waves[currentWaveIndex].enemiesList[indexEnemy].leftToKill = waves[currentWaveIndex].enemiesList[indexEnemy].count;
                waves[currentWaveIndex].enemiesList[indexEnemy].leftToSpawn = waves[currentWaveIndex].enemiesList[indexEnemy].count;
            }
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
                for (int indexEnemy = 0; indexEnemy < waves[currentWaveIndex].enemiesList.Length; indexEnemy++)
                {
                    // Si le timer entre deux spawns n'est pas lancé, alors on spawn puis on le lance
                    if (waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns == waves[currentWaveIndex].enemiesList[indexEnemy].startTimeBetweenSpawns)
                    {
                        SpawnNextEnnemy(indexEnemy);
                        waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns -= Time.deltaTime;
                    }
                    else
                    {
                        waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns -= Time.deltaTime;
                        if (waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns <= 0)
                        {
                            waves[currentWaveIndex].enemiesList[indexEnemy].timeBetweenSpawns = waves[currentWaveIndex].enemiesList[indexEnemy].startTimeBetweenSpawns;
                        }
                    }
                }
            }
            else
                Debug.Log("Il n'y a plus de vague");
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
            Instantiate(waves[currentWaveIndex].enemiesList[indexOfEnemy].enemyPrefab, transform.position, Quaternion.identity);
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
        currentWaveIndex++;
        timeBetweenWaves -= Time.deltaTime;
    }
}
