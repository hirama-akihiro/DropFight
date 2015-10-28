using DropFight.Games;
using Microsoft.Xna.Framework;

namespace DropFight.Games.Controllers
{
    /// <summary>
    /// GameSceneで使うコントローラです。
    /// これを継承したコントローラによってMoveとAttackが呼び出され操作できるようになる仕組みです
    /// </summary>
    public abstract class Controller
    {
        /// <summary>
        /// GameSceneで使うコントローラです。
        /// 継承したものを用いてください
        /// </summary>
        /// <param name="playerIndex"></param>
        protected Controller(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        private Direction direction = Direction.STOP;
        /// <summary>
        /// 攻撃が入力されているか
        /// </summary>
        public bool IsAttack
        {
            get;
            private set;
        }
        /// <summary>
        /// 上が入力されているか
        /// </summary>
        public bool IsUp
        {
            get
            {
                return direction == Direction.UP;
            }
        }
        /// <summary>
        /// 右が入力されているか
        /// </summary>
        public bool IsRight
        {
            get
            {
                return direction == Direction.RIGHT;
            }
        }
        /// <summary>
        /// 下が入力されているか
        /// </summary>
        public bool IsDown
        {
            get
            {
                return direction == Direction.DOWN;
            }
        }
        /// <summary>
        /// 左が入力されているか
        /// </summary>
        public bool IsLeft
        {
            get
            {
                return direction == Direction.LEFT;
            }
        }

        /// <summary>
        /// 攻撃をさせる
        /// </summary>
        protected void Attack()
        {
            IsAttack = true;
        }

        /// <summary>
        /// 動かす
        /// </summary>
        /// <param name="direction"></param>
        protected void Move(Direction direction)
        {
            this.direction = direction;
        }

        /// <summary>
        /// コントローラが持つプレイヤー番号です
        /// </summary>
        public PlayerIndex PlayerIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// 更新する。先に呼ぶこと
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        /// <param name="gameInfo">入力</param>
        public virtual void Update(GameInfo gameInfo,Input input)
        {
            direction = Direction.STOP;
            IsAttack = false;
        }

        /// <summary>
        /// 押されているコントローラの方向
        /// </summary>
        protected enum Direction
        {
            UP, RIGHT, DOWN, LEFT, STOP
        }
    }

}
