using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class TyphonsGreedStaff : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Typhon's Greed");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 110;
            Projectile.height = 110;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.hide = true;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }

        public override void AI()
        {
            float num = 50f;
            float num2 = 2f;
            float scaleFactor = 20f;
            Player player = Main.player[Projectile.owner];
            float num3 = -0.7853982f;
            Vector2 value = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 value2 = Vector2.Zero;
            if (player.dead)
            {
                Projectile.Kill();
                return;
            }
            Lighting.AddLight(player.Center, 0f, 0.2f, 1.45f);
            int num9 = Math.Sign(Projectile.velocity.X);
            Projectile.velocity = new Vector2((float)num9, 0f);
            if (Projectile.ai[0] == 0f)
            {
                Projectile.rotation = new Vector2((float)num9, -player.gravDir).ToRotation() + num3 + 3.14159274f;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.rotation -= 1.57079637f;
                }
            }
            Projectile.alpha -= 128;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            float arg_5DB_0 = Projectile.ai[0] / num;
            float num10 = 1f;
            Projectile.ai[0] += num10;
            Projectile.rotation += 6.28318548f * num2 / num * (float)num9;
            bool flag2 = Projectile.ai[0] == (float)((int)(num / 2f));
            if (Projectile.ai[0] >= num || (flag2 && !player.controlUseItem))
            {
                Projectile.Kill();
                player.reuseDelay = 2;
            }
            else if (flag2)
            {
                Vector2 mouseWorld2 = Main.MouseWorld;
                int num11 = (player.DirectionTo(mouseWorld2).X > 0f) ? 1 : -1;
                if ((float)num11 != Projectile.velocity.X)
                {
                    player.ChangeDir(num11);
                    Projectile.velocity = new Vector2((float)num11, 0f);
                    Projectile.netUpdate = true;
                    Projectile.rotation -= 3.14159274f;
                }
            }
            if ((Projectile.ai[0] == num10 || (Projectile.ai[0] == (float)((int)(num / 2f)) && Projectile.active)) && Projectile.owner == Main.myPlayer)
            {
                Vector2 mouseWorld3 = Main.MouseWorld;
                Vector2 mouse = player.DirectionTo(mouseWorld3) * 0f;
                player.DirectionTo(mouse);
            }
            float num12 = Projectile.rotation - 0.7853982f * (float)num9;
            value2 = (num12 + ((num9 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * (Projectile.ai[0] / num) * scaleFactor;
            Vector2 value3 = Projectile.Center + (num12 + ((num9 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * 30f;
            Vector2 vector2 = num12.ToRotationVector2();
            Vector2 value4 = vector2.RotatedBy((double)(1.57079637f * (float)Projectile.spriteDirection), default(Vector2));
            if (Main.rand.Next(2) == 0)
            {
                Dust dust3 = Dust.NewDustDirect(value3 - new Vector2(5f), 10, 10, 33, player.velocity.X, player.velocity.Y, 150, default(Color), 1f);
                dust3.velocity = Projectile.DirectionTo(dust3.position) * 0.1f + dust3.velocity * 0.1f;
            }
            for (int j = 0; j < 4; j++)
            {
                float scaleFactor2 = 1f;
                float scaleFactor3 = 1f;
                switch (j)
                {
                    case 1:
                        scaleFactor3 = -1f;
                        break;
                    case 2:
                        scaleFactor3 = 1.25f;
                        scaleFactor2 = 0.5f;
                        break;
                    case 3:
                        scaleFactor3 = -1.25f;
                        scaleFactor2 = 0.5f;
                        break;
                }
                if (Main.rand.Next(6) != 0)
                {
                    Dust dust4 = Dust.NewDustDirect(Projectile.position, 0, 0, 186, 0f, 0f, 100, default(Color), 1f);
                    dust4.position = Projectile.Center + vector2 * (60f + Main.rand.NextFloat() * 20f) * scaleFactor3;
                    dust4.velocity = value4 * (4f + 4f * Main.rand.NextFloat()) * scaleFactor3 * scaleFactor2;
                    dust4.noGravity = true;
                    dust4.noLight = true;
                    dust4.scale = 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        dust4.noGravity = false;
                    }
                }
            }
            Projectile.position = value - Projectile.Size / 2f;
            Projectile.position += value2;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = MathHelper.WrapAngle(Projectile.rotation);
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= 12f)
            {
                Projectile.localAI[0] = 0f;
                float xPos = (Main.rand.Next(2) == 0 ? Projectile.position.X + 800f : Projectile.position.X - 800f);
                Vector2 vector20 = new Vector2(xPos, Projectile.position.Y + (float)Main.rand.Next(-800, 801));
                float num80 = xPos;
                float speedX = (float)player.position.X - vector20.X;
                float speedY = (float)player.position.Y - vector20.Y;
                float dir = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                dir = 10 / num80;
                speedX *= dir * 150;
                speedY *= dir * 150;
                if (speedX > 15f)
                {
                    speedX = 15f;
                }
                if (speedX < -15f)
                {
                    speedX = -15f;
                }
                if (speedY > 15f)
                {
                    speedY = 15f;
                }
                if (speedY < -15f)
                {
                    speedY = -15f;
                }
                if (Projectile.owner == Main.myPlayer)
                {
                    float ai1 = (Main.rand.NextFloat() + 0.5f);
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector20.X, vector20.Y, speedX, speedY, Mod.Find<ModProjectile>("TyphonsGreed").Type, Projectile.damage, 2f, Projectile.owner, 0.0f, ai1);
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Rectangle myRect = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
            if (Projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (((Main.npc[i].active && !Main.npc[i].dontTakeDamage)) &&
                        ((Projectile.friendly && (!Main.npc[i].friendly || Projectile.type == 318 || (Main.npc[i].type == 22 && Projectile.owner < 255 && Main.player[Projectile.owner].killGuide) || (Main.npc[i].type == 54 && Projectile.owner < 255 && Main.player[Projectile.owner].killClothier))) ||
                        (Projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (Projectile.owner < 0 || Main.npc[i].immune[Projectile.owner] == 0 || Projectile.maxPenetrate == 1))
                    {
                        if (Main.npc[i].noTileCollide || !Projectile.ownerHitCheck)
                        {
                            bool flag3;
                            if (Main.npc[i].type == 414)
                            {
                                Rectangle rect = Main.npc[i].getRect();
                                int num5 = 8;
                                rect.X -= num5;
                                rect.Y -= num5;
                                rect.Width += num5 * 2;
                                rect.Height += num5 * 2;
                                flag3 = Projectile.Colliding(myRect, rect);
                            }
                            else
                            {
                                flag3 = Projectile.Colliding(myRect, Main.npc[i].getRect());
                            }
                            if (flag3)
                            {
                                if (Main.npc[i].reflectsProjectiles && Projectile.CanBeReflected())
                                {
                                    Main.npc[i].ReflectProjectile(Projectile);
                                    return;
                                }
                                modifiers.HitDirectionOverride = ((Main.player[Projectile.owner].Center.X < Main.npc[i].Center.X) ? 1 : -1);
                            }
                        }
                    }
                }
            }
        }

        public override void CutTiles()
        {
            float num5 = 60f;
            float f = Projectile.rotation - 0.7853982f * (float)Math.Sign(Projectile.velocity.X);
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Utils.PlotTileLine(Projectile.Center + f.ToRotationVector2() * -num5, Projectile.Center + f.ToRotationVector2() * num5, (float)Projectile.width * Projectile.scale, DelegateMethods.CutTiles);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float f = Projectile.rotation - 0.7853982f * (float)Math.Sign(Projectile.velocity.X);
            float num2 = 0f;
            float num3 = 110f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center + f.ToRotationVector2() * -num3, Projectile.Center + f.ToRotationVector2() * num3, 23f * Projectile.scale, ref num2))
            {
                return true;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 240);
            target.immune[Projectile.owner] = 6;
        }
    }
}