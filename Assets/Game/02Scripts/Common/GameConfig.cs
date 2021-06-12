/* *************************************************
* InGame�̎n�܂肩��I���܂ł��Ď�����
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
            [Tooltip("�v���C���[�̈ړ����x")] public float speed = 0.5f;
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