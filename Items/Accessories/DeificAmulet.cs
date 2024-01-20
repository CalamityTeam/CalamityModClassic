using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class DeificAmulet : ModItem
{
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.rare = ItemRarityID.Yellow;
		Item.value = 1500000;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.dAmulet = true;
		player.panic = true;
		player.GetArmorPenetration(DamageClass.Generic) += 25;
		player.manaMagnet = true;
		player.magicCuffs = true;
		if (player.wet)
		{
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 1.35f, 0.3f, 0.9f);
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.CelestialCuffs);
		recipe.AddIngredient(ItemID.JellyfishNecklace);
		recipe.AddIngredient(ItemID.PanicNecklace);
		recipe.AddIngredient(ItemID.SharkToothNecklace);
		recipe.AddIngredient(ItemID.StarVeil);
		recipe.AddIngredient(null, "Stardust", 25);
		recipe.AddIngredient(ItemID.MeteoriteBar, 25);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}