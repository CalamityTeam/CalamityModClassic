using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class SigilofCalamitas : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Cyan;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.findTreasure = true;
		player.pStone = true;
		player.lifeRegen += 1;
		player.statManaMax2 += 100;
		player.GetDamage(DamageClass.Magic) += 0.15f;
		player.manaCost *= 0.85f;
		player.GetCritChance(DamageClass.Magic) += 10;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.CharmofMyths);
		recipe.AddIngredient(ItemID.SorcererEmblem);
		recipe.AddIngredient(ItemID.CrystalShard, 20);
		recipe.AddIngredient(null, "CalamityDust", 5);
		recipe.AddIngredient(null, "CoreofChaos", 5);
		recipe.AddIngredient(ItemID.SpellTome);
		recipe.AddIngredient(null, "ChaosAmulet");
		recipe.AddIngredient(ItemID.UnholyWater, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.CharmofMyths);
		recipe.AddIngredient(ItemID.SorcererEmblem);
		recipe.AddIngredient(ItemID.CrystalShard, 20);
		recipe.AddIngredient(null, "CalamityDust", 5);
		recipe.AddIngredient(null, "CoreofChaos", 5);
		recipe.AddIngredient(ItemID.SpellTome);
		recipe.AddIngredient(null, "ChaosAmulet");
		recipe.AddIngredient(ItemID.BloodWater, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}