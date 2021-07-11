/* *************************************************
* EnemyController 敵のコントローラー
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;
    using UniRx.Triggers;
    using System;

    public class EnemyController : MonoBehaviour
    {
        // 敵自身が持ってるタイプ
        [field: SerializeField] public GameConfig.EnemyConfig.Type TypeID { get; private set; } = GameConfig.EnemyConfig.Type.None;

        private EnemyModel.DataConfig model = null;


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="model"> このモデルのデータ </param>
        public void Init(EnemyModel.DataConfig model)
        {
            this.model = model;

            this.OnTriggerEnter2DAsObservable().Subscribe(col =>
            {
                this.OnHit(col);
            });

            this.gameObject.SetActive(false);
        }

        
        /// <summary>
        /// 動き始める時に呼ばれる
        /// </summary>
        public void OnStart()
        {
            this.gameObject.SetActive(true);
        }


        public void OnUpdate()
        {
            this.model.Pos = this.transform.position;
        }



        private void OnHit(Collider2D col)
        {
            TagName tag = (TagName)Enum.Parse(typeof(TagName), $"{col.tag}");

            switch (tag)
            {
                case TagName.PlayerBullet:
                    // TODO ここのダメージ計算 GetComponent で取るの重いかも GameConfig みたいなやつから取る方法考えた方が良い？
                    float damage = col.gameObject.GetComponent<BulletController>().Damage;
                    this.model.Damage(10);

                    // HPが無くなったら死ぬ
                    this.Des();

                    break;
            }
        }



        /// <summary>
        /// HPが無くなった
        /// </summary>
        private void Des()
        {
            if (this.model.State == EnemyModel.StateConfig.Des)
            {
                gameObject.SetActive(false);
            }
        }
    }
}