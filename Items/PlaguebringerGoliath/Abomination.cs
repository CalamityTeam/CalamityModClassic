using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.PlaguebringerGoliath
{
	public class Abomination : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Abomination");
			//Tooltip.SetDefault("Calls in the airborne jungle abomination");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneJungle && !NPC.AnyNPCs(Mod.Find<ModNPC>("PlaguebringerGoliath").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("PlaguebringerGoliath").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "PlagueCellCluster", 10);
			recipe.AddIngredient(ItemID.IronBar, 3);
			recipe.AddIngredient(ItemID.Stinger, 2);
			recipe.AddIngredient(ItemID.Obsidian, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "PlagueCellCluster", 10);
			recipe.AddIngredient(ItemID.LeadBar, 3);
			recipe.AddIngredient(ItemID.Stinger, 2);
			recipe.AddIngredient(ItemID.Obsidian, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}