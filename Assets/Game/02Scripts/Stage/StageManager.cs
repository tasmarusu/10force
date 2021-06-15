/* *************************************************
* StageManager ステージを監視するスクリプト
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class StageManager : MonoBehaviour
    {
        public StageController[] StageController { get; private set; } = null;


        /***************************************************
        * 初期化
        ************************************************* */
        public void Init()
        {

        }


        /// <summary>
        /// プレイヤーの座標を戻す
        /// </summary>
        /// <returns> プレイヤーの戻る補正値 </returns>
        public Vector2 Coodinate(Vector2 playerPos)
        {
            int stageCount = this.StageController.Length;
            bool[] isOutStagePos = Enumerable.Repeat<bool>(false, stageCount).ToArray();

            // 全てのステージが入ってないかどうか調べる
            for (int i = 0; i < this.StageController.Length; i++)
            {
                isOutStagePos[i] = this.StageController[i].IsOutPlayerPos(playerPos);

                // false なら一個は入ってる
                if (isOutStagePos[i] == false)
                {
                    return Vector3.zero;
                }
            }

            // 入ってないので近い方はどのステージかを調べる
            int nearNum = 0;
            float nearDistance = this.StageController[0].GetStageToPlayerDistance(playerPos);
            for (int i = 1; i < this.StageController.Length; i++)
            {
                float dis = this.StageController[i].GetStageToPlayerDistance(playerPos);
                if (dis < nearDistance)
                {
                    nearDistance = dis;
                    nearNum = i;
                }
            }

            // 調べた結果のステージを入れる
            StageController nearStage = this.StageController[nearNum];

            // 外に出ないように戻す
            Vector2 backValue = Vector2.zero;
            switch (nearStage.UseType)
            {
                // 円形
                // 1.プレイヤーから球体のベクトルを取る
                // 2.プレイヤーと球体中心の距離 - 球体の半径 = プレイヤーが戻る量
                case ColliderType.Circle:
                    Vector2 vec = nearStage.GetStageToPlayerVec(playerPos);
                    float dis = nearStage.GetStageToPlayerDistance(playerPos);
                    float radius = nearStage.Circle.Radius;
                    backValue = (dis - radius) * vec;

                    break;

                // 四角形
                // 1.上に出てたら下に プレイヤーの縦座標 - 四角形の高さ = プレイヤーが下に戻る量
                // 2 下に出てたら上に プレイヤーの縦座標 - 四角形の高さ = プレイヤーが上に戻る量
                // 3 右に出ていたら左に プレイヤーの横座標 - 四角形の長さ = プレイヤーが左に戻る量
                // 4 左に出ていたら右に プレイヤーの横座標 - 四角形の長さ = プレイヤーが左に戻る量
                case ColliderType.Box:



                    break;
            }

            return backValue;
        }
    }
}