using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class OrnateShield : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ornate Shield");
		//Tooltip.SetDefault("Increased defense by 8 when below 25% life");
	}
	
	public override void SetDefaults()
	{
		Item.width = 36;
		Item.height = 32;
		Item.value = 150000;
		Item.rare = ItemRarityID.Pink;
		Item.defense = 4;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.statLife <= (player.statLifeMax2 * 0.25f))
		{
			player.statDefense += 8;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VerstaltiteBar", 5);
		recipe.AddIngredient(ItemID.CrystalShard, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}