/* *************************************************
* StageGroup 1�̃X�e�[�W�̌`�S�Ă��Ǘ�
*            ���̎q���� StageController ���t���Ă��� 
*            Unirx�̂�͕����ɑΉ����ĂȂ������������̂ŕs�̗p
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UniRx.Toolkit;
    using UnityEngine;

    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private PenetratingBulletGroup penetratingGroup = null;


        ///***************************************************
        //* ������
        //************************************************** */
        //public GameObject GetBulletObjInPool(GameConfig.PlayerShotType type)
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