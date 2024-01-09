﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class ZergPotion : ModItem
	{
		public override void SetStaticDefaults()
	 	{
	 		//DisplayName.SetDefault("Zerg Potion");
	 		//Tooltip.SetDefault("Boosts spawn rates...a lot...");
	 	}
	
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.rare = ItemRarityID.Orange;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.buffType = Mod.Find<ModBuff>("Zerg").Type;
			Item.buffTime = 36000;
			Item.value = 10000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BattlePotion);
			recipe.AddIngredient(null, "DemonicBoneAsh");
			recipe.AddIngredient(null, "MurkySludge", 2);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}