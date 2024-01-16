﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class NecklaceofVexation : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/NecklaceofVexation");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Necklace of Vexation");
		////Tooltip.SetDefault("Revenge\n35% increased damage when under 50% life");
		Item.width = 28;
		Item.height = 28;
		Item.value = 150000;
		Item.rare = 6;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.statLife < (player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.35f;
			player.GetDamage(DamageClass.Magic) *= 1.35f;
			player.GetDamage(DamageClass.Ranged) *= 1.35f;
			player.GetDamage(DamageClass.Throwing) *= 1.35f;
			player.GetDamage(DamageClass.Summon) *= 1.35f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "DraedonBar", 2);
		recipe.AddIngredient(ItemID.AvengerEmblem);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}