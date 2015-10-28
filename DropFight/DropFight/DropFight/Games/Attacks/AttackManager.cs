using System.Collections.Generic;
using System.Linq;
using DropFight.ModelUtils;

namespace DropFight.Games.Attacks
{
    /// <summary>
    /// 攻撃のマネージャー
    /// </summary>
    public class AttackManager
    {
        /// <summary>
        /// 攻撃のリスト
        /// </summary>
        private IList<Attack> attacks = new List<Attack>();

        /// <summary>
        /// 攻撃の情報の列挙子
        /// </summary>
        public IEnumerable<AttackInfo> Info
        {
            get
            {
                return attacks.Select(attack => attack.Info);
            }
        }

        /// <summary>
        /// 攻撃を追加する
        /// </summary>
        /// <param name="attack">攻撃</param>
        public void Add(Attack attack)
        {
            attacks.Add(attack);
        }

        /// <summary>
        /// 攻撃を削除する
        /// </summary>
        /// <param name="attack">攻撃</param>
        public void Remove(Attack attack)
        {
            attacks.Remove(attack);
        }

        /// <summary>
        /// 更新する
        /// </summary>
        public void Update()
        {
            foreach (Attack attack in attacks)
            {
                attack.Update();
            }
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="spriteBatch">スプライト描画用のオブジェクト</param>
        /// <param name="camera">モデル用のカメラ</param>
        public void Draw(Camera camera)
        {
            foreach (Attack attack in attacks)
            {
                attack.Draw(camera);
            }
        }
    }
}
