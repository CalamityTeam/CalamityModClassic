using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class DaedalusEmblem : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/DaedalusEmblem");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Daedalus Emblem");
		////Tooltip.SetDefault("15% increased ranged damage, 15% increased ranged crit, and 20% reduced ammo usage\nIncreases life regen, minion knockback, defense, and pick speed\nIncreased gun view range");
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = 9;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.ammoCost80 = true;
		player.lifeRegen += 2;
		player.statDefense += 5;
		player.GetDamage(DamageClass.Ranged) += 0.15f;
		player.GetCritChance(DamageClass.Ranged) += 15;
		player.pickSpeed -= 0.15f;
		player.GetKnockback(DamageClass.Summon).Base += 0.5f;
		player.scope = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.CelestialStone);
		recipe.AddIngredient(null, "CoreofEleum");
		recipe.AddIngredient(ItemID.SniperScope);
		recipe.AddIngredient(ItemID.SoulofSight, 30);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}