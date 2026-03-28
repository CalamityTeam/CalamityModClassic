using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Calamitas
{
    public class CalamityDust : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashes of Calamity");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 4, 50, 0);
			Item.rare = 7;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            float num = (float)Main.rand.Next(90, 111) * 0.01f;
            num *= Main.essScale;
            Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.25f * num, 0.05f * num, 0.25f * num);
        }
    }
}