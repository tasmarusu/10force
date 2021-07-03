/* *************************************************
* InGameの始まりから終わりまでを監視する
************************************************* */
namespace MainForce
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;


    [CreateAssetMenu()]
    public class GameConfig : ScriptableObject
    {
        [field : SerializeField] public PlayerConfig Player { get; private set; } = null;
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
        public class ShotConfig
        {
            [field: SerializeField] public PenetratingData Penetrating { get; private set; } = null;

            // 玉の共通項目
            [System.Serializable]
            public class ShotData
            {
                public float interval = 0.0f;
            }

            // 貫通弾
            [System.Serializable]
            public class PenetratingData : ShotData
            {
                public PlayerShotType ID = PlayerShotType.Penetrating;           // ID PlayerShotTypeを参照する為のID
                public float speed = 0.0f;
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