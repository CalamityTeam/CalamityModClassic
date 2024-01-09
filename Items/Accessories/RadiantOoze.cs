using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class RadiantOoze : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Radiant Ooze");
		//Tooltip.SetDefault("You emit light and regen life more quickly at night");
	}
	
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.value = 50000;
		Item.rare = ItemRarityID.Orange;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!Main.dayTime)
		{
			Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1f, 1f, 0.6f);
			player.lifeRegen += 2;
		}
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MurkySludge", 5);
        recipe.AddIngredient(null, "PurifiedGel", 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}