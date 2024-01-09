using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Bumblefuck
{
	public class BirbPheromones : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Birb Pheromones");
			//Tooltip.SetDefault("Attracts the bumbling birb");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneJungle && !NPC.AnyNPCs(Mod.Find<ModNPC>("Bumblefuck").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Bumblefuck").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 2);
			recipe.AddIngredient(ItemID.FragmentSolar, 4);
			recipe.AddIngredient(null, "CosmiliteBar");
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}