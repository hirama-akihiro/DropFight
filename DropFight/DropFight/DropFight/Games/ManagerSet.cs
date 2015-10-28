using DropFight.Games.Attacks;
using DropFight.Games.Blocks;
using DropFight.Games.Players;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Games
{
    /// <summary>
    /// マネージャー一式
    /// </summary>
    public class ManagerSet
    {
        /// <summary>
        /// 攻撃のマネージャー
        /// </summary>
        public AttackManager AttackManager
        {
            get;
            private set;
        }

        /// <summary>
        /// ブロックのマネージャー
        /// </summary>
        public Field Field
        {
            get;
            private set;
        }

        /// <summary>
        /// プレイヤーのマネージャー
        /// </summary>
        public PlayerManager PlayerManager
        {
            get;
            private set;
        }

        /// <param name="attackManager">攻撃のマネージャー</param>
        /// <param name="field">ブロックのマネージャー</param>
        /// <param name="playerManager">プレイヤーのマネージャー</param>
        public ManagerSet(AttackManager attackManager, Field field, PlayerManager playerManager)
        {
            this.AttackManager = attackManager;
            this.Field = field;
            this.PlayerManager = playerManager;
        }

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        public void Update(GameInfo gameInfo,Input input)
        {
            Field.Update(gameInfo);

            PlayerManager.Update(gameInfo,input);
            AddNewAttacks();

            AttackManager.Update();
        }

        /// <summary>
        /// プレイヤーが新たに生成した攻撃を全て追加する
        /// </summary>
        private void AddNewAttacks()
        {
            foreach (Player player in PlayerManager.Players)
            {
                Attack newAttack = player.NewAttack;
                if (newAttack != null)
                {
                    AttackManager.Add(newAttack);
                }
            }
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="camera">モデル用のカメラ</param>
        public void Draw(Camera camera)
        {
            Field.Draw(camera);
            PlayerManager.Draw(camera);
            AttackManager.Draw(camera);
        }
    }
}
