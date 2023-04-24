using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private float spawnRange = 9;
    public int wavenumber=1;
    public int enemyCount;
    public GameObject[] powerupPrefabs;

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;


    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPos(),powerupPrefabs[randomPowerup].transform.rotation);
        spawnEnemyWave(wavenumber);

        // Instantiate(enemyPrefab,new Vector3(0,0,6),enemyPrefab.transform.rotation);        
    }

    private Vector3 GenerateSpawnPos(){
        float spawnPosX=Random.Range(-spawnRange, spawnRange);
        float  spawnPosZ=Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos=new Vector3(spawnPosX, 0,spawnPosZ);
        return randomPos;

    }

    void SpawnBossWave(int currentRound)
{
        int miniEnemysToSpawn;
        //We dont want to divide by 0!
        if (bossRound != 0)
        {
        miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
        miniEnemysToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GenerateSpawnPos(),
        bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
}

    public void SpawnMiniEnemy(int amount)
{
        for(int i = 0; i < amount; i++)
        {
        int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
        Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPos(),
        miniEnemyPrefabs[randomMini].transform.rotation);
}
}


    void spawnEnemyWave(int enemiestospawn){
        for(int i=0;i<enemiestospawn;i++){
            int randomEnemy= Random.Range(0,enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy],GenerateSpawnPos(),enemyPrefab[randomEnemy].transform.rotation);

        }
    }

    // Update is called once per frame
    void Update()
    { 
        enemyCount = FindObjectsOfType<Enemy>().Length;
    if(enemyCount == 0)
    {
    wavenumber++;
    //Spawn a boss every x number of waves
    if (wavenumber % bossRound == 0)
    {
        SpawnBossWave(wavenumber);
    }
    else
    {
        spawnEnemyWave(wavenumber);
    }
    //Updated to select a random powerup prefab for the Medium Challenge
    int randomPowerup = Random.Range(0, powerupPrefabs.Length);
    Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPos(),powerupPrefabs[randomPowerup].transform.rotation);
    }
}
}
