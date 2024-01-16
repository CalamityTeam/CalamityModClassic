using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class StatisBlessing : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/StatisBlessing");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Statis' Blessing");
		////Tooltip.SetDefault("Increased max minions by 3 and 20% increased minion damage\nIncreased minion knockback\nMinions cause enemies to cry on hit");
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = 9;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer.tearMinions = true;
		player.GetKnockback(DamageClass.Summon).Base += 2.5f;
		player.GetDamage(DamageClass.Summon) += 0.2f;
		player.maxMinions += 3;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.PapyrusScarab);
		recipe.AddIngredient(ItemID.PygmyNecklace);
		recipe.AddIngredient(ItemID.SummonerEmblem);
		recipe.AddIngredient(ItemID.BottledWater);
		recipe.AddIngredient(null, "CoreofCinder", 5);
		recipe.AddIngredient(ItemID.HolyWater, 30);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}