using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float waidToLoad = 6;
    int numberOfAttackers = 0;
    bool levelTimerFinished = false;

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        Time.timeScale = 1;
    }

    public void AttackerSpawnerd()
    {
        numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;

        if (numberOfAttackers <= 0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waidToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLostCondition()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private static void StopSpawners()
    {
        AtackerSpawner[] spawnerArray = FindObjectsOfType<AtackerSpawner>();

        foreach (AtackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }
}
