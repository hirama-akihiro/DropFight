using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Util
{
    [Obsolete]
	public static class DrawUtil {
		/// <summary>
		/// 画面サイズに合わせて良い感じに描画してくれる
		/// </summary>
		/// <param name="spriteBatch">スプライトバッチ</param>
		/// <param name="texture">テクスチャ</param>
		/// <param name="position">1600x1200での位置</param>
		/// <param name="scale">テクスチャ拡大率</param>
		/// <param name="color">テクスチャカラー</param>
		public static void ScalingDraw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float scale, Color color) {
			spriteBatch.Draw(texture, ScalingRect(texture.Bounds, position, scale), color);
		}

		/// <summary>
		/// 画面サイズに合わせて良い感じに描画してくれる
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch</param>
		/// <param name="texture">テクスチャー。</param>
		/// <param name="position">1600x1200での位置</param>
		/// <param name="scale">テクスチャ拡大率</param>
		/// <param name="sourceRectangle">テクスチャーから元のテクセルをテクセル単位で指定する矩形。テクスチャー全体を描画する場合は null を使用します。</param>
		/// <param name="color">スプライトを着色する色。フルカラーで着色なしの場合は Color.White を使用します。</param>
		/// <param name="rotation">スプライトを中心の周りで回転させる角度 (ラジアン単位) を指定します。</param>
		/// <param name="origin">スプライトの原点。デフォルト値は (0,0) で、左上隅を表します。</param>
		/// <param name="effects">適用するエフェクト。</param>
		/// <param name="layerDepth">レイヤーの深さ。既定では、0 はフロント レイヤーを表し、1 はバック レイヤーを表します。描画中にスプライトをソートする場合は SpriteSortMode を使用します。</param>
		public static void ScalingDraw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float scale, Nullable<Rectangle> sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
		{
			if (sourceRectangle == null)
				spriteBatch.Draw(texture, ScalingRect(texture.Bounds, position, scale), sourceRectangle, color, rotation, origin, effects, layerDepth);
			else
				spriteBatch.Draw(texture, ScalingRect((Rectangle)sourceRectangle, position, scale), sourceRectangle, color, rotation, origin, effects, layerDepth);
		}

		/// <summary>
		/// 画面サイズにあわせて良い感じに文字列を描画してくれる
		/// ※文字サイズは変わらない，描画位置のみスケーリング
		/// </summary>
		/// <param name="spriteBatch">スプライトバッチ</param>
		/// <param name="spriteFont">スプライトフォント</param>
		/// <param name="text">描画テキスト</param>
		/// <param name="position">1600x1200での位置</param>
		/// <param name="color">フォントカラー</param>
		public static void ScalingDrawString(this SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color){
            Vector2 scaledPosition = new Vector2(position.X * Game1.ScreenWidthScale, position.Y * Game1.ScreenHeightScale);
			spriteBatch.DrawString(spriteFont, text, scaledPosition, color);
		}

		/// <summary>
		/// 画面サイズにあわせて四角を良い感じに縮小してくれる
		/// </summary>
		/// <param name="rect">縮小する四角</param>
		/// <param name="pos">位置</param>
		/// <param name="scale">さらに縮小率</param>
		/// <returns>縮小された四角</returns>
		public static Rectangle ScalingRect(Rectangle rect, Vector2 pos, float scale) {
            Vector2 scaling = new Vector2(Game1.ScreenWidthScale * scale, Game1.ScreenHeightScale * scale);
#warning 変更
            return new Rectangle((int)(pos.X * Game1.ScreenWidthScale/*scaling.X*/), (int)(pos.Y * Game1.ScreenHeightScale/*scaling.Y*/), (int)(rect.Width * scaling.X), (int)(rect.Height * scaling.Y));
		}

		/// <summary>
		/// 姿勢行列を良い感じに計算してくれる
		/// </summary>
		/// <param name="pose">姿勢データ配列(xyz + rpy)</param>
		/// <returns>姿勢行列</returns>
		public static Matrix ComputePoseMatrix(float[] pose) {
			float roll_rad = MathHelper.ToRadians(pose[3]);
			float pitch_rad = MathHelper.ToRadians(pose[4]);
			float yaw_rad = MathHelper.ToRadians(pose[5]);
			return Matrix.CreateRotationZ(roll_rad) *
				Matrix.CreateRotationX(pitch_rad) *
				Matrix.CreateRotationY(yaw_rad) *
				Matrix.CreateTranslation(new Vector3(pose[0], pose[1], pose[2]));			
		}
	}
}
