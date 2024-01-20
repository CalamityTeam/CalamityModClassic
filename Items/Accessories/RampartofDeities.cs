using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class RampartofDeities : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 44;
		Item.value = 10000000;
		Item.defense = 12;
		Item.accessory = true;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.dAmulet = true;
		player.panic = true;
		player.GetArmorPenetration(DamageClass.Generic) += 50;
		player.manaMagnet = true;
		player.magicCuffs = true;
		if (player.wet)
		{
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 1.35f, 0.3f, 0.9f);
		}
		player.noKnockback = true;
		if ((float)player.statLife > (float)player.statLifeMax2 * 0.25f)
		{
			player.hasPaladinShield = true;
			if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0)
			{
				int myPlayer = Main.myPlayer;
				if (Main.player[myPlayer].team == player.team && player.team != 0)
				{
					float arg = player.position.X - Main.player[myPlayer].position.X;
					float num3 = player.position.Y - Main.player[myPlayer].position.Y;
					if ((float)Math.Sqrt((double)(arg * arg + num3 * num3)) < 800f)
					{
						Main.player[myPlayer].AddBuff(43, 20, true);
					}
				}
			}
		}
		if ((double)player.statLife <= (double)player.statLifeMax2 * 0.3)
		{
			player.AddBuff(62, 5, true);
		}
		if ((double)player.statLife <= (double)player.statLifeMax2 * 0.15)
		{
			player.endurance += 0.05f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "FrigidBulwark");
		recipe.AddIngredient(null, "DeificAmulet");
		recipe.AddIngredient(null, "CosmiliteBar", 20);
		recipe.AddIngredient(null, "GalacticaSingularity", 5);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}