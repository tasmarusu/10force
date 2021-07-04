/* *************************************************
* BulletPoolProvider 弾の Pool ロード
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;

    public class BulletPoolProvider
    {
        [SerializeField] private PenetratingBulletController penetratingPrefab = null;

        private PenetratingBulletPool penetratingPool = null;


        /* *************************************************
        * 貫通弾のロード
        ************************************************* */
        public PenetratingBulletPool GetPenetrating()
        {
            // 既に準備済みならそちらを返す
            if (this.penetratingPool != null)
            {
                return this.penetratingPool;
            }

            // Pool を作成
            this.penetratingPool = new PenetratingBulletPool(this.penetratingPrefab);

            // 事前に Pool のサイズを 10 確保しておく
            int count = 10;
            this.penetratingPool.PreloadAsync(count, count).Subscribe();

            return this.penetratingPool;
        }


        private void OnDestroy()
        {
            // Pool 内全て削除
            this.penetratingPool.Dispose();
        }
    }
}