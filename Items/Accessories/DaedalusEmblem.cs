using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class DaedalusEmblem : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Cyan;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.ammoCost80 = true;
		player.lifeRegen += 2;
		player.statDefense += 5;
		player.GetDamage(DamageClass.Ranged) += 0.15f;
		player.GetCritChance(DamageClass.Ranged) += 10;
		player.pickSpeed -= 0.15f;
		player.GetKnockback(DamageClass.Summon).Base += 0.5f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.CelestialStone);
		recipe.AddIngredient(null, "CoreofCalamity");
		recipe.AddIngredient(ItemID.SniperScope);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}