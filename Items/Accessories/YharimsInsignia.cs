using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class YharimsInsignia : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 22;
		Item.height = 38;
		Item.value = 5000000;
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
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.yInsignia = true;
		player.longInvince = true;
		player.kbGlove = true;
		player.GetDamage(DamageClass.Melee) += 0.07f;
		player.GetAttackSpeed(DamageClass.Melee) += 0.07f;
		player.lavaImmune = true;
		if(player.statLife <= (player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetDamage(DamageClass.Ranged) += 0.15f;
			player.GetDamage(DamageClass.Throwing) += 0.15f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
		}
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
		recipe.AddIngredient(ItemID.WarriorEmblem);
		recipe.AddIngredient(null, "NecklaceofVexation");
		recipe.AddIngredient(null, "CoreofCinder", 5);
		recipe.AddIngredient(ItemID.CrossNecklace);
		recipe.AddIngredient(null, "BadgeofBravery");
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}