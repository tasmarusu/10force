/* *************************************************
* InGame�̎n�܂肩��I���܂ł��Ď�����
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
        public class EnemyConfig
        {
            // �o�����Ă���̑ҋ@����
            [field: SerializeField] public float WaitTime { get; private set; } = 1.0f;

            [field:SerializeField] public NoneConfig None { get; private set; }
            [field:SerializeField] public ChaseConfig Chase { get; private set; }

            public enum Type
            {
                None = 1,       // �������Ȃ��G
                Chase = 5,      // �ǐՂ���G
                OneShot = 6,    // �O���ɂP�����ł�������G
                ThreeShot = 7,  // �O���ɂR�����ł�������G
            }


            /// <summary>
            /// HP���擾
            /// </summary>
            /// <param name="type"> �G�̃f�[�^ </param>
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
            /// �S�G�ɑΉ�����σf�[�^
            /// </summary>
            [System.Serializable]
            public class BaseData
            {
                // ���̃^�C�v��ID
                [field: SerializeField] public Type ID { get; private set; } = Type.Chase;
                // �G��Prefab�f�[�^
                [field: SerializeField] public EnemyController Enemy { get; private set; } = null;
                // �G��HP
                [field: SerializeField] public int HP { get; private set; } = 0;
            }

            /// <summary>
            /// �������Ȃ��G�L����
            /// </summary>
            [System.Serializable]
            public class NoneConfig : BaseData
            {

            }


            /// <summary>
            /// �ǐՂ���G�L����
            /// </summary>
            [System.Serializable]
            public class ChaseConfig : BaseData
            {
                // �G�̒ǐՃX�s�[�h
                [field: SerializeField] public float Speed { get; private set; } = 0.1f;
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
                public float Interval = 0.0f;
                public float Damage = 0.0f;
                public PlayerShotType ID = PlayerShotType.Penetrating;           // ID PlayerShotType���Q�Ƃ���ׂ�ID
                public AnimationCurve SpeedCurve = null;
                public float SpeedParam = 0.0f;
            }

            // �ђʒe
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