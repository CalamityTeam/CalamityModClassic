using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
    public class Levi : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Levi");
            // Tooltip.SetDefault("Summons a baby Leviathan pet");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.shoot = Mod.Find<ModProjectile>("Levi").Type;
            Item.buffType = Mod.Find<ModBuff>("Levi").Type;
			Item.rare = 10;
		}

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }
}