using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Polterghast
{
	public class NecroplasmicBeacon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Necroplasmic Beacon");
			/* Tooltip.SetDefault("It's spooky\n" +
                "Summons Polterghast\n" +
                "Not consumable"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneDungeon && !NPC.AnyNPCs(Mod.Find<ModNPC>("Polterghast").Type) && CalamityWorldPreTrailer.downedBossAny;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Polterghast").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Phantoplasm", 100);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}