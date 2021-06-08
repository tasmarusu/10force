/* *************************************************
* PlayerModel �v���C���[�̏���ێ�
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UniRx;

    public class PlayerModel : MonoBehaviour
    {
        // [�S�[�X�g�Ŏg��] ���t���[������� �v���C���[���P�t���[���ňړ�������
        public List<Vector2Int> MoveValue { get; private set; } = new List<Vector2Int>();
        // [�S�[�X�g�Ŏg��] ���t���[������� �v���C���[�̊p�x
        public List<float> AngleValue { get; private set; } = new List<float>();
        // [�S�[�X�g�Ŏg��] ����t���[���̂� �v���C���[���ł����t���[����
        public List<int> ShotFrame { get; private set; } = new List<int>();
        // �v���C���[�̍��̍��W
        public Vector2 Pos { get; private set; } = Vector2.zero;


        /***************************************************
        * ������
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
        * ������
        ************************************************** */
        // ���t���[�������
        public void SetCharaValue(Vector2Int move, float angle, Vector2 pos)
        {
            this.MoveValue.Add(move);
            this.AngleValue.Add(angle);
            this.Pos = pos;
        }
        // ����̃t���[���̂ݓ����
        public void SetShotValue(int shotFrame)
        {
            this.ShotFrame.Add(shotFrame);
        }
    }
}