using DropFight.ModelUtils;
using Microsoft.Xna.Framework;
using DropFight.Games.Players;

namespace DropFight.Games.Blocks
{
    /// <summary>
    /// 外部から見えるブロックの情報
    /// </summary>
    public class BlockInfo : FallableInfo
    {
		public readonly bool IsDead;
		public readonly PlayerInfo PlayerInfo;
        /// <param name="block">ブロック</param>
        public BlockInfo(Block block)
            : base(block)
        {
			this.IsDead = block.IsDead;
			this.PlayerInfo = block.PlayerInfo;
        }
    }
}
