using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class AngelTreads : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 36;
		Item.height = 32;
		Item.value = 500000;
		Item.rare = ItemRarityID.Yellow;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.accRunSpeed += 1.975f;
		player.rocketBoots = 3;
		player.moveSpeed += 1.05f;
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