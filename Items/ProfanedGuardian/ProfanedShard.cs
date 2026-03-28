using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.ProfanedGuardian
{
	public class ProfanedShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Profaned Shard");
			/* Tooltip.SetDefault("A shard of the unholy flame\n" +
                "Summons the Profaned Guardians\n" +
                "Can only be used during daytime"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("ProfanedGuardianBoss").Type) && Main.dayTime && (player.ZoneHallow || player.ZoneUnderworldHeight);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("ProfanedGuardianBoss").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyEssence", 15);
			recipe.AddIngredient(ItemID.LunarBar, 3);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}