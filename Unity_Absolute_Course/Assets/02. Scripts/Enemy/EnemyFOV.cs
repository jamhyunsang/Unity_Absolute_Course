using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{

    //적캐릭터의 추적 사정 거리의 범위
    public float viewRange = 15.0f;
    [Range(0, 360)]
    //적 캐릭터의 시야각
    public float viewAngle = 120.0f;

    private Transform enemyTr;
    private Transform playerTr;
    private int playerLayer;
    private int obstacleLayer;
    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        playerLayer = LayerMask.NameToLayer("Player");
        obstacleLayer = LayerMask.NameToLayer("OBSTACLE");
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }

    //주어진 각도에 의해 원주 위의 점의 좌표값을 계산하는 함수
    public Vector3 CirclePoint(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),0,Mathf.Cos(angle*Mathf.Deg2Rad));
    }

    public bool isTracePlayer()
    {
        bool isTrace = false;

        Collider[] colls = Physics.OverlapSphere(enemyTr.position, viewRange, 1 << playerLayer);
        if(colls.Length==1 )
        {
            Vector3 dir = (playerTr.position - enemyTr.position).normalized;

            if(Vector3 .Angle(enemyTr.forward,dir)<viewAngle*0.5f)
                {
                isTrace = true;
            }
        }
            return isTrace;
    }
    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit;
        Vector3 dir = (playerTr.position - enemyTr.position).normalized;
        if(Physics.Raycast(enemyTr.position,dir,out hit,viewRange,layerMask))
        {
            isView = (hit.collider.CompareTag("Player"));
        }

        return isView;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
