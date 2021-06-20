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
        [SerializeField] private StageGroup[] Groups = null;

        public StageGroup useStage { get; private set; } = null;


        /***************************************************
        * 初期化
        ************************************************* */
        public void Init()
        {
            // 使うステージをランダム生成 TODO 後で決め打ちするように変更
            int num = Random.Range(0, this.Groups.Length);
            this.useStage = Instantiate(this.Groups[num], this.transform);

            // 初期化
            for (int i = 0; i < this.useStage.Controllers.Length; i++)
            {
                this.useStage.Init();
            }
        }


        /// <summary>
        /// プレイヤーの座標を内側に戻す
        /// </summary>
        /// <returns> プレイヤーの補正された座標 </returns>
        public Vector2 ReplaceOutPlayerPos(Vector2 playerPos)
        {
            StageController[] controllers = this.useStage.Controllers;
            int stageCount = controllers.Length;
            bool[] isOutStagePos = Enumerable.Repeat<bool>(false, stageCount).ToArray();

            // 全てのステージが入ってないかどうか調べる
            for (int i = 0; i < controllers.Length; i++)
            {
                isOutStagePos[i] = controllers[i].IsOutPlayerPos(playerPos);

                // false なら一個は入ってる
                if (isOutStagePos[i] == false)
                {
                    return playerPos;
                }
            }

            // 入ってないので近い方はどのステージかを調べる
            int nearNum = 0;
            float nearDistance = controllers[0].GetStageToPlayerDistance(playerPos);
            for (int i = 1; i < controllers.Length; i++)
            {
                float dis = controllers[i].GetStageToPlayerDistance(playerPos);
                if (dis < nearDistance)
                {
                    nearDistance = dis;
                    nearNum = i;
                }
            }

            // 調べた結果近いステージを入れる
            StageController nearStage = controllers[nearNum];

            // 外に出ないように戻す
            Vector2 replacePlayerPos = Vector2.zero;
            switch (nearStage.UseType)
            {
                // 円形
                // 1.プレイヤーから球体のベクトルを取る
                // 2.プレイヤーと球体中心の距離 - 球体の半径 = プレイヤーが戻る量
                case ColliderType.Circle:
                    Vector2 vec = nearStage.GetStageToPlayerVec(playerPos);
                    float dis = nearStage.GetStageToPlayerDistance(playerPos);
                    float radius = nearStage.Circle.Radius;
                    replacePlayerPos = playerPos + ((dis - radius) * vec);

                    break;

                // 四角形
                // 1.上に出てたら下に プレイヤーの縦座標 - 四角形の高さ = プレイヤーが下に戻る量
                // 2 下に出てたら上に プレイヤーの縦座標 - 四角形の高さ = プレイヤーが上に戻る量
                // 3 右に出ていたら左に プレイヤーの横座標 - 四角形の長さ = プレイヤーが左に戻る量
                // 4 左に出ていたら右に プレイヤーの横座標 - 四角形の長さ = プレイヤーが左に戻る量
                case ColliderType.Box:
                    Vector2 stagePos = nearStage.Box.Pos;
                    float min = stagePos.x - nearStage.Box.Width * 0.5f;
                    float max = stagePos.x + nearStage.Box.Width * 0.5f;
                    replacePlayerPos.x = Mathf.Clamp(playerPos.x, min, max);
                    min = stagePos.y - nearStage.Box.Height * 0.5f;
                    max = stagePos.y + nearStage.Box.Height * 0.5f;
                    replacePlayerPos.y = Mathf.Clamp(playerPos.y, min, max);

                    break;
            }

            return replacePlayerPos;
        }
    }
}