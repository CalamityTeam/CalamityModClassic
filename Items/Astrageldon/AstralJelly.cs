using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.Astrageldon
{
	public class AstralJelly : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aureus Cell");
            /* Tooltip.SetDefault("Gives mana regeneration and magic power for 6 minutes\n" +
                "Restores 200 mana"); */
        }
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
            Item.useAnimation = 17;
			Item.useTime = 17;
            Item.rare = 7;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 4, 50, 0);
			Item.buffType = BuffID.WellFed;
            Item.buffTime = 108000;
        }

        public override void OnConsumeItem(Player player)
        {
            player.statMana += 200;
            if (player.statMana > player.statManaMax2)
            {
                player.statMana = player.statManaMax2;
            }
            player.AddBuff(BuffID.ManaSickness, Player.manaSickTime, true);
            if (Main.myPlayer == player.whoAmI)
            {
                player.ManaEffect(200);
            }
            player.AddBuff(BuffID.ManaRegeneration, 21600);
            player.AddBuff(BuffID.MagicPower, 21600);
            player.AddBuff(BuffID.WellFed, 108000);
        }
    }
}