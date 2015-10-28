using System;
using DropFight.Games.Players;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Games.Attacks
{
    /// <summary>
    /// 攻撃
    /// </summary>
    public class Attack
    {
        /// <summary>
        /// 対応するブロックの座標
        /// </summary>
        public Vector3 BlockPosition
        {
            get;
            private set;
        }

        /// <summary>
        /// 速度
        /// </summary>
        public Vector3 Speed
        {
            get;
            private set;
        }

        /// <summary>
        /// モデルの情報
        /// </summary>
        public ModelInfo ModelInfo
        {
            get;
            private set;
        }

        /// <summary>
        /// 攻撃の情報
        /// </summary>
        public AttackInfo Info
        {
            get
            {
                return new AttackInfo(this);
            }
        }

		public PlayerInfo OwnerInfo
		{
			get
			{
				return owner.Info;
			}
		}
        /// <summary>
        /// 生成したプレイヤー
        /// </summary>
        private Player owner;

        /// <summary>
        /// モデル
        /// </summary>
        private Model model;

        /// <param name="owner">生成したプレイヤー</param>
		public Attack(Player owner)
		{
            this.owner = owner;

			model = owner.AttackModel;
			Speed = owner.AttackSpeed;

			ModelInfo = new ModelInfo();
			ModelInfo.Position = owner.AttackPosition;

            Vector3 direction = owner.Direction;
			ModelInfo.Radian.Y = owner.RadianY;
		
			Vector3 blockPosition = ModelInfo.Position;
			blockPosition.Y--;
			this.BlockPosition = blockPosition;
		}

        /// <summary>
        /// 更新する
        /// </summary>
        public void Update()
        {
            ModelInfo.Position = ModelInfo.Position + Speed;
            UpdateBlockPosition();
        }

        /// <summary>
        /// 対応するブロックの座標を更新する
        /// </summary>
        private void UpdateBlockPosition()
        {
            Vector3 blockPosition = BlockPosition;

            if (Speed.X > 0)
            {
                blockPosition.X = (float)Math.Floor(ModelInfo.Position.X);
            }
            if (Speed.X < 0)
            {
                blockPosition.X = (float)Math.Ceiling(ModelInfo.Position.X);
            }
            if (Speed.Z > 0)
            {
                blockPosition.Z = (float)Math.Floor(ModelInfo.Position.Z);
            }
            if (Speed.Z < 0)
            {
                blockPosition.Z = (float)Math.Ceiling(ModelInfo.Position.Z);
            }

            this.BlockPosition = blockPosition;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="camera">モデル用のカメラ</param>
        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = ModelInfo.World;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }

                mesh.Draw();
            }
        }
    }
}
