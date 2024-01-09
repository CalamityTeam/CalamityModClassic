using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class ElementalGauntlet : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 22;
		Item.height = 38;
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
		modPlayer.eGauntlet = true;
		player.longInvince = true;
		player.kbGlove = true;
		player.GetDamage(DamageClass.Melee) += 0.2f;
		player.GetCritChance(DamageClass.Melee) += 20;
		player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
		player.lavaImmune = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FireGauntlet);
		recipe.AddIngredient(null, "YharimsInsignia");
		recipe.AddIngredient(null, "Phantoplasm", 20);
		recipe.AddIngredient(null, "NightmareFuel", 20);
		recipe.AddIngredient(null, "EndothermicEnergy", 20);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}