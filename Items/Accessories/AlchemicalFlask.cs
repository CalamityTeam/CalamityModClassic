using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class AlchemicalFlask : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 150000;
		Item.rare = ItemRarityID.Yellow;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.alchFlask = true;
		player.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.BottledWater);
		recipe.AddIngredient(ItemID.Bezoar);
		recipe.AddIngredient(null, "PlagueCellCluster", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}