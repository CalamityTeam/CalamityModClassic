using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class StaffoftheMechworm : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
		{
			//texture =("CalamityModClassic1Point1/Items/Weapons/10.DevourerofGods/StaffoftheMechworm");
			return true;
		}

		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Staff of the Mechworm");
			Item.damage = 75;
			Item.mana = 15;
			Item.width = 62;
			Item.height = 62;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 1;
			////Tooltip.SetDefault("Summons an aerial mechworm to fight for you\nThe tail will disappear when minion slots are maxed out");
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 2f;
			Item.value = 1250000;
			Item.rare = 10;
			Item.UseSound = SoundID.Item113;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("MechwormHead").Type;
			Item.shootSpeed = 10f;
			Item.buffType = Mod.Find<ModBuff>("Mechworm").Type;
			Item.buffTime = 3600;
			Item.DamageType = DamageClass.Summon;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			damage = (int)(damage * ((player.GetDamage(DamageClass.Summon).Flat * 5 / 3) + ((player.GetDamage(DamageClass.Summon).Flat * 0.46f) * (player.maxMinions - 1))));  //36 +
			int owner = player.whoAmI;
			float num72 = Item.shootSpeed;
			player.itemTime = Item.useTime;
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default(Vector2));
			Vector2 vector3 = Main.MouseWorld - vector2;
			float velX = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float velY = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				velY = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
			}
			float dist = (float)Math.Sqrt((double)(velX * velX + velY * velY));
			if ((float.IsNaN(velX) && float.IsNaN(velY)) || (velX == 0f && velY == 0f))
			{
				velX = (float)player.direction;
				velY = 0f;
				dist = num72;
			} else
			{
				dist = num72 / dist;
			}
			velX *= dist;
			velY *= dist;
			int head = -1;
			int tail = -1;
			//Looking these up every iteration is very costly,
			// so cache the value before staring the loop
			int typeHead = Mod.Find<ModProjectile>("MechwormHead").Type;
			int typeTail = Mod.Find<ModProjectile>("MechwormTail").Type;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == owner)
				{
					if (head == -1 && Main.projectile[i].type == typeHead)
					{
						head = i;
					} 
					else if (tail == -1 && Main.projectile[i].type == typeTail)
					{
						tail = i;
					}
					if (head != -1 && tail != -1)
					{
						break;
					}
				}
			}
			if (head == -1 && tail == -1)
			{
				float num77 = Vector2.Dot(value, vector3);
				if (num77 > 0f)
				{
					player.ChangeDir(1);
				} 
				else
				{
					player.ChangeDir(-1);
				}
				velX = 0f;
				velY = 0f;
				vector2.X = (float)Main.mouseX + Main.screenPosition.X;
				vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
				int curr = Projectile.NewProjectile(source, vector2.X, vector2.Y, velX, velY, Mod.Find<ModProjectile>("MechwormHead").Type, damage, knockback, owner);

				int prev = curr;
				curr = Projectile.NewProjectile(source, vector2.X, vector2.Y, velX, velY, Mod.Find<ModProjectile>("MechwormBody").Type, damage, knockback, owner, (float)prev);

				prev = curr;
				curr = Projectile.NewProjectile(source, vector2.X, vector2.Y, velX, velY, Mod.Find<ModProjectile>("MechwormBody2").Type, damage, knockback, owner, (float)prev);
				Main.projectile[prev].localAI[1] = (float)curr;
				Main.projectile[prev].netUpdate = true;

				prev = curr;
				curr = Projectile.NewProjectile(source, vector2.X, vector2.Y, velX, velY, Mod.Find<ModProjectile>("MechwormTail").Type, damage, knockback, owner, (float)prev);
				Main.projectile[prev].localAI[1] = (float)curr;
				Main.projectile[prev].netUpdate = true;
			} 
			else if (head != -1 && tail != -1)
			{
				int body = Projectile.NewProjectile(source, vector2.X, vector2.Y, velX, velY, Mod.Find<ModProjectile>("MechwormBody").Type, damage, knockback, owner, Main.projectile[tail].ai[0]);
				int back = Projectile.NewProjectile(source, vector2.X, vector2.Y, velX, velY, Mod.Find<ModProjectile>("MechwormBody2").Type, damage, knockback, owner, (float)body);

				Main.projectile[body].localAI[1] = (float)back;
				Main.projectile[body].ai[1] = 1f;
				Main.projectile[body].netUpdate = true;

				Main.projectile[back].localAI[1] = (float)tail;
				Main.projectile[back].netUpdate = true;
				Main.projectile[back].ai[1] = 1f;

				Main.projectile[tail].ai[0] = (float)back;
				Main.projectile[tail].netUpdate = true;
				Main.projectile[tail].ai[1] = 1f;
			}
			return false;
		}
	}
}