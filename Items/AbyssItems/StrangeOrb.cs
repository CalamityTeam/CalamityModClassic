using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.AbyssItems
{
    public class StrangeOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Strange Orb");
            /* Tooltip.SetDefault("Summons a young Siren light pet\n" +
                "Provides a large amount of light while underwater"); */
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WispinaBottle);
            Item.shoot = Mod.Find<ModProjectile>("SirenYoung").Type;
            Item.buffType = Mod.Find<ModBuff>("StrangeOrb").Type;
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