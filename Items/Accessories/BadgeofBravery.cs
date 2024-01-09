using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class BadgeofBravery : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Badge of Bravery");
		//Tooltip.SetDefault("The lower the health the greater the melee speed");
	}
	
	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 30;
		Item.value = 150000;
		Item.accessory = true;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 200);
            }
        }
    }
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UeliaceBar", 2);
		recipe.AddIngredient(ItemID.FeralClaws);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}