using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Scenes
{
    /// <summary>
    /// 場面
    /// </summary>
    public interface Scene
    {
        /// <summary>
        /// 更新する
        /// </summary>
        /// <param name="gameTime">ゲーム内部の時間</param>
        /// <returns>次の場面</returns>
        Scene Update(GameTime gameTime,Input input);

        /// <summary>
        /// 描画する
        /// </summary>
        void Draw(SpriteBatch spriteBatch);
    }
}
