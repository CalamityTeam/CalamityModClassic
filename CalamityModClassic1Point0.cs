using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Tiles;

namespace CalamityModClassic1Point0
{
    public class CalamityModClassic1Point0 : Mod
    {
        public static CalamityModClassic1Point0 instance;

        public override void Load()
        {
            instance = this;
        }

        public override void Unload()
        {
            instance = null;
        }
    }
}