namespace DropFight.Games.Players
{
    /// <summary>
    /// プレイヤーの行動
    /// </summary>
    public enum PlayerAction
    {
        /// <summary>
        /// なし
        /// </summary>
        Nothing,

        /// <summary>
        /// 移動
        /// </summary>
        Move,

        /// <summary>
        /// 攻撃の準備
        /// </summary>
        PrepareForAttack,

        /// <summary>
        /// 攻撃
        /// </summary>
        Attack,
    }
}
