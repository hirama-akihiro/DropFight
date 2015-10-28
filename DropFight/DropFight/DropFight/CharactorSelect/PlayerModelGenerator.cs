using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DropFight.CharactorSelect
{
    class PlayerModelGenerator
    {
        private PlayerModelGenerator()
        {
        }
        public static PlayerModel getModel(ContentManager content, ModelType type, ModelColor color)
        {
            Model normal, attack, walk;
            switch (type)
            {

                case ModelType.Legend:
                    switch (color)
                    {

                        case ModelColor.BLUE:

                            normal = content.Load<Model>("Bone/Legend/BLegend/BLegend(normal)");
                            walk = content.Load<Model>("Bone/Legend/BLegend/BLegend(walk)");
                            attack = content.Load<Model>("Bone/Legend/BLegend/BLegend(attack)");

                            break;
                        case ModelColor.RED:

                            normal = content.Load<Model>("Bone/Legend/RLegend/RLegend(normal)");
                            walk = content.Load<Model>("Bone/Legend/RLegend/RLegend(walk)");
                            attack = content.Load<Model>("Bone/Legend/RLegend/RLegend(attack)");
                            break;
                        case ModelColor.GREEN:

                            normal = content.Load<Model>("Bone/Legend/GLegend/GLegend(normal)");
                            walk = content.Load<Model>("Bone/Legend/GLegend/GLegend(walk)");
                            attack = content.Load<Model>("Bone/Legend/GLegend/GLegend(attack)");
                            break;
                        case ModelColor.ORANGE:

                            normal = content.Load<Model>("Bone/Legend/OLegend/OLegend(normal)");
                            walk = content.Load<Model>("Bone/Legend/OLegend/OLegend(walk)");
                            attack = content.Load<Model>("Bone/Legend/OLegend/OLegend(attack)");
                            break;
                        default:

                            normal = content.Load<Model>("Bone/Legend/RLegend/RLegend(walk)");
                            walk = content.Load<Model>("Bone/Legend/RLegend/RLegend(walk)");
                            attack = content.Load<Model>("Bone/Legend/RLegend/RLegend(attack)");
                            break;
                    }
                    break;
                case ModelType.SnowMan:
                    switch (color)
                    {
                        case ModelColor.BLUE:

                            normal = content.Load<Model>("Bone/SnowMan/BSnowMan/BSnowMan(normal)");
                            walk = content.Load<Model>("Bone/SnowMan/BSnowMan/BSnowMan(walk)");
                            attack = content.Load<Model>("Bone/SnowMan/BSnowMan/BSnowMan(attack)");

                            break;
                        case ModelColor.RED:

                            normal = content.Load<Model>("Bone/SnowMan/RSnowMan/RSnowMan(normal)");
                            walk = content.Load<Model>("Bone/SnowMan/RSnowMan/RSnowMan(walk)");
                            attack = content.Load<Model>("Bone/SnowMan/RSnowMan/RSnowMan(attack)");
                            break;
                        case ModelColor.GREEN:

                            normal = content.Load<Model>("Bone/SnowMan/GSnowMan/GSnowMan(normal)");
                            walk = content.Load<Model>("Bone/SnowMan/GSnowMan/GSnowMan(walk)");
                            attack = content.Load<Model>("Bone/SnowMan/GSnowMan/GSnowMan(attack)");
                            break;
                        case ModelColor.ORANGE:

                            normal = content.Load<Model>("Bone/SnowMan/OSnowMan/OSnowMan(normal)");
                            walk = content.Load<Model>("Bone/SnowMan/OSnowMan/OSnowMan(walk)");
                            attack = content.Load<Model>("Bone/SnowMan/OSnowMan/OSnowMan(attack)");
                            break;
                        default:

                            normal = content.Load<Model>("Bone/SnowMan/RSnowMan/RSnowMan(normal)");
                            walk = content.Load<Model>("Bone/SnowMan/RSnowMan/RSnowMan(walk)");
                            attack = content.Load<Model>("Bone/SnowMan/RSnowMan/RSnowMan(attack)");
                            break;
                    }
                    break;
                case ModelType.Inoshishi:
                    switch (color)
                    {
                        case ModelColor.BLUE:

                            normal = content.Load<Model>("Bone/Inoshishi/BInoshishi/BInoshishi(normal)");
                            walk = content.Load<Model>("Bone/Inoshishi/BInoshishi/BInoshishi(walk)");
                            attack = content.Load<Model>("Bone/Inoshishi/BInoshishi/BInoshishi(attack)");
                            break;
                        case ModelColor.RED:
                            normal = content.Load<Model>("Bone/Inoshishi/RInoshishi/RInoshishi(normal)");
                            walk = content.Load<Model>("Bone/Inoshishi/RInoshishi/RInoshishi(walk)");
                            attack = content.Load<Model>("Bone/Inoshishi/RInoshishi/RInoshishi(attack)");
                            break;
                        case ModelColor.GREEN:
                            normal = content.Load<Model>("Bone/Inoshishi/GInoshishi/GInoshishi(normal)");
                            walk = content.Load<Model>("Bone/Inoshishi/GInoshishi/GInoshishi(walk)");
                            attack = content.Load<Model>("Bone/Inoshishi/GInoshishi/GInoshishi(attack)");
                            break;
                        case ModelColor.ORANGE:
                            normal = content.Load<Model>("Bone/Inoshishi/OInoshishi/OInoshishi(normal)");
                            walk = content.Load<Model>("Bone/Inoshishi/OInoshishi/OInoshishi(walk)");
                            attack = content.Load<Model>("Bone/Inoshishi/OInoshishi/OInoshishi(attack)");
                            break;
                        default:
                            normal = content.Load<Model>("Bone/Inoshishi/OInoshishi/OInoshishi(normal)");
                            walk = content.Load<Model>("Bone/Inoshishi/OInoshishi/OInoshishi(walk)");
                            attack = content.Load<Model>("Bone/Inoshishi/OInoshishi/OInoshishi(attack)");
                            break;
                    }
                    break;
                case ModelType.Metall:
                    switch (color)
                    {
                        case ModelColor.BLUE:
                            normal = content.Load<Model>("Bone/Metall/BMetall/BMetall(normal)");
                            walk = content.Load<Model>("Bone/Metall/BMetall/BMetall(walk)");
                            attack = content.Load<Model>("Bone/Metall/BMetall/BMetall(attack)");
                            break;
                        case ModelColor.RED:
                            normal = content.Load<Model>("Bone/Metall/RMetall/RMetall(normal)");
                            walk = content.Load<Model>("Bone/Metall/RMetall/RMetall(walk)");
                            attack = content.Load<Model>("Bone/Metall/RMetall/RMetall(attack)");
                            break;
                        case ModelColor.GREEN:
                            normal = content.Load<Model>("Bone/Metall/GMetall/GMetall(normal)");
                            walk = content.Load<Model>("Bone/Metall/GMetall/GMetall(walk)");
                            attack = content.Load<Model>("Bone/Metall/GMetall/GMetall(attack)");
                            break;
                        case ModelColor.ORANGE:
                            normal = content.Load<Model>("Bone/Metall/OMetall/OMetall(normal)");
                            walk = content.Load<Model>("Bone/Metall/OMetall/OMetall(walk)");
                            attack = content.Load<Model>("Bone/Metall/OMetall/OMetall(attack)");
                            break;
                        default:
                            normal = content.Load<Model>("Bone/Metall/OMetall/OMetall(normal)");
                            walk = content.Load<Model>("Bone/Metall/OMetall/OMetall(walk)");
                            attack = content.Load<Model>("Bone/Metall/OMetall/OMetall(attack)");
                            break;
                    }
                    break;
                default:
                    normal = content.Load<Model>("Bone/Metall/OMetall/OMetall(normal)");
                    walk = content.Load<Model>("Bone/Metall/OMetall/OMetall(walk)");
                    attack = content.Load<Model>("Bone/Metall/OMetall/OMetall(attack)");
                    break;
            }
            return new PlayerModel(normal, attack, walk);
        }
    }
}
