using System;
using System.Diagnostics;
using System.Collections.Generic;
using DropFight.Games.Attacks;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DropFight.Games.Players;

namespace DropFight.Games.Blocks
{
    /// <summary>
    /// ブロック
    /// </summary>
    public class Block : Fallable
    {
        /// <summary>
        /// 最小落下待ち時間
        /// </summary>
        private readonly TimeSpan minBeforeFallTime = new TimeSpan(0, 0, 1);

        /// <summary>
        /// 最小落下時間
        /// </summary>
        private readonly TimeSpan minFallTime = new TimeSpan(0, 0, 3);

        /// <summary>
        /// モデル
        /// </summary>
        private Model model;

		/// <summary>
		/// 最後にブロックを攻撃したプレイヤー
		/// </summary>
		private PlayerInfo lastAttackPlayerInfo;

        /// <summary>
        /// ブロックの情報
        /// </summary>
        public BlockInfo Info
        {
            get
            {
                return new BlockInfo(this);
            }
        }

		public PlayerInfo PlayerInfo
		{
			get;
			private set;
		}

        /// <summary>
        /// 落下待ち時間を測定するタイマー
        /// </summary>
        private Stopwatch beforeFallTimer = new Stopwatch();

		private List<Vector3> defaultDiffuseColor = new List<Vector3>();

		public bool IsDead
		{
			get;
			private set;
		}
        /// <param name="x">ブロックのx座標</param>
        /// <param name="z">ブロックのz座標</param>
		/// <param name="model">モデル</param>
        public Block(int x, int z, Model model)
        {
            BasePositionY = -1;
            ModelInfo.Position = new Vector3(x, BasePositionY, z);
            UpdatePosition();
			this.model = model;

			foreach (ModelMesh mesh in model.Meshes)
			{
				foreach (BasicEffect effect in mesh.Effects)
				{
					defaultDiffuseColor.Add(effect.DiffuseColor);
				}
			}

			IsDead = false;
        }

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        public void Update(GameInfo gameInfo)
        {
            UpdatePosition();
            FallIfNecessary(gameInfo);
            ResetStatusOfFallingIfNecessary();
			UpdatePlayerInfo(gameInfo);
        }

        /// <summary>
        /// 必要に応じて落下させる
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        private void FallIfNecessary(GameInfo gameInfo)
        {
            if (IsFalling)
            {
                return;
            }

            if (!beforeFallTimer.IsRunning || beforeFallTimer.Elapsed < minBeforeFallTime)
            {
                foreach (AttackInfo attackInfo in gameInfo.AttackInfos)
                {
                    if (ModelInfo.Position == attackInfo.BlockPosition)
                    {
						lastAttackPlayerInfo = attackInfo.OwnerInfo;
                        beforeFallTimer.Start();
                        break;
                    }
                }
                return;
            }

            beforeFallTimer.Reset();
            Fall();
        }

		public void KillBlock()
		{
			if(!beforeFallTimer.IsRunning) beforeFallTimer.Start();
			IsDead = true;
		}
        /// <summary>
        /// 必要に応じて落下の状態を初期化する
        /// </summary>
        private void ResetStatusOfFallingIfNecessary()
        {
			if (IsDead)
			{
				return;
			}
            if (!IsFalling)
            {
                return;
            }
            if (FallTime < minFallTime)
            {
                return;
            }

            ResetStatusOfFalling();
        }

		private void UpdatePlayerInfo(GameInfo gameInfo)
		{
			foreach (PlayerInfo playerInfo in gameInfo.PlayerInfos)
			{
				if (new Vector2(ModelInfo.Position.X, ModelInfo.Position.Z) == new Vector2(playerInfo.BlockPositionX, playerInfo.BlockPositionZ))
				{
					this.PlayerInfo = playerInfo;
					return;
				}
			}
			this.PlayerInfo = null;
		}

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="camera">モデル用のカメラ</param>
        public void Draw(Camera camera)
        {
			int i = 0;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
					
                    //effect.EnableDefaultLighting();
					//effect.DirectionalLight0.Direction = ModelInfo.Position;
					effect.DirectionalLight1.Direction = ModelInfo.Position + new Vector3(0.5f, 1.0f, 0.5f);
					//effect.DirectionalLight2.Direction = ModelInfo.Position;
                    effect.World = ModelInfo.World;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;

					if (beforeFallTimer.IsRunning || IsFalling)
					{
						// 時間経過でブロックを落とす場合
						if (IsDead) effect.DiffuseColor = new Vector3(0.0f, 0.0f, 0.0f);
						// 1Pが攻撃した場合:赤
						else if (lastAttackPlayerInfo.Index == PlayerIndex.One) effect.DiffuseColor = new Vector3(1.0f, 0.0f, 0.0f);
						// 1Pが攻撃した場合:オレンジ
						else if (lastAttackPlayerInfo.Index == PlayerIndex.Two) effect.DiffuseColor = new Vector3(1.0f, 0.65f, 0.0f);
						// 1Pが攻撃した場合:緑
						else if (lastAttackPlayerInfo.Index == PlayerIndex.Three) effect.DiffuseColor = new Vector3(0.0f, 1.0f, 0.0f);
						// 1Pが攻撃した場合:青
						else if (lastAttackPlayerInfo.Index == PlayerIndex.Four) effect.DiffuseColor = new Vector3(0.0f, 0.0f, 1.0f);
					}
					else
					{
						effect.DiffuseColor = defaultDiffuseColor[i];
						i++;
					}
					
                }
                mesh.Draw();
            }
        }
    }
}
