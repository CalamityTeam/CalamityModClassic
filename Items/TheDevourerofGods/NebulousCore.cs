﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.TheDevourerofGods {
public class NebulousCore : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Nebulous Core");
		Item.width = 16;
		Item.height = 14;
		Item.maxStack = 1;
		////Tooltip.SetDefault("+50 max mana, 15% reduced mana usage and 15% increased magic damage and magic crit\nStars fall when damaged\nSummons floating nebula stars to protect you");
		Item.value = 513750;
		Item.rare = 7;
		Item.accessory = true;
		Item.expert = true;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.35f * num, 0.05f * num, 0.35f * num);
    }
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 50;
		player.GetDamage(DamageClass.Magic) += 0.15f;
		player.manaCost *= 0.85f;
		player.GetCritChance(DamageClass.Magic) += 15;
		player.starCloakItem = Item;
		int damage = 220;
		float knockBack = 3f;
		if (Main.rand.Next(15) == 0)
		{
			int num = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == Mod.Find<ModProjectile>("NebulaStar").Type)
				{
					num++;
				}
			}
			if (Main.rand.Next(15) >= num && num < 10)
			{
				int num2 = 50;
				int num3 = 24;
				int num4 = 90;
				for (int j = 0; j < num2; j++)
				{
					int num5 = Main.rand.Next(200 - j * 2, 400 + j * 2);
					Vector2 center = player.Center;
					center.X += (float)Main.rand.Next(-num5, num5 + 1);
					center.Y += (float)Main.rand.Next(-num5, num5 + 1);
					if (!Collision.SolidCollision(center, num3, num3) && !Collision.WetCollision(center, num3, num3))
					{
						center.X += (float)(num3 / 2);
						center.Y += (float)(num3 / 2);
						if (Collision.CanHit(new Vector2(player.Center.X, player.position.Y), 1, 1, center, 1, 1) || Collision.CanHit(new Vector2(player.Center.X, player.position.Y - 50f), 1, 1, center, 1, 1))
						{
							int num6 = (int)center.X / 16;
							int num7 = (int)center.Y / 16;
							bool flag = false;
							if (Main.rand.Next(3) == 0 && Main.tile[num6, num7] != null && Main.tile[num6, num7].WallType > 0)
							{
								flag = true;
							}
							else
							{
								center.X -= (float)(num4 / 2);
								center.Y -= (float)(num4 / 2);
								if (Collision.SolidCollision(center, num4, num4))
								{
									center.X += (float)(num4 / 2);
									center.Y += (float)(num4 / 2);
									flag = true;
								}
							}
							if (flag)
							{
								for (int k = 0; k < 1000; k++)
								{
									if (Main.projectile[k].active && Main.projectile[k].owner == player.whoAmI && Main.projectile[k].type == Mod.Find<ModProjectile>("NebulaStar").Type && (center - Main.projectile[k].Center).Length() < 48f)
									{
										flag = false;
										break;
									}
								}
								if (flag && Main.myPlayer == player.whoAmI)
								{
									Projectile.NewProjectile(player.GetSource_FromThis(), center.X, center.Y, 0f, 0f, Mod.Find<ModProjectile>("NebulaStar").Type, damage, knockBack, player.whoAmI, 0f, 0f);
									return;
								}
							}
						}
					}
				}
			}
		}
	}
}}