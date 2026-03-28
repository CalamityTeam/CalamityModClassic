using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class RedLightningContainer : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Red Lightning Container");
			/* Tooltip.SetDefault("Permanently makes Rage Mode do 15% (60% in Death Mode) more damage\n" +
                "Revengeance drop"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
        }
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item122;
			Item.consumable = true;
			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override bool CanUseItem(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (modPlayer.rageBoostThree)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
				CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
				modPlayer.rageBoostThree = true;
			}
			return true;
		}
	}
}