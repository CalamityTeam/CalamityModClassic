using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class GhastlyVisage : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Visage");
			Main.projFrames[Projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.65f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f);
        	Player player = Main.player[Projectile.owner];
			float num = 0f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			if (Projectile.spriteDirection == -1)
			{
				num = 3.14159274f;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
			   Projectile.frame = 0;
			}
			Projectile.ai[0] += 1f;
			int num39 = 0;
			if (Projectile.ai[0] >= 240f)
			{
				num39++;
			}
			if (Projectile.ai[0] >= 480f)
			{
				num39++;
			}
			int num40 = 40;
			int num41 = 2;
			Projectile.ai[1] -= 1f;
			bool flag15 = false;
			if (Projectile.ai[1] <= 0f)
			{
				Projectile.ai[1] = (float)(num40 - num41 * num39);
				flag15 = true;
				int arg_1EF4_0 = (int)Projectile.ai[0] / (num40 - num41 * num39);
			}
			bool flag16 = player.channel && !player.noItems && !player.CCed;
			if (Projectile.localAI[0] > 0f)
			{
				Projectile.localAI[0] -= 1f;
			}
            int manaCost = (int)(20f * player.manaCost);
			if (Projectile.localAI[1] == 0f)
			{
				if (player.statMana < manaCost) 
				{
                    if (player.manaFlower)
                    {
                        player.QuickMana();
                        if (player.statMana >= (int)((float)manaCost))
                        {
                            player.manaRegenDelay = (int)player.maxRegenDelay;
                            player.statMana -= manaCost;
                        }
                        else
                        {
                            Projectile.Kill();
                        }
                    }
                    else
                    {
                        Projectile.Kill();
                    }
                }
				else
				{
                    if (player.statMana >= (int)((float)manaCost))
                    {
                        player.statMana -= manaCost;
                        player.manaRegenDelay = (int)player.maxRegenDelay;
                    }
                }
				Projectile.localAI[1] += 1f;
				SoundEngine.PlaySound(SoundID.Item117, Projectile.position);
			}
			if (Projectile.soundDelay <= 0 && flag16)
			{
                if (player.statMana < manaCost)
                {
                    if (player.manaFlower)
                    {
                        player.QuickMana();
                        if (player.statMana >= (int)((float)manaCost))
                        {
                            player.manaRegenDelay = (int)player.maxRegenDelay;
                            player.statMana -= manaCost;
                        }
                        else
                        {
                            Projectile.Kill();
                        }
                    }
                    else
                    {
                        Projectile.Kill();
                    }
                }
                else
                {
                    if (player.statMana >= (int)((float)manaCost))
                    {
                        player.statMana -= manaCost;
                        player.manaRegenDelay = (int)player.maxRegenDelay;
                    }
                }
                Projectile.soundDelay = num40 - num41 * num39;
				if (Projectile.ai[0] != 1f)
				{
					SoundEngine.PlaySound(SoundID.Item117, Projectile.position);
				}
				Projectile.localAI[0] = 12f;
			}
			if (flag15 && Main.myPlayer == Projectile.owner)
			{
				int num42 = Mod.Find<ModProjectile>("GhastlyBlast").Type;
				float scaleFactor11 = 6f;
				int weaponDamage2 = player.GetWeaponDamage(player.inventory[player.selectedItem]);
				float weaponKnockback2 = player.inventory[player.selectedItem].knockBack;
				if (flag16)
				{
					weaponKnockback2 = player.GetWeaponKnockback(player.inventory[player.selectedItem], weaponKnockback2);
					float scaleFactor12 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
					Vector2 vector19 = vector;
					Vector2 value18 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector19;
					if (player.gravDir == -1f)
					{
						value18.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector19.Y;
					}
					Vector2 value19 = Vector2.Normalize(value18);
					if (float.IsNaN(value19.X) || float.IsNaN(value19.Y))
					{
						value19 = -Vector2.UnitY;
					}
					value19 *= scaleFactor12;
					if (value19.X != Projectile.velocity.X || value19.Y != Projectile.velocity.Y)
					{
						Projectile.netUpdate = true;
					}
					Projectile.velocity = value19 * 0.55f;
					Vector2 vector20 = Vector2.Normalize(Projectile.velocity) * scaleFactor11 * (0.6f + Main.rand.NextFloat() * 0.8f);
					if (float.IsNaN(vector20.X) || float.IsNaN(vector20.Y))
					{
						vector20 = -Vector2.UnitY;
					}
					Vector2 vector21 = vector19 + Utils.RandomVector2(Main.rand, -10f, 10f);
					int num44 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector21.X, vector21.Y, vector20.X, vector20.Y, num42, weaponDamage2, weaponKnockback2, Projectile.owner, 0f, 0f);
				}
				else
				{
					Projectile.Kill();
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