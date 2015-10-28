using Microsoft.Xna.Framework;
using System;

namespace DropFight.Games.Players
{
    /// <summary>
    /// 外部から見えるプレイヤーの情報
    /// </summary>
    public class PlayerInfo : FallableInfo
    {
        /// <summary>
        /// 名前
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 向き
        /// </summary>
        public readonly Vector3 Direction;

        /// <summary>
        /// プレイヤーの行動
        /// </summary>
        public readonly PlayerAction Action;

        /// <summary>
        /// 残機
        /// </summary>
        public readonly uint Stock;

        /// <summary>
        /// 死亡したかどうか
        /// </summary>
        public readonly bool IsDead;

        /// <summary>
        /// 死亡した時間。死亡してないならTimeSpan.Zero
        /// </summary>
        public readonly TimeSpan DeadTime;

        /// <summary>
        /// プレイヤー番号
        /// </summary>
        public readonly PlayerIndex Index;

		public readonly int BlockPositionX;
		public readonly int BlockPositionZ;
        /// <param name="player">プレイヤー</param>
        public PlayerInfo(Player player)
            : base(player)
        {
            Name = player.Name;
            Direction = player.Direction;
            Action = player.Action;
            Stock = player.Stock;
            IsDead = player.IsDead;
            Index = player.Index;
			BlockPositionX = player.BlockPositionX;
			BlockPositionZ = player.BlockPositionZ;
            DeadTime = player.DeadTime;
        }
    }
}
