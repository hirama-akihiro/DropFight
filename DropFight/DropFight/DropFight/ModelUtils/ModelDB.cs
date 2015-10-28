using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DropFight.ModelUtils
{
	/// <summary>
	/// モデル情報データベース(取得のみ)
	/// </summary>
	class ModelDB
	{
		#region スケール情報
		private static Vector3 MettallScale = new Vector3(0.4f, 0.4f, 0.4f);
		private static Vector3 LegendScale = new Vector3(0.25f, 0.25f, 0.25f);
		private static Vector3 SnowManScale = new Vector3(0.3f, 0.3f, 0.3f);
		private static Vector3 InoshishiScale = new Vector3(0.4f, 0.25f, 0.25f);
		#endregion

		#region ファイルパス情報

		#region 攻撃モデル
		private static String DirBWave = "AttackWave/BWave";
		private static String DirRWave = "AttackWave/RWave";
		private static String DirGWave = "AttackWave/GWave";
		private static String DirOWave = "AttackWave/OWave";
		#endregion

		#region イノシシモデル
		private static String DirBInoshishiN = "Bone/Inoshishi/BInoshishi/BInoshishi(normal)";
		private static String DirBInoshishiW = "Bone/Inoshishi/BInoshishi/BInoshishi(walk)";
		private static String DirBInoshishiA = "Bone/Inoshishi/BInoshishi/BInoshishi(attack)";

		private static String DirRInoshishiN = "Bone/Inoshishi/RInoshishi/RInoshishi(normal)";
		private static String DirRInoshishiW = "Bone/Inoshishi/RInoshishi/RInoshishi(walk)";
		private static String DirRInoshishiA = "Bone/Inoshishi/RInoshishi/RInoshishi(attack)";

		private static String DirGInoshishiN = "Bone/Inoshishi/GInoshishi/GInoshishi(normal)";
		private static String DirGInoshishiW = "Bone/Inoshishi/GInoshishi/GInoshishi(walk)";
		private static String DirGInoshishiA = "Bone/Inoshishi/GInoshishi/GInoshishi(attack)";

		private static String DirOInoshishiN = "Bone/Inoshishi/OInoshishi/OInoshishi(normal)";
		private static String DirOInoshishiW = "Bone/Inoshishi/OInoshishi/OInoshishi(walk)";
		private static String DirOInoshishiA = "Bone/Inoshishi/OInoshishi/OInoshishi(attack)";
		#endregion

		#region メットールモデル
		private static String DirBMetallN = "Bone/Metall/BMetall/BMetall(normal)";
		private static String DirBMetallW = "Bone/Metall/BMetall/BMetall(walk)";
		private static String DirBMetallA = "Bone/Metall/BMetall/BMetall(attack)";

		private static String DirRMetallN = "Bone/Metall/RMetall/RMetall(normal)";
		private static String DirRMetallW = "Bone/Metall/RMetall/RMetall(walk)";
		private static String DirRMetallA = "Bone/Metall/RMetall/RMetall(attack)";

		private static String DirGMetallN = "Bone/Metall/GMetall/GMetall(normal)";
		private static String DirGMetallW = "Bone/Metall/GMetall/GMetall(walk)";
		private static String DirGMetallA = "Bone/Metall/GMetall/GMetall(attack)";

		private static String DirOMetallN = "Bone/Metall/OMetall/OMetall(normal)";
		private static String DirOMetallW = "Bone/Metall/OMetall/OMetall(walk)";
		private static String DirOMetallA = "Bone/Metall/OMetall/OMetall(attack)";
		#endregion

		#region レジェンドモデル
		private static String DirBLegendN = "Bone/Legend/BLegend/BLegend(normal)";
		private static String DirBLegendW = "Bone/Legend/BLegend/BLegend(walk)";
		private static String DirBLegendA = "Bone/Legend/BLegend/BLegend(attack)";

		private static String DirRLegendN = "Bone/Legend/RLegend/RLegend(normal)";
		private static String DirRLegendW = "Bone/Legend/RLegend/RLegend(walk)";
		private static String DirRLegendA = "Bone/Legend/RLegend/RLegend(attack)";

		private static String DirGLegendN = "Bone/Legend/GLegend/GLegend(normal)";
		private static String DirGLegendW = "Bone/Legend/GLegend/GLegend(walk)";
		private static String DirGLegendA = "Bone/Legend/GLegend/GLegend(attack)";

		private static String DirOLegendN = "Bone/Legend/OLegend/OLegend(normal)";
		private static String DirOLegendW = "Bone/Legend/OLegend/OLegend(walk)";
		private static String DirOLegendA = "Bone/Legend/OLegend/OLegend(attack)";
		#endregion

		#region スノウマンモデル
		private static String DirBSnowManN = "Bone/SnowMan/BSnowMan/BSnowMan(normal)";
		private static String DirBSnowManW = "Bone/SnowMan/BSnowMan/BSnowMan(walk)";
		private static String DirBSnowManA = "Bone/SnowMan/BSnowMan/BSnowMan(attack)";

		private static String DirRSnowManN = "Bone/SnowMan/RSnowMan/RSnowMan(normal)";
		private static String DirRSnowManW = "Bone/SnowMan/RSnowMan/RSnowMan(walk)";
		private static String DirRSnowManA = "Bone/SnowMan/RSnowMan/RSnowMan(attack)";

		private static String DirGSnowManN = "Bone/SnowMan/GSnowMan/GSnowMan(normal)";
		private static String DirGSnowManW = "Bone/SnowMan/GSnowMan/GSnowMan(walk)";
		private static String DirGSnowManA = "Bone/SnowMan/GSnowMan/GSnowMan(attack)";

		private static String DirOSnowManN = "Bone/SnowMan/OSnowMan/OSnowMan(normal)";
		private static String DirOSnowManW = "Bone/SnowMan/OSnowMan/OSnowMan(walk)";
		private static String DirOSnowManA = "Bone/SnowMan/OSnowMan/OSnowMan(attack)";
		#endregion

		#endregion

		/// <summary>
		/// モデルのスケールを取得するメソッド
		/// </summary>
		/// <param name="modelType"></param>
		/// <returns></returns>
		public static Vector3 GetScale(ModelType modelType)
		{
			switch (modelType)
			{
				case ModelType.Metall:
					return MettallScale;
				case ModelType.Legend:
					return LegendScale;
				case ModelType.SnowMan:
					return SnowManScale;
				case ModelType.Inoshishi:
					return InoshishiScale;
				default:
					return Vector3.One;
			}
		}

		/// <summary>
		/// モデルのファイルパス
		/// </summary>
		/// <param name="modelType"></param>
		/// <param name="modelColor"></param>
		/// <param name="modelAnimation"></param>
		/// <returns></returns>
		public static String GetModelPath(ModelType modelType, ModelColor modelColor, ModelAnimation modelAnimation)
		{
			String filePath = DirBWave;
			switch (modelType)
			{
				case ModelType.Metall:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirBMetallN;
								case ModelAnimation.WALK:
									return DirBMetallW;
								case ModelAnimation.ATTACK:
									return DirBMetallA;
							}
							break;
						case ModelColor.RED:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirRMetallN;
								case ModelAnimation.WALK:
									return DirRMetallW;
								case ModelAnimation.ATTACK:
									return DirRMetallA;
							}
							break;
						case ModelColor.GREEN:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirGMetallN;
								case ModelAnimation.WALK:
									return DirGMetallW;
								case ModelAnimation.ATTACK:
									return DirGMetallA;
							}
							break;
						case ModelColor.ORANGE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirOMetallN;
								case ModelAnimation.WALK:
									return DirOMetallW;
								case ModelAnimation.ATTACK:
									return DirOMetallA;
							}
							break;
					}
					break;
				case ModelType.Legend:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirBLegendN;
								case ModelAnimation.WALK:
									return DirBLegendW;
								case ModelAnimation.ATTACK:
									return DirBLegendA;
							}
							break;
						case ModelColor.RED:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirRLegendN;
								case ModelAnimation.WALK:
									return DirRLegendW;
								case ModelAnimation.ATTACK:
									return DirRLegendA;
							}
							break;
						case ModelColor.GREEN:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirGLegendN;
								case ModelAnimation.WALK:
									return DirGLegendW;
								case ModelAnimation.ATTACK:
									return DirGLegendA;
							}
							break;
						case ModelColor.ORANGE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirOLegendN;
								case ModelAnimation.WALK:
									return DirOLegendW;
								case ModelAnimation.ATTACK:
									return DirOLegendA;
							}
							break;
					}
					break;
				case ModelType.SnowMan:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirBSnowManN;
								case ModelAnimation.WALK:
									return DirBSnowManW;
								case ModelAnimation.ATTACK:
									return DirBSnowManA;
							}
							break;
						case ModelColor.RED:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirRSnowManN;
								case ModelAnimation.WALK:
									return DirRSnowManW;
								case ModelAnimation.ATTACK:
									return DirRSnowManA;
							}
							break;
						case ModelColor.GREEN:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirGSnowManN;
								case ModelAnimation.WALK:
									return DirGSnowManW;
								case ModelAnimation.ATTACK:
									return DirGSnowManA;
							}
							break;
						case ModelColor.ORANGE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirOSnowManN;
								case ModelAnimation.WALK:
									return DirOSnowManW;
								case ModelAnimation.ATTACK:
									return DirOSnowManA;
							}
							break;
					}
					break;
				case ModelType.Inoshishi:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirBInoshishiN;
								case ModelAnimation.WALK:
									return DirBInoshishiW;
								case ModelAnimation.ATTACK:
									return DirBInoshishiA;
							}
							break;
						case ModelColor.RED:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirRInoshishiN;
								case ModelAnimation.WALK:
									return DirRInoshishiW;
								case ModelAnimation.ATTACK:
									return DirRInoshishiA;
							}
							break;
						case ModelColor.GREEN:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirGInoshishiN;
								case ModelAnimation.WALK:
									return DirGInoshishiW;
								case ModelAnimation.ATTACK:
									return DirGInoshishiA;
							}
							break;
						case ModelColor.ORANGE:
							switch (modelAnimation)
							{
								case ModelAnimation.NORMAL:
									return DirOInoshishiN;
								case ModelAnimation.WALK:
									return DirOInoshishiW;
								case ModelAnimation.ATTACK:
									return DirOInoshishiA;
							}
							break;
					}
					break;
				case ModelType.Wave:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							return DirBWave;
						case ModelColor.RED:
							return DirRWave;;
						case ModelColor.GREEN:
							return DirGWave;
						case ModelColor.ORANGE:
							return DirOWave;
					}
					break;
			}
			return DirBWave;
		}

		/// <summary>
		/// モデルのファイルパス取得
		/// </summary>
		/// <param name="modelType"></param>
		/// <param name="modelColor"></param>
		/// <returns></returns>
		public static String GetModelPath(ModelType modelType, ModelColor modelColor)
		{
			String filePath = DirBWave;
			switch (modelType)
			{
				case ModelType.Metall:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							return DirBMetallN;
						case ModelColor.RED:
							return DirRMetallN;
						case ModelColor.GREEN:
							return DirGMetallN;
						case ModelColor.ORANGE:
							return DirOMetallN;
					}
					break;
				case ModelType.Legend:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							return DirBMetallN;
						case ModelColor.RED:
							return DirRMetallN;
						case ModelColor.GREEN:
							return DirGMetallN;
						case ModelColor.ORANGE:
							return DirOMetallN;
					}
					break;
				case ModelType.SnowMan:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							return DirBMetallN;
						case ModelColor.RED:
							return DirRMetallN;
						case ModelColor.GREEN:
							return DirGMetallN;
						case ModelColor.ORANGE:
							return DirOMetallN;
					}
					break;
				case ModelType.Inoshishi:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							return DirBMetallN;
						case ModelColor.RED:
							return DirRMetallN;
						case ModelColor.GREEN:
							return DirGMetallN;
						case ModelColor.ORANGE:
							return DirOMetallN;
					}
					break;
				case ModelType.Wave:
					switch (modelColor)
					{
						case ModelColor.BLUE:
							return DirBMetallN;
						case ModelColor.RED:
							return DirRMetallN;
						case ModelColor.GREEN:
							return DirGMetallN;
						case ModelColor.ORANGE:
							return DirOMetallN;
					}
					break;
			}
			return DirBWave;
		}
	}
}
