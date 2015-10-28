using System.Collections.Generic;
using System.Linq;

namespace DropFight.Games.Blocks
{
    /// <summary>
    /// フィールドの情報
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// ブロックの情報の2次元配列
        /// </summary>
        private readonly BlockInfo[,] blocksInfo;

        /// <summary>
        /// 幅(ブロック)
        /// </summary>
        public int Width
        {
            get
            {
                return blocksInfo.GetLength(0);
            }
        }

        /// <summary>
        /// 高さ(ブロック)
        /// </summary>
        public int Height
        {
            get
            {
                return blocksInfo.GetLength(1);
            }
        }

        /// <param name="field">フィールド</param>
        public FieldInfo(Field field)
        {
            int width = field.Width;
            int height = field.Height;
            blocksInfo = new BlockInfo[width, height];

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    BlockInfo blockInfo = null;
                    Block block = field.GetBlock(x, z);
                    if (block != null)
                    {
                        blockInfo = block.Info;
                    }

                    blocksInfo[x, z] = blockInfo;
                }
            }
        }

        /// <summary>
        /// 指定された座標のブロックの情報を返す
        /// 指定された座標にブロックがなければnullを返す
        /// </summary>
        /// <param name="x">ブロックのx座標</param>
        /// <param name="z">ブロックのz座標</param>
        public BlockInfo GetBlockInfo(int x, int z)
        {
            if (!IsValidPosition(x, z))
            {
                return null;
            }

            return blocksInfo[x, z];
        }
        
        /// <summary>
        /// 座標が範囲外かどうかを返す
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="z">z座標</param>
        /// <returns></returns>
        private bool IsValidPosition(int x, int z)
        {
            if (x < 0)
            {
                return false;
            }
            if (x >= Width)
            {
                return false;
            }
            if (z < 0)
            {
                return false;
            }
            if (z >= Height)
            {
                return false;
            }
            return true;
        }
    }
}
