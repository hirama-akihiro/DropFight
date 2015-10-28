using System;
using System.Diagnostics;
using DropFight.Games.Controllers;
using DropFight.Games.Attacks;
using DropFight.Games.Blocks;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Games.Players
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    public class Player : Fallable
    {
        /// <summary>
        /// 向きベクトルの列挙
        /// </summary>
        private static class Directions
        {

            /// <summary>
            /// 上
            /// </summary>
            public static readonly Vector3 Up = Vector3.Forward;

            /// <summary>
            /// 下
            /// </summary>
            public static readonly Vector3 Down = Vector3.Backward;

            /// <summary>
            /// 左
            /// </summary>
            public static readonly Vector3 Left = Vector3.Left;

            /// <summary>
            /// 右
            /// </summary>
            public static readonly Vector3 Right = Vector3.Right;
        }

        /// <summary>
        /// 復活してからの時間
        /// </summary>
        private Stopwatch resbornTimer = new Stopwatch();

        /// <summary>
        /// 移動の速度
        /// </summary>
        private const float moveSpeed = 2f / 30f;

        /// <summary>
        /// 攻撃の速度
        /// </summary>
        private const float attackSpeed = 0.3f;

        /// <summary>
        /// 最小攻撃待ち時間
        /// </summary>
        private readonly TimeSpan minPrepareForAttackTime = new TimeSpan(0, 0, 0, 0, 350);

        /// <summary>
        /// 最小攻撃後硬直時間
        /// </summary>
		private readonly TimeSpan minAttackTime = new TimeSpan(0, 0, 0, 0, 350);

        /// <summary>
        /// 最小落下時間
        /// </summary>
        private readonly TimeSpan minFallTime = new TimeSpan(0, 0, 2);

        /// <summary>
        /// 復活後の無敵時間
        /// </summary>
        private readonly TimeSpan invincibleTime = new TimeSpan(0, 0, 1);
        
        /// <summary>
        /// 対応するブロックのX座標
        /// </summary>
        public int BlockPositionX
        {
            get
            {
                return (int)Math.Round(ModelInfo.Position.X);
            }
        }

        /// <summary>
        /// 対応するブロックのZ座標
        /// </summary>
        public int BlockPositionZ
        {
            get
            {
                return (int)Math.Round(ModelInfo.Position.Z);
            }
        }

        /// <summary>
        /// 初期位置の座標
        /// </summary>
        private readonly Vector3 defaultPosition;

        /// <summary>
        /// 名前
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 行動
        /// </summary>
        public PlayerAction Action
        {
            get;
            private set;
        }

        /// <summary>
        /// 向きベクトル
        /// </summary>
        public Vector3 Direction
        {
            get;
            private set;
        }

        /// <summary>
        /// Y座標方向への角度
        /// </summary>
        public float RadianY
        {
            get
            {
                return (float)Math.Atan2(Direction.X, Direction.Z);
            }
        }

        /// <summary>
        /// プレイヤーの情報
        /// </summary>
        public PlayerInfo Info
        {
            get
            {
                return new PlayerInfo(this);
            }
        }

        /// <summary>
        /// 新たに生成した攻撃
        /// </summary>
        public Attack NewAttack
        {
            get;
            private set;
        }

        /// <summary>
        /// 残機
        /// </summary>
        public uint Stock
        {
            get;
            private set;
        }

        /// <summary>
        /// 死亡したかどうか
        /// </summary>
        public bool IsDead
        {
            get
            {
                return Stock <= 0;
            }
        }

        /// <summary>
        /// 死亡した時間,死亡してないならTimeSpan.Zero
        /// </summary>
        public TimeSpan DeadTime
        {
            get;
            private set;
        }
        /// <summary>
        /// 攻撃前の時間を計測するタイマー
        /// </summary>
        private Stopwatch prepareForAttackTimer = new Stopwatch();

        /// <summary>
        /// 攻撃中の時間を計測するタイマー
        /// </summary>
        private Stopwatch attackTimer = new Stopwatch();

        /// <summary>
        /// コントローラー
        /// </summary>
        private Controller controller;

        /// <summary>
        /// モデル
        /// </summary>
        private PlayerModel model;

        /// <summary>
        /// 生成する攻撃のモデル
        /// </summary>
        public Model AttackModel
        {
            get;
            private set;
        }

        /// <summary>
        /// 装飾の色
        /// </summary>
        public readonly Color Color;

        /// <param name="name">名前</param>
        /// <param name="x">初期位置のx座標</param>
        /// <param name="z">初期位置のz座標</param>
        /// <param name="controller">コントローラー</param>
        /// <param name="model">モデル</param>
        /// <param name="attackModel">攻撃のモデル</param>
        /// <param name="attackColor">生成する攻撃の色</param>
        public Player(string name, float x, float z, Controller controller, PlayerModel model, Model attackModel, Color attackColor)
        {
            this.Name = name;
            this.controller = controller;
            this.model = model;
            this.AttackModel = attackModel;
            this.Color = attackColor;
            this.defaultPosition = new Vector3(x, 0, z);
            DeadTime = TimeSpan.Zero;
            Direction = Directions.Down;

#if GAME_DEBUG
            Stock = 5;
#else
            Stock = 1;
#endif

            BasePositionY = -0.5f;
            ModelInfo.Position = new Vector3(x, BasePositionY, z);
            UpdatePosition();
        }

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        public void Update(GameInfo gameInfo,Input input)
        {
            if (IsDead)
            {
                return;
            }

            UpdatePosition();

            Action = PlayerAction.Nothing;
            NewAttack = null;

            FallIfNecessary(gameInfo);
            ResetStatusOfFallingIfNecessary(gameInfo);
            AttackIfNeccesary();
            InputFromController(gameInfo,input);
            UpdateModel(gameInfo);
        }

        public bool IsInvincible
        {
            get
            {
                return resbornTimer.Elapsed < invincibleTime && resbornTimer.IsRunning;
            }
        }

        /// <summary>
        /// 必要に応じて落下させる
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        private void FallIfNecessary(GameInfo gameInfo)
        {
            if (IsDead)
            {
                return;
            }
            if (IsFalling)
            {
                return;
            }

            if (IsInvincible)
            {
                return;
            }

            FieldInfo fieldInfo = gameInfo.FieldInfo;
            BlockInfo blockInfo = fieldInfo.GetBlockInfo(BlockPositionX, BlockPositionZ);
            if (blockInfo != null && !blockInfo.IsFalling)
            {
                return;
            }

            Fall();
        }

        /// <summary>
        /// 必要に応じて落下の状態を初期化する
        /// </summary>
        private void ResetStatusOfFallingIfNecessary(GameInfo gameInfo)
        {
            if (IsDead)
            {
                return;
            }
            if (!IsFalling)
            {
                return;
            }
            if (FallTime <= minFallTime)
            {
                return;
            }

            if (!gameInfo.HasFinished) Stock--;
            ResetStatusOfFalling();

            if (IsDead)
            {
                if (DeadTime == TimeSpan.Zero) DeadTime = gameInfo.CurrentTime;
                return;
            }
            resbornTimer.Restart();
            ModelInfo.Position = getResetPosition(gameInfo);
        }

		/// <summary>
		/// プレイヤーが復帰する場所を取得する
		/// </summary>
		/// <param name="gameInfo"></param>
		/// <returns></returns>
		private Vector3 getResetPosition(GameInfo gameInfo)
		{
			// 探索するレンジ
			int searchingRange = (gameInfo.FieldInfo.Height < gameInfo.FieldInfo.Width) ? gameInfo.FieldInfo.Width : gameInfo.FieldInfo.Height;
			for (int r = 0; r < searchingRange; r++)
			{
				for (int x = -r + (int)defaultPosition.X; x <= r + (int)defaultPosition.X; x++)
				{
					for (int z = -r + (int)defaultPosition.Z; z <= r + (int)defaultPosition.Z; z++)
					{
						BlockInfo blockInfo = gameInfo.FieldInfo.GetBlockInfo(x, z);
						if (blockInfo != null && blockInfo.PlayerInfo == null && !blockInfo.IsFalling)
						{
							return new Vector3(x, defaultPosition.Y, z);
						}
					}
				}
			}
			
			return defaultPosition;
		}
		
        /// <summary>
        /// コントローラから入力する
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        private void InputFromController(GameInfo gameInfo,Input input)
        {
            if (IsDead)
            {
                return;
            }
            if (IsFalling)
            {
                return;
            }
            if (Action != PlayerAction.Nothing)
            {
                return;
            }
            if (gameInfo.HasFinished)
            {
                return;
            }

            controller.Update(gameInfo, input);
            if (controller.IsAttack && gameInfo.IsRunning)
            {
                PrepareForAttack();
            }
            else if (controller.IsUp)
            {
                Move(gameInfo, Directions.Up);
            }
            else if (controller.IsRight)
            {
                Move(gameInfo, Directions.Right);
            }
            else if (controller.IsLeft)
            {
                Move(gameInfo, Directions.Left);
            }
            else if (controller.IsDown)
            {
                Move(gameInfo, Directions.Down);
            }
        }

        /// <summary>
        /// 可能ならば移動する
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        /// <param name="direction">方向ベクトル</param>
        private void Move(GameInfo gameInfo, Vector3 direction)
        {
            if (IsDead || !gameInfo.IsRunning)
            {
                return;
            }

            this.Direction = direction;
            Action = PlayerAction.Move;

            Rotate();

            Vector3 previousPosition = ModelInfo.Position;
            ModelInfo.Position = ModelInfo.Position + moveSpeed * Direction;
            
            FieldInfo fieldInfo = gameInfo.FieldInfo;
            BlockInfo blockInfo = fieldInfo.GetBlockInfo(BlockPositionX, BlockPositionZ);
             //)&&!IsInvincible
            if (blockInfo == null || blockInfo.IsDead == true || Collides(gameInfo) || (blockInfo.IsFalling && !IsInvincible))
            {
                ModelInfo.Position = previousPosition;
            }
        }

		private bool Collides(GameInfo gameInfo){
            BoundingSphere mySphere = new BoundingSphere();
            mySphere.Radius = 0.6f;
            mySphere.Center = ModelInfo.Position + Direction * moveSpeed + Direction / 2.0f * moveSpeed;

		    BoundingSphere rivalSphere = new BoundingSphere();
			rivalSphere.Radius = 0.6f;

            foreach (PlayerInfo p in gameInfo.PlayerInfos)
            {
                if (p.IsDead)
                {
                    continue;
                }
                if (p.Name == Name)
                {
                    continue;
                }

                rivalSphere.Center = p.Position;
                if (mySphere.Intersects(rivalSphere))
                {
                    return true;
                }
            }
			return false;
		}

		/// <summary>
		/// モデルを回転する
		/// </summary>
		private void Rotate()
		{
            ModelInfo.Radian.Y = RadianY;
		}

        /// <summary>
        /// 攻撃の準備をする
        /// </summary>
        private void PrepareForAttack()
        {
            Action = PlayerAction.PrepareForAttack;
            prepareForAttackTimer.Start();
        }

        /// <summary>
        /// 必要に応じて攻撃する
        /// </summary>
        private void AttackIfNeccesary()
        {
            if (IsDead)
            {
                return;
            }

            if (prepareForAttackTimer.IsRunning)
            {
                if (prepareForAttackTimer.Elapsed < minPrepareForAttackTime)
                {
                    Action = PlayerAction.PrepareForAttack;
                }
                else
                {
                    Attack();
                }
            }

            if (attackTimer.IsRunning)
            {
                if (attackTimer.Elapsed < minAttackTime)
                {
                    Action = PlayerAction.Attack;
                }
                else
                {
                    Action = PlayerAction.Nothing;
                    attackTimer.Reset();
                }
            }
        }

        /// <summary>
        /// 攻撃する
        /// </summary>
        private void Attack()
        {
            if (IsDead)
            {
                return;
            }

            Action = PlayerAction.Attack;
			NewAttack = new Attack(this);

            prepareForAttackTimer.Reset();
            attackTimer.Start();
        }
        
        /// <summary>
        /// 生成する攻撃の初期位置の座標
        /// </summary>
        public Vector3 AttackPosition
        {
            get
            {
                float x = BlockPositionX + Direction.X;
                float z = BlockPositionZ + Direction.Z;
                return new Vector3(x, 0, z);
            }
        }

        /// <summary>
        /// 生成する攻撃の速度の
        /// </summary>
        public Vector3 AttackSpeed
        {
            get
            {
                float x = Direction.X * attackSpeed;
                float z = Direction.Z * attackSpeed;
                return new Vector3(x, 0, z);
            }
        }

        /// <summary>
        /// モデルを更新する
        /// </summary>
        private void UpdateModel(GameInfo gameInfo)
        {
            switch (Action)
            {
                case PlayerAction.Attack:
                case PlayerAction.PrepareForAttack:
                    model.ChangeMotion(PlayerModel.ModelAnimation.ATTACK);
                    break;

                case PlayerAction.Nothing:

                case PlayerAction.Move:
                    model.ChangeMotion(PlayerModel.ModelAnimation.WALK);
                    break;
            }

            model.Update(gameInfo.GameTime);
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="camera">モデル用のカメラ</param>
        public void Draw(Camera camera)
        {
            if (IsDead)
            {
                return;
            }
            model.Draw(camera,ModelInfo);
        }

        public PlayerIndex Index
        {
            get
            {
                return controller.PlayerIndex;
            }
        }

		public PlayerModel Model
		{
			get { return model; }
		}
    }
}
