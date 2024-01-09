using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class CoreofCalamity : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Core of Calamity");
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 16));
	}
	
	public override void SetDefaults()
	{
		Item.width = 40;
		Item.height = 40;
		Item.maxStack = 69;
		Item.value = 500000;
		Item.rare = ItemRarityID.Red;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CoreofCinder", 5);
        recipe.AddIngredient(null, "CoreofEleum", 5);
        recipe.AddIngredient(null, "CoreofChaos", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}