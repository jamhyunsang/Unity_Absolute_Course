using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    private const string bulletTag = "Bullet";
    private const string enemyTag = "Enemy";
    private float initHp = 100.0f;
    public float currHp;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        currHp = initHp;
    }

    //충돌한 collider의 istrigger옵션이 체크 됐을 때 발생

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==bulletTag)
        {
            Destroy(other.gameObject);
            currHp -= 5.0f;
            Debug.Log("playerHp = " + currHp.ToString());

            if(currHp<=0)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        OnPlayerDie();
        Debug.Log("playerDie");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
    }
}
