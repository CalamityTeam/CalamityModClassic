using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class StatisNinjaBelt : ModItem
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
		player.autoJump = true;
		player.jumpSpeedBoost += 3.2f;
		player.extraFall += 35;
		player.blackBelt = true;
		player.dash = 1;
		player.spikedBoots = 2;
		player.GetDamage(DamageClass.Throwing) += 0.15f;
        player.GetCritChance(DamageClass.Throwing) += 10;
        player.ThrownVelocity += 0.15f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FrogLeg);
		recipe.AddIngredient(null, "PurifiedGel", 50);
		recipe.AddIngredient(null, "CoreofEleum");
		recipe.AddIngredient(ItemID.MasterNinjaGear);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}