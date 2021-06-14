/* *************************************************
* StageController ステージの形などを保持
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StageController : MonoBehaviour
    {
        [SerializeField] private ColliderType useType = ColliderType.Circle;
        private Circle circle;
        private Box box;

        private enum ColliderType
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


        /***************************************************
        * Collider によって決まった構造体の中身を決める
        ************************************************** */
        // 円型
        struct Circle
        {
            public Circle(Vector2 pos, float radius, CircleCollider2D collider)
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
        struct Box
        {
            public Box(Vector2 pos, float width, float height, BoxCollider2D collider)
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
        * メインゲームのUIをロードする
        ************************************************** */
        public void Init()
        {
            Vector2 pos = this.transform.position;
            switch (this.useType)
            {
                case ColliderType.Circle:
                    CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
                    float radius = circleCollider.radius;
                    this.circle = new Circle(pos, radius, circleCollider);

                    break;

                case ColliderType.Box:
                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    float width = boxCollider.bounds.size.x;
                    float height = boxCollider.bounds.size.y;
                    this.box = new Box(pos, width, height, boxCollider);

                    break;
            }
        }

        /***************************************************
        * メインゲームのUIをロードする
        ************************************************** */
        public void OutgoingPosCoordinates(Vector2 playerPos)
        {
            Vector2 pos = this.transform.position;

            // 範囲外にいるかどうかの判定
            switch (this.useType)
            {
                case ColliderType.Circle:
                    // プレイヤーと球の距離と球の半径から範囲外か決める
                    float dis = Vector2.Distance(playerPos, pos);
                    if (dis > this.circle.Radius)
                    {
                        // 範囲外
                    }

                    break;

                case ColliderType.Box:
                    // 横縦のプレイヤーとの距離を取る
                    float height = Mathf.Abs(pos.y - playerPos.y);  // 縦
                    float width = Mathf.Abs(pos.x - playerPos.x);   // 横

                    // 範囲外なら最短距離の位置へ両方戻す
                    // 縦
                    if (height > this.box.Height)
                    {
                        // 範囲外
                        // float value = height - this.box.Height;
                    }
                    // 横
                    if (width > this.box.Width)
                    {
                        // 範囲外
                        // float value = width - this.box.Width;
                    }

                    break;
            }

            // 全てのステージが入ってない時は座標を戻す
            // これ StageManager で取るべき

            // 1.近い方の当たり判定の座標を確認

            // 円形
            // 1.プレイヤーから球体のベクトルを取る
            // 2.プレイヤーと球体の距離 - 球体の半径 = プレイヤーが戻る量

            // 四角形
            // 1.上に出てたら下に プレイヤーの縦座標 - 四角形の高さ = プレイヤーが下に戻る量
            // 2 下に出てたら上に プレイヤーの縦座標 - 四角形の高さ = プレイヤーが上に戻る量
            // 3 右に出ていたら左に プレイヤーの横座標 - 四角形の長さ = プレイヤーが左に戻る量
            // 4 左に出ていたら右に プレイヤーの横座標 - 四角形の長さ = プレイヤーが左に戻る量
        }
    }
}