/* *************************************************
* StageGroup 1つのステージの形全てを管理
*            この子供に StageController が付いている
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UniRx.Toolkit;
    using UnityEngine;

    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private PenetratingBulletPool penetratingGroup = null;



        ///***************************************************
        //* 初期化
        //************************************************** */
        //public GameObject GetBulletObjInPool( type)
        //{
        //    GameObject getShot = null;

        //    switch (type)
        //    {
        //        case GameConfig.PlayerShotType.None:
        //            Debug.Log($"設定されていません");
        //            break;

        //        case GameConfig.PlayerShotType.Penetrating:


        //            break;
        //    }


        //    return getShot;
        //}



        ///***************************************************
        //* 初期化
        //************************************************** */
        //private GameObject CreateBullet(GameConfig.PlayerShotType type)
        //{
        //    GameObject bullet = null;

        //    switch (type)
        //    {
        //        case GameConfig.PlayerShotType.None:
        //            Debug.Log($"設定されていません");
        //            break;

        //        case GameConfig.PlayerShotType.Penetrating:

        //            break;
        //    }

        //    return bullet;
        //}
    }
}