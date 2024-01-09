using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class RedtideSword : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Redtide Sword");
	}

	public override void SetDefaults()
	{
		Item.width = 42;
		Item.damage = 23;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.useAnimation = 19;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 19;
		Item.useTurn = true;
		Item.knockBack = 4;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.height = 42;
		Item.maxStack = 1;
		Item.value = 50000;
		Item.rare = ItemRarityID.Green;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
