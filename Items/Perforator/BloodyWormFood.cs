using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Perforator
{
	public class BloodyWormFood : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bloody Worm Food");
			// Tooltip.SetDefault("Summons the Perforator Hive");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = 3;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneCrimson && !NPC.AnyNPCs(Mod.Find<ModNPC>("PerforatorHive").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("PerforatorHive").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Vertebrae, 9);
			recipe.AddIngredient(null, "BloodSample", 5);
			recipe.AddIngredient(ItemID.CrimtaneBar, 2);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}