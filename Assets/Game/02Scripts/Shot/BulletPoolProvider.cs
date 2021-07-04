/* *************************************************
* BulletPoolProvider �e�� Pool ���[�h
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
        * �ђʒe�̃��[�h
        ************************************************* */
        public PenetratingBulletPool GetPenetrating()
        {
            // ���ɏ����ς݂Ȃ炻�����Ԃ�
            if (this.penetratingPool != null)
            {
                return this.penetratingPool;
            }

            // Pool ���쐬
            this.penetratingPool = new PenetratingBulletPool(this.penetratingPrefab);

            // ���O�� Pool �̃T�C�Y�� 10 �m�ۂ��Ă���
            int count = 10;
            this.penetratingPool.PreloadAsync(count, count).Subscribe();

            return this.penetratingPool;
        }


        private void OnDestroy()
        {
            // Pool ���S�č폜
            this.penetratingPool.Dispose();
        }
    }
}