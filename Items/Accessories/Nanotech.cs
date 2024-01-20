using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class Nanotech : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 32;
		Item.value = 10000000;
		Item.accessory = true;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
	{
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }
        }
    }
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.nanotech = true;
		player.GetDamage(DamageClass.Throwing) += 0.2f;
        player.GetCritChance(DamageClass.Throwing) += 15;
        player.ThrownVelocity += 0.2f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.MartianConduitPlating, 250);
		recipe.AddIngredient(ItemID.Nanites, 500);
		recipe.AddIngredient(null, "Phantoplasm", 20);
		recipe.AddIngredient(null, "NightmareFuel", 20);
		recipe.AddIngredient(null, "EndothermicEnergy", 20);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}