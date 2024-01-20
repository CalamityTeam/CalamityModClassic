using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class AmalgamatedBrain : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 34;
		Item.value = 300000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.aBrain = true;
		if (player.immune)
		{
			if (Main.rand.NextBool(8))
			{
				if (player.whoAmI == Main.myPlayer)
        		{
			 	    for (int l = 0; l < 1; l++)
					{
						float x = player.position.X + (float)Main.rand.Next(-400, 400);
						float y = player.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num15 = player.position.X + (float)(player.width / 2) - vector.X;
						float num16 = player.position.Y + (float)(player.height / 2) - vector.Y;
						num15 += (float)Main.rand.Next(-100, 101);
						int num17 = 22;
						float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
						num18 = (float)num17 / num18;
						num15 *= num18;
						num16 *= num18;
						int num19 = Projectile.NewProjectile(player.GetSource_FromThis(), x, y, num15, num16, Mod.Find<ModProjectile>("AuraRain").Type, 60, 2f, player.whoAmI, 0f, 0f);
						Main.projectile[num19].ai[1] = player.position.Y;
						Main.projectile[num19].tileCollide = false;
					}
				}
			}
		}
		player.GetDamage(DamageClass.Melee) += 0.1f;
		player.GetDamage(DamageClass.Magic) += 0.1f;
		player.GetDamage(DamageClass.Ranged) += 0.1f;
		player.GetDamage(DamageClass.Throwing) += 0.1f;
		player.GetDamage(DamageClass.Summon) += 0.1f;
		player.GetCritChance(DamageClass.Melee) += 5;
		player.GetCritChance(DamageClass.Magic) += 5;
		player.GetCritChance(DamageClass.Ranged) += 5;
		player.GetCritChance(DamageClass.Throwing) += 5;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "RottenBrain");
        recipe.AddIngredient(ItemID.BrainOfConfusion);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}