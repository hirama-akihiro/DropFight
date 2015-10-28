using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DropFight.ModelUtils
{
    public class PlayerModel : AnimationModel
    {
        public static readonly string normalStr = "ArmatureAction";
        public static readonly string walkStr = "walk";
        public static readonly string attackStr = "attack";

        public ModelAnimation modelAnimation = ModelAnimation.WALK;

        /// <param name="normalModel">何もしていない時のモデル</param>
        /// <param name="attackModel">攻撃時のモデル</param>
        /// <param name="walkModel">歩いている時のモデル</param>
        public PlayerModel(Model normalModel, Model attackModel, Model walkModel)
            : base(walkModel, CreateModels(normalModel, attackModel, walkModel))
        {
            ChangeMotion(modelAnimation);
        }

        /// <summary>
        /// 全動作のモデルを格納した辞書を生成する
        /// </summary>
        /// <param name="normalModel">何もしていない時のモデル</param>
        /// <param name="attackModel">攻撃時のモデル</param>
        /// <param name="walkModel">歩いている時のモデル</param>
        private static Dictionary<string, Model> CreateModels(Model normalModel, Model attackModel, Model walkModel)
        {
            Dictionary<string, Model> models = new Dictionary<string, Model>();
            models.Add(PlayerModel.normalStr, normalModel);
            models.Add(PlayerModel.attackStr, attackModel);
            models.Add(PlayerModel.walkStr, walkModel);
            return models;
        }

        /// <summary>
        /// アニメーションの種類変える。何回呼んでも問題ない
        /// </summary>
        /// <param name="motion"></param>
        public void ChangeMotion(ModelAnimation modelAnimation)
        {
            this.modelAnimation = modelAnimation;
        }

        /// <summary>
        /// 保持している動作の種類
        /// </summary>
        public enum ModelAnimation
        {
            NORMAL, WALK, ATTACK
        }

        /// <summary>
        /// 保持しているモーションによって動作を変える
        /// base.AnimationStartは同じモーションならアニメーションが初期化されることはない
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime){
            switch (modelAnimation)
            {
                case ModelAnimation.NORMAL:
                    AnimationStart(normalStr, normalStr);
                    break;
                case ModelAnimation.ATTACK:
                    AnimationStart(attackStr, attackStr);
                    break;
                case ModelAnimation.WALK:
                    AnimationStart(walkStr, walkStr);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

    }
}
