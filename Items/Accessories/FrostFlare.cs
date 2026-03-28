using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
	public class FrostFlare : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frost Flare");
			/* Tooltip.SetDefault("All melee attacks and projectiles inflict frostburn\n" +
				"Immunity to frostburn, chilled, and frozen\n" +
				"Resistant to cold attacks and +1 life regen\n" +
				"Being above 75% life grants the player 10% increased damage\n" +
				"Being below 25% life grants the player 10 defense and 15% increased max movement speed and acceleration\n" +
                "Revengeance drop"); */
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 10));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.lifeRegen = 1;
            Item.value = Item.buyPrice(0, 24, 0, 0);
            Item.rare = 5;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.frostFlare = true;
		}
	}
}