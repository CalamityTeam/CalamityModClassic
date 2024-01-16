using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class StatisNinjaBelt : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/StatisNinjaBelt");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Statis' Ninja Belt");
		////Tooltip.SetDefault("Increases jump speed and allows constant jumping\nCan climb walls, dash, and dodge attacks\n20% increased throwing damage and velocity\n15% increased throwing crit chance");
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = 9;
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
		player.GetDamage(DamageClass.Throwing) += 0.20f;
        player.GetCritChance(DamageClass.Throwing) += 15;
        player.ThrownVelocity += 0.20f;
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