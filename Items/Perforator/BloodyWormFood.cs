using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Perforator
{
	public class BloodyWormFood : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Bloody Worm Food");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Summons the crimson flesh eaters");
			Item.rare = 3;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("PerforatorSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneCrimson;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Vertebrae, 9);
			recipe.AddIngredient(ItemID.TissueSample, 5);
			recipe.AddIngredient(ItemID.CrimtaneBar, 2);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}