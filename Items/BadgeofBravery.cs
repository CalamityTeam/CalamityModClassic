using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class BadgeofBravery : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/BadgeofBravery");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Badge of Bravery");
		////Tooltip.SetDefault("The lower the health the greater the melee speed");
		Item.width = 30;
		Item.height = 30;
		Item.value = 150000;
		Item.rare = 8;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.2f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.GetAttackSpeed(DamageClass.Melee) *= 1.4f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
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
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}