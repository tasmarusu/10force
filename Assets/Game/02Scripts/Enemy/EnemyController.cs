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
        // 敵の ID
        public int ID { get; private set; } = -1;

        public void Init()
        {
            this.OnTriggerEnter2DAsObservable().Subscribe(col =>
            {
                this.OnHit(col);
            });
        }


        public void OnUpdate()
        {
            
        }



        private void OnHit(Collider2D col)
        {
            TagName tag = (TagName)Enum.Parse(typeof(TagName), $"{col.tag}");

            switch (tag)
            {
                case TagName.PlayerBullet:
                    // TODO ここのダメージ計算 GetComponent で取るの重いかも GameConfig みたいなやつから取る方法考えた方が良い？
                    float damage = col.gameObject.GetComponent<BulletController>().Damage;
                    break;
            }
        }
    }
}