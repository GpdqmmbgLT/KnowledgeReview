using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;//子弹预制体
    public Transform bulletsParent;//子弹创建下的父物体
    public Camera cameras;//摄像机
    public int weaponNum;//子弹编号
    public float timer;//计时器
    public int[] weaponNums = { 1, 2, 3 };//编号列表
    public KeyCode[] keyCodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };//预设的按键
    public GameObjectPool bulletsPool;
    Dictionary<int, WeaponParameters> weaponData;
    void Start()
    {
        weaponNum = 1;
        bulletsPool = GetComponent<GameObjectPool>();
        weaponData = ConfigData.weaponData;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                weaponNum = weaponNums[i];
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && timer >= weaponData[weaponNum].fireRate)
        {
            ShutBullets(weaponNum);
            timer = 0;
        }
    }
    public void ShutBullets(int num)
    {
        Vector3 pos = new Vector3(0, 4, 0);
        GameObject bullet = bulletsPool.GetObj(bulletPrefab, pos,
                                        Quaternion.identity, bulletsParent);
        bullet.GetComponent<BulletsTest>().Init(weaponData[num].damage, weaponData[num].bulletSpeed, weaponData[num].bulletColor, cameras);

    }
}
