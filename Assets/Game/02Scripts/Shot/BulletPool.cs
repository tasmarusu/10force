/* *************************************************
* StageGroup 1�̃X�e�[�W�̌`�S�Ă��Ǘ�
*            ���̎q���� StageController ���t���Ă���
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
        //* ������
        //************************************************** */
        //public GameObject GetBulletObjInPool( type)
        //{
        //    GameObject getShot = null;

        //    switch (type)
        //    {
        //        case GameConfig.PlayerShotType.None:
        //            Debug.Log($"�ݒ肳��Ă��܂���");
        //            break;

        //        case GameConfig.PlayerShotType.Penetrating:


        //            break;
        //    }


        //    return getShot;
        //}



        ///***************************************************
        //* ������
        //************************************************** */
        //private GameObject CreateBullet(GameConfig.PlayerShotType type)
        //{
        //    GameObject bullet = null;

        //    switch (type)
        //    {
        //        case GameConfig.PlayerShotType.None:
        //            Debug.Log($"�ݒ肳��Ă��܂���");
        //            break;

        //        case GameConfig.PlayerShotType.Penetrating:

        //            break;
        //    }

        //    return bullet;
        //}
    }
}