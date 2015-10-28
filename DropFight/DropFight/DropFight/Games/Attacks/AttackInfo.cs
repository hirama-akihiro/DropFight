using Microsoft.Xna.Framework;
using DropFight.Games.Players;

namespace DropFight.Games.Attacks
{
    /// <summary>
    /// 外部から見える攻撃の情報
    /// </summary>
    public class AttackInfo
    {
        /// <summary>
        /// 位置
        /// </summary>
        public readonly Vector3 Position;
        
        /// <summary>
        /// 対応するブロックの座標
        /// </summary>
        public readonly Vector3 BlockPosition;
        
        /// <summary>
        /// 速度
        /// </summary>
        public readonly Vector3 Speed;

		public readonly PlayerInfo OwnerInfo;

        /// <param name="attack">攻撃</param>
        public AttackInfo(Attack attack)
        {
            Position = attack.ModelInfo.Position;
            BlockPosition = attack.BlockPosition;
            Speed = attack.Speed;
			OwnerInfo = attack.OwnerInfo;
        }
    }
}
