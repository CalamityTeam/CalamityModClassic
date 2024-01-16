using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.HiveMind {
public class RottenBrain : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Rotten Brain");
		////Tooltip.SetDefault("Increase to all damage types ranging from 1% to 25% as health drops\nDecrease to movement speed ranging from 1% to 10% as health drops\nShade rains down when you are hit");
		Item.width = 34;
		Item.height = 34;
		Item.value = 100000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.immune)
		{
			if (Main.rand.Next(8) == 0)
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
					int num19 = Projectile.NewProjectile(player.GetSource_FromThis(), x, y, num15, num16, Mod.Find<ModProjectile>("AuraRain").Type, 18, 2f, player.whoAmI, 0f, 0f);
					Main.projectile[num19].ai[1] = player.position.Y;
					Main.projectile[num19].tileCollide = false;
				}
			}
		}
		if(player.statLife <= (player.statLifeMax2 * 0.9f) && player.statLife > (player.statLifeMax2 * 0.8f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.01f;
        	player.GetDamage(DamageClass.Throwing) *= 1.01f;
	        player.GetDamage(DamageClass.Ranged) *= 1.01f;
    	    player.GetDamage(DamageClass.Magic) *= 1.01f;
    	    player.GetDamage(DamageClass.Summon) *= 1.01f;
    	    player.moveSpeed -= 0.01f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.7f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.02f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.02f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.02f;
    	    player.GetDamage(DamageClass.Magic) *= 1.02f;
    	    player.GetDamage(DamageClass.Summon) *= 1.02f;
    	    player.moveSpeed -= 0.02f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.7f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.04f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.04f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.04f;
    	    player.GetDamage(DamageClass.Magic) *= 1.04f;
    	    player.GetDamage(DamageClass.Summon) *= 1.04f;
    	    player.moveSpeed -= 0.03f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.06f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.06f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.06f;
    	    player.GetDamage(DamageClass.Magic) *= 1.06f;
    	    player.GetDamage(DamageClass.Summon) *= 1.06f;
    	    player.moveSpeed -= 0.04f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.5f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.09f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.09f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.09f;
    	    player.GetDamage(DamageClass.Magic) *= 1.09f;
    	    player.GetDamage(DamageClass.Summon) *= 1.09f;
    	    player.moveSpeed -= 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.3f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.12f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.12f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.12f;
    	    player.GetDamage(DamageClass.Magic) *= 1.12f;
    	    player.GetDamage(DamageClass.Summon) *= 1.12f;
    	    player.moveSpeed -= 0.06f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.3f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.16f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.16f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.16f;
    	    player.GetDamage(DamageClass.Magic) *= 1.16f;
    	    player.GetDamage(DamageClass.Summon) *= 1.16f;
    	    player.moveSpeed -= 0.07f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f) && player.statLife > (player.statLifeMax2 * 0.1f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.2f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.2f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.2f;
    	    player.GetDamage(DamageClass.Magic) *= 1.2f;
    	    player.GetDamage(DamageClass.Summon) *= 1.2f;
    	    player.moveSpeed -= 0.08f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.1f))
		{
			player.GetDamage(DamageClass.Melee) *= 1.25f;
    	    player.GetDamage(DamageClass.Throwing) *= 1.25f;
    	    player.GetDamage(DamageClass.Ranged) *= 1.25f;
    	    player.GetDamage(DamageClass.Magic) *= 1.25f;
    	    player.GetDamage(DamageClass.Summon) *= 1.25f;
    	    player.moveSpeed -= 0.1f;
		}
	}
}}