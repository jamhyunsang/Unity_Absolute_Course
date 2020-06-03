using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Create Info")]
    public Transform[] points;
    public GameObject enemy;
    public float createTime = 2.0f;
    public int maxEnemy = 10;
    public bool isGameOver = false;

    public static GameManager instance = null;

    [Header("Object Pool")]
    public GameObject bulletPrefab;
    public int maxPool = 10;
    public List<GameObject> bulletPool = new List<GameObject>();
    private void Awake()
    {
        if(instance == null)
        {
            instance=this;
        }
        else if( instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        CreatePooling();
    }

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if(points.Length>0)
        {
            StartCoroutine(this.CreateEnemy());
        }
    }

    IEnumerator CreateEnemy()
    {
        while(!isGameOver)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if(enemyCount<maxEnemy)
            {
                yield return new WaitForSeconds(createTime);
                int idx = Random.Range(0, points.Length);
                Instantiate(enemy, points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }

public GameObject GetBullet()
    {
        for(int i = 0; i < bulletPool.Count; i++)
        {
            if(bulletPool[i].activeSelf==false)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("ObjectPools");
        for(int i = 0; i < maxPool;i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);
            obj.name = "Bullet_" + i.ToString("00");
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }
}
