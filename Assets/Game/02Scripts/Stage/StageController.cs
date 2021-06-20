/* *************************************************
* StageController ステージの形などを保持
*                 この親に StageGroup が付いている
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum ColliderType
    {
        /// <summary>
        /// 円型 の当たり判定
        /// </summary>
        Circle = 0,
        /// <summary>
        /// 箱型 の当たり判定
        /// </summary>
        Box = 1,
    }

    public class StageController : MonoBehaviour
    {
        [field: SerializeField] public ColliderType UseType { get; private set; } = ColliderType.Circle;
        public CircleStruct Circle { get; private set; }
        public BoxStruct Box { get; private set; }


        /***************************************************
        * Collider によって決まった構造体の中身を決める
        ************************************************** */
        // 円型
        public struct CircleStruct
        {
            public CircleStruct(Vector2 pos, float radius, CircleCollider2D collider)
            {
                this.Pos = pos;
                this.Radius = radius;
                this.Collider = collider;
            }
            public Vector2 Pos { get; }    // 中心座標
            public float Radius { get; }   // 半径
            public CircleCollider2D Collider { get; }  // 当たり判定
        }
        // 箱型
        public struct BoxStruct
        {
            public BoxStruct(Vector2 pos, float width, float height, BoxCollider2D collider)
            {
                this.Pos = pos;
                this.Width = width;
                this.Height = height;
                this.Collider = collider;
            }
            public Vector2 Pos { get; }    // 中心座標
            public float Width { get; }    // 横の長さ
            public float Height { get; }   // 縦の長さ
            public BoxCollider2D Collider { get; } // 当たり判定
        }

        /***************************************************
        * 初期化
        ************************************************** */
        public void Init()
        {
            Vector2 pos = this.transform.position;
            switch (this.UseType)
            {
                case ColliderType.Circle:
                    CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
                    float radius = circleCollider.bounds.size.x * 0.5f;
                    this.Circle = new CircleStruct(pos, radius, circleCollider);

                    break;

                case ColliderType.Box:
                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    float width = boxCollider.bounds.size.x;
                    float height = boxCollider.bounds.size.y;
                    this.Box = new BoxStruct(pos, width, height, boxCollider);

                    break;
            }
        }

        /***************************************************
        * このステージでプレイヤーが外に出てるかどうか
        ************************************************** */
        public bool IsOutPlayerPos(Vector2 playerPos)
        {
            Vector2 pos = this.transform.position;

            // 範囲外にいるかどうかの判定
            switch (this.UseType)
            {
                case ColliderType.Circle:
                    // プレイヤーと球の距離と球の半径から範囲外か決める
                    float dis = Vector2.Distance(playerPos, pos);
                    Debug.Log($"dis {dis} radius { this.Circle.Radius}");
                    if (dis > this.Circle.Radius)
                    {
                        // 範囲外
                        return true;
                    }

                    break;

                case ColliderType.Box:
                    // 横縦のプレイヤーとの距離を取る
                    float height = Mathf.Abs(pos.y - playerPos.y);  // 縦
                    float width = Mathf.Abs(pos.x - playerPos.x);   // 横

                    // 範囲外なら最短距離の位置へ両方戻す
                    // 縦
                    if (height > this.Box.Height)
                    {
                        // 範囲外
                        // float value = height - this.box.Height;
                        return true;
                    }
                    // 横
                    if (width > this.Box.Width)
                    {
                        // 範囲外
                        // float value = width - this.box.Width;
                        return true;
                    }

                    break;
            }

            // 範囲外に出ていないので false を返す
            return false;
        }


        /***************************************************
        * このステージの中心とプレイヤーとの距離
        ************************************************** */
        public float GetStageToPlayerDistance(Vector2 playerPos)
        {
            return Vector2.Distance(this.transform.position, playerPos);
        }


        /***************************************************
        * このステージの中心とプレイヤーとのベクトル
        ************************************************** */
        public Vector2 GetStageToPlayerVec(Vector2 playerPos)
        {
            return ((Vector2)this.transform.position - playerPos).normalized;
        }
    }
}