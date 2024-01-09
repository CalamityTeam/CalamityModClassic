using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class ElementalQuiver : ModItem
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
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.eQuiver = true;
		player.GetDamage(DamageClass.Ranged) += 0.2f;
        player.GetCritChance(DamageClass.Throwing) += 20;
        player.ammoCost80 = true;
		player.lifeRegen += 2;
		player.statDefense += 5;
		player.pickSpeed -= 0.15f;
		player.GetKnockback(DamageClass.Summon).Base += 0.5f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.MagicQuiver);
		recipe.AddIngredient(null, "DaedalusEmblem");
		recipe.AddIngredient(null, "Phantoplasm", 20);
		recipe.AddIngredient(null, "NightmareFuel", 20);
		recipe.AddIngredient(null, "EndothermicEnergy", 20);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}