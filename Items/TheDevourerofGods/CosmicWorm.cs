using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.TheDevourerofGods
{
	public class CosmicWorm : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Cosmic Worm");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Summons the devourer of the cosmos");
			Item.rare = 10;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("DoGSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofMight, 7);
			recipe.AddIngredient(null, "ArmoredShell", 3);
			recipe.AddIngredient(null, "TwistingNether");
			recipe.AddIngredient(null, "DarkPlasma");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}