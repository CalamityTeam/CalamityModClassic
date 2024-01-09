﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class YharimsStimulants : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Yharim's Stimulants");
			//Tooltip.SetDefault("Gives decent buffs to ALL offensive and defensive stats");
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
			Item.buffType = Mod.Find<ModBuff>("YharimPower").Type;
			Item.buffTime = 54000;
			Item.value = 10000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.EndurancePotion);
			recipe.AddIngredient(ItemID.IronskinPotion);
			recipe.AddIngredient(ItemID.SwiftnessPotion);
			recipe.AddIngredient(ItemID.ArcheryPotion);
			recipe.AddIngredient(ItemID.MagicPowerPotion);
			recipe.AddIngredient(ItemID.TitanPotion);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}