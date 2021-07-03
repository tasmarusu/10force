/* *************************************************
* InGame�̎n�܂肩��I���܂ł��Ď�����
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
            None = 0, // ���������@�������p�H
            Penetrating = 1,  // �ђʒe
        }


        [System.Serializable]
        public class PlayerConfig
        {
            [Tooltip("�v���C���[�̈ړ����x")]
            public float speed = 0.5f;
            [Tooltip("�v���C���[�̃^�C�v�ɂ���ĉ摜�ȂǕ�����")]
            public List<Data> types = null;

            [System.Serializable]
            public class Data
            {
                public PlayerShotType ID = 0;           // ID PlayerShotType���Q�Ƃ���ׂ�ID
                public float speed = 0.5f;              // �ړ����x
                public Sprite sprite = null;            // �v���C���[�̉摜
                public GameObject shotPrefab = null;    // �ʂ̎��
            }


            /// <summary>
            /// ID ���� Type ���擾
            /// </summary>
            /// <param name="ID"> �g�� Type �� ID </param>
            /// <returns></returns>
            public Data GetType(PlayerShotType ID)
            {
                return this.types.Find(x => x.ID == ID);    // types.Find �� List �Ŏg�p�\�@�z��͖���
            }
        }


        [System.Serializable]
        public class ShotConfig
        {
            [field: SerializeField] public PenetratingData Penetrating { get; private set; } = null;

            // �ʂ̋��ʍ���
            [System.Serializable]
            public class ShotData
            {
                public float interval = 0.0f;
            }

            // �ђʒe
            [System.Serializable]
            public class PenetratingData : ShotData
            {
                public PlayerShotType ID = PlayerShotType.Penetrating;           // ID PlayerShotType���Q�Ƃ���ׂ�ID
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