using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DropFight.ModelUtils;

namespace DropFight.Games.Players
{
    /// <summary>
    /// プレイヤーのマネージャー
    /// </summary>
    public class PlayerManager
    {
        /// <summary>
        /// プレイヤーの列挙子
        /// </summary>
        public IEnumerable<Player> Players;

        /// <summary>
        /// プレイヤーの情報の列挙子
        /// </summary>
        public IEnumerable<PlayerInfo> Info
        {
            get
            {
                return Players.Select(player => player.Info);
            }
        }

        /// <param name="players">プレイヤーの列挙子</param>
        public PlayerManager(params Player[] players)
        {
            this.Players = players;
        }

        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameInfo">ゲームの情報</param>
        public void Update(GameInfo gameInfo,Input input)
        {
            foreach (Player player in Players)
            {
                if (player.IsDead)
                {
                    continue;
                }

                player.Update(gameInfo,input);
            }

            // TODO 他のプレイヤーとぶつかったプレイヤーの座標を補正する
            foreach (Player player1 in Players)
            {
                foreach (Player player2 in Players)
                {
                    if (player1 == player2)
                    {
                        continue;
                    }

                    /*
                    Debug.WriteLine(player1.BoundingSphere);
                    Debug.WriteLine(player2.BoundingSphere);
                    Debug.WriteLine(player1.BoundingSphere.Intersects(player2.BoundingSphere));
                    Debug.WriteLine("");
                     */
                }
            }
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="spriteBatch">スプライト描画用のオブジェクト</param>
        /// <param name="camera">モデル用のカメラ</param>
        public void Draw(Camera camera)
        {
            foreach (Player player in Players)
            {
                if (player.IsDead)
                {
                    continue;
                }

                player.Draw(camera);
            }
        }
    }
}