/* *************************************************
* PenetratingBulletGroup 貫通弾のプール
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
            // readonly ってこれ行けるんか…
            this.penetratingPrefab = penetratingPrefab;

            // 親になるオブジェクト
            this.rootPenetrating = new GameObject().transform;
            this.rootPenetrating.name = $"{this} Parent";
            this.rootPenetrating.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        // readonly これは無理
        // private void Set() => this.penetratingPrefab = null;


        protected override PenetratingBulletController CreateInstance()
        {
            var newBullet = GameObject.Instantiate(this.penetratingPrefab);
            newBullet.transform.SetParent(this.rootPenetrating);

            return newBullet;
        }
    }
}