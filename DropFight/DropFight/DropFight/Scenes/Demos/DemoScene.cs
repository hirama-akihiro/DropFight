using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Scenes.Demos
{
    /// <summary>
    /// デモ場面
    /// </summary>
    public abstract class DemoScene : Scene
    {
        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        /// <returns>次の場面</returns>
        public Scene Update(GameTime gameTime, Input input)
        {
            // TODO: 何かしらの条件でタイトルに戻る

            return UpdateDemo(gameTime);
        }

        /// <summary>
        /// デモ場面を更新する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        /// <returns>次のデモ場面</returns>
        protected abstract DemoScene UpdateDemo(GameTime gameTime);

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
