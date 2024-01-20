using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class StatisCurse : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/StatisCurse");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Statis' Curse");
		////Tooltip.SetDefault("Increased max minions by 3 and 20% increased minion damage\nIncreased minion knockback\nGrants shadowflame powers to all minions\nMinions make enemies cry on hit");
		Item.width = 28;
		Item.height = 32;
		Item.value = 10000000;
		Item.rare = 10;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point1.shadowMinions = true;
		CalamityPlayer1Point1.tearMinions = true;
		player.GetKnockback(DamageClass.Summon).Base += 2.5f;
		player.GetDamage(DamageClass.Summon) += 0.2f;
		player.maxMinions += 3;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FragmentStardust, 10);
		recipe.AddIngredient(null, "StatisBlessing");
		recipe.AddIngredient(null, "TheFirstShadowflame");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}