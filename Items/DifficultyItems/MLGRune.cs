using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items.DifficultyItems
{
	public class MLGRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Demon Trophy");
			/* Tooltip.SetDefault("Boosts spawn rate by 1.25 times\n" +
			                   "Effects cannot be reversed"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 99;
			Item.rare = 1;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item119;
			Item.consumable = true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			CalamityWorldPreTrailer.demonMode = true;
			return true;
		}
	}
}