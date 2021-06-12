/* *************************************************
* InGameの始まりから終わりまでを監視する
************************************************* */
namespace MainForce
{
    using UnityEngine;

    [CreateAssetMenu()]
    public class GameConfig : ScriptableObject
    {
        [field : SerializeField] public PlayerConfig Player { get; private set; } = null;



        [System.Serializable]
        public class PlayerConfig
        {
            [Tooltip("プレイヤーの移動速度")] public float speed = 0.5f;
        }




        private static GameConfig instance;
        public static GameConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    string path = "";
                    instance = Resources.Load<GameConfig>(path);
                }
                return instance;
            }
        }
    }
}