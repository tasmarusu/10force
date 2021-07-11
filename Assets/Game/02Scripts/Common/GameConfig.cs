/* *************************************************
* InGameの始まりから終わりまでを監視する
************************************************* */
namespace MainForce
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;


    public enum TagName
    {
        Untagged = 0,
        Respawn = 1,
        Finish = 2,
        EditorOnly = 3,
        MainCamera = 4,
        Player = 5,
        GameController = 6,
        Ground = 7,
        PlayerBullet = 8,
        Enemy = 9,
        EnemyBullet = 10,
    }



    [CreateAssetMenu()]
    public class GameConfig : ScriptableObject
    {
        [field : SerializeField] public PlayerConfig Player { get; private set; } = null;
        [field: SerializeField] public EnemyConfig Enemy { get; private set; } = null;
        [field: SerializeField] public ShotConfig Shot { get; private set; } = null;


        public enum PlayerShotType
        {
            None = 0, // 何も無し　初期化用？
            Penetrating = 1,  // 貫通弾
        }


        [System.Serializable]
        public class PlayerConfig
        {
            [Tooltip("プレイヤーの移動速度")]
            public float speed = 0.5f;
            [Tooltip("プレイヤーのタイプによって画像など分ける")]
            public List<Data> types = null;

            [System.Serializable]
            public class Data
            {
                public PlayerShotType ID = 0;           // ID PlayerShotTypeを参照する為のID
                public float speed = 0.5f;              // 移動速度
                public Sprite sprite = null;            // プレイヤーの画像
                public GameObject shotPrefab = null;    // 玉の種類
            }


            /// <summary>
            /// ID から Type を取得
            /// </summary>
            /// <param name="ID"> 使う Type の ID </param>
            /// <returns></returns>
            public Data GetType(PlayerShotType ID)
            {
                return this.types.Find(x => x.ID == ID);    // types.Find は List で使用可能　配列は無理
            }
        }


        [System.Serializable]
        public class EnemyConfig
        {
            // 出現してからの待機時間
            [field: SerializeField] public float WaitTime { get; private set; } = 1.0f;

            [field:SerializeField] public NoneConfig None { get; private set; }
            [field:SerializeField] public ChaseConfig Chase { get; private set; }

            public enum Type
            {
                None = 1,       // 何もしない敵
                Chase = 5,      // 追跡する敵
                OneShot = 6,    // 前方に１発ずつ打ち続ける敵
                ThreeShot = 7,  // 前方に３発ずつ打ち続ける敵
            }


            /// <summary>
            /// HPを取得
            /// </summary>
            /// <param name="type"> 敵のデータ </param>
            /// <returns></returns>
            public int GetStartHP(Type type)
            {
                int hp = 0;

                switch (type)
                {
                    case Type.None:
                        hp = this.None.HP;
                        break;

                    case Type.Chase:
                        hp = this.Chase.HP;
                        break;

                    case Type.OneShot:
                        hp = this.None.HP;
                        break;

                    case Type.ThreeShot:

                        break;
                }
                return hp;
            }


            /// <summary>
            /// 全敵に対応する可変データ
            /// </summary>
            [System.Serializable]
            public class BaseData
            {
                // このタイプのID
                [field: SerializeField] public Type ID { get; private set; } = Type.Chase;
                // 敵のPrefabデータ
                [field: SerializeField] public EnemyController Enemy { get; private set; } = null;
                // 敵のHP
                [field: SerializeField] public int HP { get; private set; } = 0;
            }

            /// <summary>
            /// 何もしない敵キャラ
            /// </summary>
            [System.Serializable]
            public class NoneConfig : BaseData
            {

            }


            /// <summary>
            /// 追跡する敵キャラ
            /// </summary>
            [System.Serializable]
            public class ChaseConfig : BaseData
            {
                // 敵の追跡スピード
                [field: SerializeField] public float Speed { get; private set; } = 0.1f;
            }
        }



        [System.Serializable]
        public class ShotConfig
        {
            [field: SerializeField] public PenetratingData Penetrating { get; private set; } = null;

            // 玉の共通項目
            [System.Serializable]
            public class ShotData
            {
                public float Interval = 0.0f;
                public float Damage = 0.0f;
                public PlayerShotType ID = PlayerShotType.Penetrating;           // ID PlayerShotTypeを参照する為のID
                public AnimationCurve SpeedCurve = null;
                public float SpeedParam = 0.0f;
            }

            // 貫通弾
            [System.Serializable]
            public class PenetratingData : ShotData
            {

            }
        }




        private static GameConfig instance;
        public static GameConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    string path = "GameConfig";
                    instance = Resources.Load<GameConfig>(path);
                }
                return instance;
            }
        }
    }
}