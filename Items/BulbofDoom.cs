using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class BulbofDoom : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Portable Bulb");
			//Tooltip.SetDefault("Summons Plantera");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = ItemRarityID.Lime;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BarofLife", 3);
			recipe.AddIngredient(null, "LivingShard");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}