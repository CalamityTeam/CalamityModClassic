using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Calamitas {
public class CalamityRing : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Void of Calamity");
		//Tooltip.SetDefault("Cursed?\n15% increase to most damage and 10% increase to summon damage\nBrimstone fire rains down while invincibility is active");
	}
	
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 22;
		Item.value = 500000;
		Item.rare = ItemRarityID.Lime;
		Item.accessory = true;
		Item.expert = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Melee) *= 1.15f;
        player.GetDamage(DamageClass.Throwing) *= 1.15f;
        player.GetDamage(DamageClass.Ranged) *= 1.15f;
        player.GetDamage(DamageClass.Magic) *= 1.15f;
        player.GetDamage(DamageClass.Summon) *= 1.1f;
        player.endurance -= 0.3f;
        if (player.whoAmI == Main.myPlayer)
        {
			if (player.immune)
			{
				if (Main.rand.NextBool(8))
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
						int num19 = Projectile.NewProjectile(player.GetSource_FromThis(), x, y, num15, num16, Mod.Find<ModProjectile>("StandingFire").Type, 30, 5f, player.whoAmI, 0f, 0f);
						Main.projectile[num19].ai[1] = player.position.Y;
					}
				}
	        }
        }
	}
}}