/* *************************************************
* PenetratingBulletGroup �ђʒe�̃v�[��
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UniRx.Toolkit;
    using UnityEngine;

    public class PenetratingBulletPool : ObjectPool<PenetratingBulletController>
    {
        private List<PenetratingBulletController> controllers = new List<PenetratingBulletController>();

        private readonly PenetratingBulletController penetratingPrefab = null;
        private readonly Transform rootPenetrating = null;

        public PenetratingBulletPool(PenetratingBulletController penetratingPrefab)
        {
            // readonly ���Ă���s����񂩁c
            this.penetratingPrefab = penetratingPrefab;

            // �e�ɂȂ�I�u�W�F�N�g
            this.rootPenetrating = new GameObject().transform;
            this.rootPenetrating.name = $"{this} Parent";
            this.rootPenetrating.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        // readonly ����͖���
        // private void Set() => this.penetratingPrefab = null;


        protected override PenetratingBulletController CreateInstance()
        {
            var newBullet = GameObject.Instantiate(this.penetratingPrefab);
            newBullet.transform.SetParent(this.rootPenetrating);

            return newBullet;
        }
    }
}