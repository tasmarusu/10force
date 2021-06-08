/* *************************************************
* PlayerModel プレイヤーの情報を保持
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;

    public class PlayerModel : MonoBehaviour
    {
        // [ゴーストで使う] 毎フレーム入れる プレイヤーが１フレームで移動した量
        public List<Vector2Int> MoveValue { get; private set; } = new List<Vector2Int>();
        // [ゴーストで使う] 毎フレーム入れる プレイヤーの角度
        public List<float> AngleValue { get; private set; } = new List<float>();
        // [ゴーストで使う] 特定フレームのみ プレイヤーが打ったフレーム数
        public List<int> ShotFrame { get; private set; } = new List<int>();
        // プレイヤーの今の座標
        public Vector2 Pos { get; private set; } = Vector2.zero;


        /***************************************************
        * 初期化
        ************************************************** */
        public void Init()
        {
            this.MoveValue = new List<Vector2Int>();
            this.AngleValue = new List<float>();
            this.ShotFrame = new List<int>();
            this.Pos = Vector2.zero;
        }
        public PlayerModel()
        {
            this.Init();
        }

        /***************************************************
        * 初期化
        ************************************************** */
        // 毎フレーム入れる
        public void SetCharaValue(Vector2Int move, float angle, Vector2 pos)
        {
            this.MoveValue.Add(move);
            this.AngleValue.Add(angle);
            this.Pos = pos;
        }
        // 特定のフレームのみ入れる
        public void SetShotValue(int shotFrame)
        {
            this.ShotFrame.Add(shotFrame);
        }
    }
}