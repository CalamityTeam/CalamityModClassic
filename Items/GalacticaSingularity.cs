using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class GalacticaSingularity : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Galactica Singularity");
		//Tooltip.SetDefault("A shard of the cosmos");
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 19));
	}
		
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 28;
		Item.maxStack = 69;
		Item.value = 500000;
		Item.rare = ItemRarityID.Red;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FragmentSolar, 3);
		recipe.AddIngredient(ItemID.FragmentVortex, 3);
		recipe.AddIngredient(ItemID.FragmentStardust, 3);
		recipe.AddIngredient(ItemID.FragmentNebula, 3);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 1f * num, 0.3f * num, 0.3f * num);
    }
}}