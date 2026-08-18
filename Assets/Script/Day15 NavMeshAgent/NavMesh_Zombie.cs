using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieStatu
{
    Patrol = 1,//巡逻状态
    Track = 2,//追踪状态
    Attack = 3,//攻击状态

}
[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NavMesh_Zombie : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent navMesh;
    public Animator animator;
    ZombieStatu zombieStatu;
    Vector3 _zombieStopPosition, _zombiePatrolPosition;//丧尸停下来的位置，丧尸往返巡逻的另一个位置
    float _maxTrack, _minAttack;//最大追击,最小攻击距离
    float _patrolSpeed, _trackSpeed;//巡逻速度，追踪速度
    void Start()
    {
        zombieStatu = ZombieStatu.Attack;//丧尸状态默认为攻击
        _maxTrack = 20;
        _minAttack = 2;
        _patrolSpeed = 1.2f;
        _trackSpeed = 3.5f;
        _zombieStopPosition = transform.position;
        _zombiePatrolPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        ZombieBehaviour(distance);
    }

    /// <summary>
    /// 丧尸的行为改变 巡逻 追踪 攻击
    /// </summary>
    /// <param name="distance">丧尸到目标点的距离</param>
    public void ZombieBehaviour(float distance)
    {
        if (distance > _maxTrack)
        {
            if (zombieStatu != ZombieStatu.Patrol)//如果首次切换为巡逻就执行
            {
                zombieStatu = ZombieStatu.Patrol;//更改丧尸状态为巡逻
                navMesh.speed = _patrolSpeed;
                navMesh.ResetPath();//清除路径
                animator.SetBool("IsRun", false);//动画更新 - walk
                _zombiePatrolPosition = GetPatrolPositon();
                navMesh.destination = _zombiePatrolPosition;
            }
            ZombiePatrol();

        }
        else if (distance <= _maxTrack && distance > _minAttack)
        {
            if (zombieStatu != ZombieStatu.Track)//如果首次切换为追踪就执行
            {
                navMesh.speed = _trackSpeed;
                zombieStatu = ZombieStatu.Track;//更改丧尸状态为追击
                animator.SetBool("IsRun", true);//动画更新 - run
            }
            navMesh.destination = player.transform.position;//设置目标点
        }
        else
        {
            if (zombieStatu != ZombieStatu.Attack)//如果首次切换为攻击就执行
            {
                zombieStatu = ZombieStatu.Attack;//更改丧尸状态为攻击
            }
            animator.SetTrigger("Attack"); //动画更新 - Attack
            navMesh.ResetPath();

        }
    }
    /// <summary>
    /// 丧尸巡逻逻辑
    /// </summary>
    public void ZombiePatrol()
    {
        if (_zombiePatrolPosition != Vector3.zero)
        {
            //如果目标点是终点并且已经到达，就设置目标点为起点
            if (navMesh.destination == _zombiePatrolPosition && Vector3.Distance(transform.position, _zombiePatrolPosition) < 1f)
            {
                Debug.Log("到达终点");
                navMesh.destination = _zombieStopPosition;
            }
            //如果目标点是起点并且已经到达，就设置目标点为终点
            if (navMesh.destination == _zombieStopPosition && Vector3.Distance(transform.position, _zombieStopPosition) < 1f)
            {
                Debug.Log("到达起点");
                navMesh.destination = _zombiePatrolPosition;
            }
        }
        else
        {
            navMesh.ResetPath();
        }

    }
    /// <summary>
    /// 获取丧尸有效巡逻点
    /// </summary>
    /// <returns> 
    /// Vector3.zero - 未找到有效点
    /// Other - 有效点
    /// </returns>
    public Vector3 GetPatrolPositon()
    {
        _zombieStopPosition = transform.position;//记录当前停止位置
        Vector3[] _zombiePatrolPositionTemp = new Vector3[4];//临时变量，获取有效巡逻点
        NavMeshHit hit;
        _zombiePatrolPositionTemp[0] = _zombieStopPosition + new Vector3(10, 0, 0);
        _zombiePatrolPositionTemp[1] = _zombieStopPosition + new Vector3(-10, 0, 0);
        _zombiePatrolPositionTemp[2] = _zombieStopPosition + new Vector3(0, 0, 10);
        _zombiePatrolPositionTemp[3] = _zombieStopPosition + new Vector3(0, 0, -10);
        //检测四个目标点，如果存在有效点便返回，否则为0对象
        foreach (var item in _zombiePatrolPositionTemp)
        {
            if (NavMesh.SamplePosition(item, out hit, 5, NavMesh.AllAreas))
            {
                Debug.Log("返回的是有效点");
                return hit.position;
            }
        }
        Debug.Log("返回的是无效点");
        return Vector3.zero;//未存在目标点，返回0对象

    }
}
