using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;
using System.Collections;
namespace DropFight.ModelUtils
{
    public abstract class AnimationModel
    {
        /// <summary>
        /// アニメーションプレイヤー
        /// </summary>
        private AnimationPlayer animationPlayer;
        /// <summary>l
        /// アニメーションクリップ集
        /// </summary>
        protected Dictionary<string, AnimationClip> clips;
        /// <summary>
        /// 今表示してるモデル（アニメーション）
        /// </summary>
        private Model currentModel;
        /// <summary>
        /// 保持している全てのモデル
        /// </summary>
        private Dictionary<string, Model> models;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initModel">初期表示モデル</param>
        /// <param name="models">アニメーションを持つモデルの辞書（モデルは一つのアニメーションしか持てないため）</param>
        protected AnimationModel(Model initModel, Dictionary<String, Model> models)
        {
            currentModel = initModel;
            this.models = models;
            // モデルの読み込み
            // スキニングデータ
            SkinningData skinningData = currentModel.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException("This model does not contain a SkinningData tag.");

            // アニメーションプレイヤーの初期化
            animationPlayer = new AnimationPlayer(skinningData);
            // クリップ集の取得
            clips = skinningData.AnimationClips;

        }

        /// <summary>
        /// アニメーションの開始（今のモデルと同じ指定なら初期化されることはない）
        /// </summary>
        /// <param name="clipName">クリップ名</param>
        protected void AnimationStart(string modelName, string clipName)
        {
            Model newModel;
            if (!models.TryGetValue(modelName, out newModel))
            {
                throw new InvalidOperationException("Invaid Model date.");
            }
            if (currentModel != newModel)
            {
                currentModel = newModel;
                // スキニングデータ
                SkinningData skinningData = currentModel.Tag as SkinningData;

                if (skinningData == null)
                    throw new InvalidOperationException("This model does not contain a SkinningData tag.");

                // アニメーションプレイヤーの変更
                animationPlayer = new AnimationPlayer(skinningData);
                //アニメーションを開始
                clips = skinningData.AnimationClips;
            }
            animationPlayer.StartClip(clips[clipName]);
        }

        /// <summary>
        /// アニメーションの更新
        /// （常に呼び続けて問題ない）
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Update(GameTime gameTime)
        {
            animationPlayer.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
        }

        /// <summary>
        /// 描画
        /// </summary>
        public void Draw(Camera camera, ModelInfo modelInfo)
        {
           // ボーンの取得
			Matrix[] bones = animationPlayer.GetSkinTransforms();
			// ワールド座標
			Matrix world = modelInfo.World;

			foreach (ModelMesh mesh in currentModel.Meshes)
			{
				foreach (SkinnedEffect effect in mesh.Effects)
				{
                    effect.EnableDefaultLighting();
					effect.SetBoneTransforms(bones);
					effect.World = world;
					effect.View = camera.View;
					effect.Projection = camera.Projection;
				}
				mesh.Draw();
			}
		}
        
    }
}
