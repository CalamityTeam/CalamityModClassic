using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class FrigidBulwark : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 44;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Cyan;
		Item.defense = 8;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
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
		if ((double)player.statLife <= (double)player.statLifeMax2 * 0.1)
		{
			player.endurance += 0.05f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.PaladinsShield);
		recipe.AddIngredient(ItemID.FrozenTurtleShell);
		recipe.AddIngredient(null, "CoreofEleum", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}