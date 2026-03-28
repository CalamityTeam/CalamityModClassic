using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class T1000 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("T1000");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Player player = Main.player[Projectile.owner];
			float num = 1.57079637f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			if (Projectile.type == Mod.Find<ModProjectile>("T1000").Type)
			{
				Projectile.ai[0] += 1f;
				int num2 = 0;
				if (Projectile.ai[0] >= 20f)
				{
					num2++;
				}
				if (Projectile.ai[0] >= 40f)
				{
					num2++;
				}
				if (Projectile.ai[0] >= 60f)
				{
					num2++;
				}
				int num3 = 24;
				int num4 = 6;
				Projectile.ai[1] += 1f;
				bool flag = false;
				if (Projectile.ai[1] >= (float)(num3 - num4 * num2))
				{
					Projectile.ai[1] = 0f;
					flag = true;
				}
				if (Projectile.soundDelay <= 0)
				{
					Projectile.soundDelay = num3 - num4 * num2;
					if (Projectile.ai[0] != 1f)
					{
						SoundEngine.PlaySound(SoundID.Item91, Projectile.position);
					}
				}
				if (Projectile.ai[1] == 1f && Projectile.ai[0] != 1f)
				{
					Vector2 vector2 = Vector2.UnitX * 24f;
					vector2 = vector2.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
					Vector2 value = Projectile.Center + vector2;
					for (int i = 0; i < 2; i++)
					{
						int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, 66, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
						Main.dust[num5].velocity *= 0.66f;
						Main.dust[num5].noGravity = true;
						Main.dust[num5].scale = 1.4f;
					}
				}
				if (flag && Main.myPlayer == Projectile.owner)
				{
                    int weaponDamage2 = player.GetWeaponDamage(player.inventory[player.selectedItem]);
                    bool flag2 = player.channel && player.CheckMana(player.inventory[player.selectedItem].mana, true, false) && !player.noItems && !player.CCed;
					if (flag2)
					{
						float scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
						Vector2 value2 = vector;
						Vector2 value3 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - value2;
						if (player.gravDir == -1f)
						{
							value3.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - value2.Y;
						}
						Vector2 vector3 = Vector2.Normalize(value3);
						if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
						{
							vector3 = -Vector2.UnitY;
						}
						vector3 *= scaleFactor;
						if (vector3.X != Projectile.velocity.X || vector3.Y != Projectile.velocity.Y)
						{
							Projectile.netUpdate = true;
						}
						Projectile.velocity = vector3;
						int num6 = Mod.Find<ModProjectile>("T1000Laser").Type;
						float scaleFactor2 = 14f;
						int num7 = 7;
						for (int j = 0; j < 4; j++)
						{
							value2 = Projectile.Center + new Vector2((float)Main.rand.Next(-num7, num7 + 1), (float)Main.rand.Next(-num7, num7 + 1));
							Vector2 spinningpoint = Vector2.Normalize(Projectile.velocity) * scaleFactor2;
							spinningpoint = spinningpoint.RotatedBy(Main.rand.NextDouble() * 0.19634954631328583 - 0.098174773156642914, default(Vector2));
							if (float.IsNaN(spinningpoint.X) || float.IsNaN(spinningpoint.Y))
							{
								spinningpoint = -Vector2.UnitY;
							}
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), value2.X, value2.Y, spinningpoint.X, spinningpoint.Y, num6, weaponDamage2, Projectile.knockBack, Projectile.owner, 0f, 0f);
						}
					}
					else
					{
						Projectile.Kill();
					}
				}
			}
			Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
			Projectile.rotation = Projectile.velocity.ToRotation() + num;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.timeLeft = 2;
			player.ChangeDir(Projectile.direction);
			player.heldProj = Projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
        }
    }
}