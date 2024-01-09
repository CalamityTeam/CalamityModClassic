using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class BadgeofBravery : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Badge of Bravery");
		//Tooltip.SetDefault("The lower the health the greater the melee speed");
		Item.width = 30;
		Item.height = 30;
		Item.value = 150000;
		Item.rare = 6;
		Item.accessory = true;
	}
	
	public override void UpdateEquip(Player player)
	{
		if(player.statLife <= (player.statLifeMax2 * 0.8f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.2f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.4f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.6f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.8f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UeliaceBar", 2);
		recipe.AddIngredient(ItemID.FeralClaws);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}