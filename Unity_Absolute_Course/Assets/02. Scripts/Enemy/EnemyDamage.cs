using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private const string bulletTag = "Bullet";
    //체력
    private float hp = 100.0f;
    //피격시 사용할 혈흔 효과
    private GameObject bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        //혈흔 효과 프리팹을 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            //혈흔 효과를 생성하는 함수 호출
            ShowBloodEffect(collision);
            //총알 삭제
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            //체력 차감
            hp -= collision.gameObject.GetComponent<BulletCtrl>().damage;
            if (hp <= 0.0f)
            {
                //적 캐릭터 상태를 Die로 변경
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
        }
    }
        void ShowBloodEffect(Collision collision)
        {
            //총알이 충돌한 지점 산출
            Vector3 pos = collision.contacts[0].point;
            //총알이 충돌 했을때의 법선 벡터
            Vector3 _normal = collision.contacts[0].normal;
            //총알의 충돌 시 방향 벡터의 회전값 계산
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, _normal);

            //혈흔 효과 생성
            GameObject blood = Instantiate(bloodEffect, pos, rot);
            Destroy(blood, 1.0f);
        }
    

}
