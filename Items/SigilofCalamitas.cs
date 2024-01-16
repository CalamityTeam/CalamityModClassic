using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class SigilofCalamitas : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/SigilofCalamitas");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Sigil of Calamitas");
		////Tooltip.SetDefault("20% increased magic damage, 15% increased magic crit, and 15% decreased mana usage\n+150 max mana\nIncreases life regen and reveals treasure locations\nAttacks inflict on fire and reduces the cooldown of healing potions");
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = 9;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.findTreasure = true;
		player.magmaStone = true;
		player.pStone = true;
		player.lifeRegen += 1;
		player.statManaMax2 += 150;
		player.GetDamage(DamageClass.Magic) += 0.2f;
		player.manaCost *= 0.85f;
		player.GetCritChance(DamageClass.Magic) += 15;
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