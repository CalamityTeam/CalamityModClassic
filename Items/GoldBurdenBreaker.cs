using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class GoldBurdenBreaker : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/GoldBurdenBreaker");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Burden Breaker");
		////Tooltip.SetDefault("The good time\nGo fast\nWARNING: May have disastrous results");
		Item.width = 28;
		Item.height = 28;
		Item.value = 150000;
		Item.rare = 3;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.velocity.X > 5f)
		{
			player.velocity.X *= 1.025f;
			if (player.velocity.X >= 500f)
			{
				player.velocity.X = 0f;
			}
		}
		else if (player.velocity.X < -5f)
		{
			player.velocity.X *= 1.025f;
			if (player.velocity.X <= -500f)
			{
				player.velocity.X = 0f;
			}
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Bone, 50);
		recipe.AddIngredient(ItemID.GoldBar, 7);
		recipe.AddIngredient(ItemID.SoulofMight);
		recipe.AddIngredient(ItemID.SoulofSight);
		recipe.AddIngredient(ItemID.SoulofFright);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Bone, 50);
		recipe.AddIngredient(ItemID.PlatinumBar, 7);
		recipe.AddIngredient(ItemID.SoulofMight);
		recipe.AddIngredient(ItemID.SoulofSight);
		recipe.AddIngredient(ItemID.SoulofFright);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}