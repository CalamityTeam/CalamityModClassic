using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class GoldBurdenBreaker : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Burden Breaker");
		//Tooltip.SetDefault("The good time\nGo fast\nWARNING: May have disastrous results");
	}
	
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 28;
		Item.value = 150000;
		Item.rare = ItemRarityID.Orange;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		for (int doom = 0; doom < 200; doom++)
		{
			if (Main.npc[doom].active && Main.npc[doom].boss)
			{
				return;
			}
		}
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