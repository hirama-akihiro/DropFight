using DropFight.Games.Players;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Games.Indicator
{
    /// <summary>
    /// プレイヤーの状態を示します。
    /// プレイヤーの頭上に表示されます。
    /// </summary>
    class NameDrawer
    {
        private SpriteFont font;
        private Player player;
        private float theta = 0.0f;
       
        //枠の太さ
        private static readonly float thickness = 2.0f;
        public NameDrawer(Player player, ContentManager content)
        {
            this.player = player;

            font = content.Load<SpriteFont>("Font/Indicator");
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (player.IsDead) return;

            GraphicsDevice graphics = spriteBatch.GraphicsDevice;
            Viewport viewport = graphics.Viewport;

            Matrix rotatedView = Matrix.CreateRotationY(this.theta) * camera.View;
            Vector3 position = player.ModelInfo.Position;
            // ３次元座標からスクリーンの座標算出
            // 本当なら深度での描画の判断しないといけないけどカメラ固定なので省略
            Vector3 screenPosition3D = viewport.Project(position, camera.Projection, rotatedView, Matrix.Identity);
            Vector2 screenPosition = new Vector2(screenPosition3D.X, screenPosition3D.Y);

            // テキスト描画
            //枠の描画
            spriteBatch.DrawString(font, player.Name, new Vector2(screenPosition.X, screenPosition.Y - thickness), player.Color);
            spriteBatch.DrawString(font, player.Name, new Vector2(screenPosition.X, screenPosition.Y + thickness), player.Color);
            spriteBatch.DrawString(font, player.Name, new Vector2(screenPosition.X - thickness, screenPosition.Y), player.Color);
            spriteBatch.DrawString(font, player.Name, new Vector2(screenPosition.X + thickness, screenPosition.Y - thickness), player.Color);
            //文字の描画
            spriteBatch.DrawString(font, player.Name, new Vector2(screenPosition.X, screenPosition.Y), Color.White);
        }
    }
}
