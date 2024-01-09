using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class WulfrumBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Wulfrum Blade");
	}

	public override void SetDefaults()
	{
		Item.width = 46;
		Item.damage = 15;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 20;
		Item.useTurn = true;
		Item.knockBack = 3.75f;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.height = 54;
		Item.value = 20000;
		Item.rare = ItemRarityID.Blue;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "WulfrumShard", 12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
