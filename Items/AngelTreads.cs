using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class AngelTreads : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/AngelTreads");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Angel Treads");
		////Tooltip.SetDefault("Extreme speed!\nGreater mobility on ice\nWater and lava walking\nTemporary immunity to lava\nIncreased wing flight time");
		Item.width = 36;
		Item.height = 32;
		Item.value = 500000;
		Item.rare = 8;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.accRunSpeed = 9.75f;
		player.rocketBoots = 3;
		player.moveSpeed += 0.5f;
		player.iceSkate = true;
		player.waterWalk = true;
		player.fireWalk = true;
		player.lavaMax += 420;
		player.wingTimeMax += 50;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FrostsparkBoots);
		recipe.AddIngredient(ItemID.LavaWaders);
		recipe.AddIngredient(null, "HarpyRing");
		recipe.AddIngredient(null, "EssenceofCinder", 5);
		recipe.AddIngredient(null, "AerialiteBar", 20);
		recipe.AddIngredient(ItemID.SoulofMight);
		recipe.AddIngredient(ItemID.SoulofSight);
		recipe.AddIngredient(ItemID.SoulofFright);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}