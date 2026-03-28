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
	public class Ectoheart : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ectoheart");
			/* Tooltip.SetDefault("Permanently makes Adrenaline Mode take 5 less seconds to charge\n" +
                "Revengeance drop"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 5));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
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
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override bool CanUseItem(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (modPlayer.adrenalineBoostThree)
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
				modPlayer.adrenalineBoostThree = true;
			}
			return true;
		}
	}
}