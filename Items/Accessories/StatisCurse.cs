using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class StatisCurse : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 32;
		Item.value = 10000000;
		Item.rare = ItemRarityID.Red;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.shadowMinions = true;
		modPlayer.tearMinions = true;
		player.GetKnockback(DamageClass.Summon).Base += 2.5f;
		player.GetDamage(DamageClass.Summon) += 0.15f;
		player.maxMinions += 3;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FragmentStardust, 10);
		recipe.AddIngredient(null, "StatisBlessing");
		recipe.AddIngredient(null, "TheFirstShadowflame");
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}