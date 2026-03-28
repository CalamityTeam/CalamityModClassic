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
    public class CharredRelic : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charred Relic");
            // Tooltip.SetDefault("Contains a small amount of brimstone");
        }

        public override void SetDefaults()
        {
            Item.shoot = Mod.Find<ModProjectile>("Brimling").Type;
            Item.buffType = Mod.Find<ModBuff>("BrimlingBuff").Type;
			Item.rare = 4;
			Item.UseSound = SoundID.NPCHit51;
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