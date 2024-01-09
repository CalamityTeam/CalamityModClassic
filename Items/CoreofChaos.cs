using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class CoreofChaos : ModItem
{	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.5f * num, 0.3f * num, 0.05f * num);
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Core of Chaos");
		Item.width = 16;
		Item.height = 16;
		Item.maxStack = 999;
		Item.value = 50000;
		Item.rare = 8;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(3);
		recipe.AddIngredient(null, "EssenceofChaos");
        recipe.AddIngredient(ItemID.Ectoplasm, 1);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}