using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{

    private const string bulletTag = "Bullet";
    private const string enemyTag = "Enemy";
    private float initHp = 100.0f;
    public float currHp;

    //블러드스크린 텍스처를 저장하기 위한 변수
    public Image bloodScreen;

    public Image hpBar;
    private readonly Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    private Color currColor;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;


    private void OnEnable()
    {
        GameManager.OnItemChange += UpdateSetup;
    }
    void UpdateSetup()
    {
        initHp = GameManager.instance.gameData.hp;
        currHp += GameManager.instance.gameData.hp - currHp;
    }

    // Start is called before the first frame update
    void Start()
    {
        initHp = GameManager.instance.gameData.hp;
        currHp = initHp;

        hpBar.color = initColor;
        currColor = initColor;
    }

    //충돌한 collider의 istrigger옵션이 체크 됐을 때 발생

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==bulletTag)
        {
            Destroy(other.gameObject);
            //혈흔 효과를 표현할 코루틴 함수 호출
            StartCoroutine(ShowBloodScreen());
            currHp -= 5.0f;
            Debug.Log("playerHp = " + currHp.ToString());

            DisplayHpbar();

            if(currHp<=0)
            {
                PlayerDie();
            }
        }
    }

    IEnumerator ShowBloodScreen()
    {
        //블러드스크린 텍스처의 알파값을 불규칙하게 변경
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        //블러드 스크린 텍스처의 색상을 모두 0으로 변경
        bloodScreen.color = Color.clear;

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

    void DisplayHpbar()
    {
        if((currHp/initHp)>0.5f)
        {
            currColor.r = (1 - (currHp / initHp)) * 2.0f;
        }
        else 
        {
            currColor.g = (currHp / initHp) * 2.0f;
        }
        hpBar.color = currColor;
        hpBar.fillAmount = (currHp / initHp);
    }
}
