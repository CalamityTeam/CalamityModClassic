using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class DankCreeper : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dank Creeper");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 74;
            Projectile.height = 74;
            Projectile.scale = 0.75f;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1;
            Projectile.aiStyle = 54;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 317;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
        	if (Projectile.localAI[0] == 0f)
        	{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num226 = 36;
				for (int num227 = 0; num227 < num226; num227++)
				{
					Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
					vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
					Vector2 vector7 = vector6 - Projectile.Center;
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 14, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].noLight = true;
					Main.dust[num228].velocity = vector7;
				}
				Projectile.localAI[0] += 1f;
        	}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("DankCreeper").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.AddBuff(Mod.Find<ModBuff>("DankCreeper").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.dCreeper = false;
				}
				if (modPlayer.dCreeper)
				{
					Projectile.timeLeft = 2;
				}
			}
            int num3;
            for (int num534 = 0; num534 < 1000; num534 = num3 + 1)
            {
                if (num534 != Projectile.whoAmI && Main.projectile[num534].active && Main.projectile[num534].owner == Projectile.owner &&
                    Main.projectile[num534].type == Mod.Find<ModProjectile>("DankCreeper").Type &&
                    Math.Abs(Projectile.position.X - Main.projectile[num534].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num534].position.Y) < (float)Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[num534].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + 0.05f;
                    }
                    if (Projectile.position.Y < Main.projectile[num534].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.05f;
                    }
                }
                num3 = num534;
            }
            float num535 = Projectile.position.X;
            float num536 = Projectile.position.Y;
            float num537 = 1300f;
            bool flag19 = false;
            int num538 = 1100;
            if (Projectile.ai[1] != 0f)
            {
                num538 = 1800;
            }
            if (Math.Abs(Projectile.Center.X - Main.player[Projectile.owner].Center.X) + Math.Abs(Projectile.Center.Y - Main.player[Projectile.owner].Center.Y) > (float)num538)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
				if (player.HasMinionAttackTargetNPC)
				{
					NPC npc = Main.npc[player.MinionAttackTargetNPC];
					if (npc.CanBeChasedBy(Projectile, false))
					{
						float num539 = npc.position.X + (float)(npc.width / 2);
						float num540 = npc.position.Y + (float)(npc.height / 2);
						float num541 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num539) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num540);
						if (num541 < num537 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
						{
							num537 = num541;
							num535 = num539;
							num536 = num540;
							flag19 = true;
						}
					}
				}
				else
				{
					for (int num542 = 0; num542 < 200; num542 = num3 + 1)
                    {
                        if (Main.npc[num542].CanBeChasedBy(Projectile, false))
                        {
                            float num543 = Main.npc[num542].position.X + (float)(Main.npc[num542].width / 2);
                            float num544 = Main.npc[num542].position.Y + (float)(Main.npc[num542].height / 2);
                            float num545 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num543) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num544);
                            if (num545 < num537 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num542].position, Main.npc[num542].width, Main.npc[num542].height))
                            {
                                num537 = num545;
                                num535 = num543;
                                num536 = num544;
                                flag19 = true;
                            }
                        }
                        num3 = num542;
                    }
                }
            }
            if (!flag19)
            {
                float num546 = 8f;
                if (Projectile.ai[0] == 1f)
                {
                    num546 = 12f;
                }
                Vector2 vector42 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num547 = Main.player[Projectile.owner].Center.X - vector42.X;
                float num548 = Main.player[Projectile.owner].Center.Y - vector42.Y - 60f;
                float num549 = (float)Math.Sqrt((double)(num547 * num547 + num548 * num548));
                if (num549 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                }
                if (num549 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.width / 2);
                }
                if (num549 > 70f)
                {
                    num549 = num546 / num549;
                    num547 *= num549;
                    num548 *= num549;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num547) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num548) / 21f;
                }
                else
                {
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X = -0.15f;
                        Projectile.velocity.Y = -0.05f;
                    }
                    Projectile.velocity *= 1.01f;
                }
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                    return;
                }
            }
            else
            {
                if (Projectile.ai[1] == -1f)
                {
                    Projectile.ai[1] = 11f;
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 1f;
                }
                if (Projectile.ai[1] == 0f)
                {
                    float num550 = 8f; //12
                    Vector2 vector43 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num551 = num535 - vector43.X;
                    float num552 = num536 - vector43.Y;
                    float num553 = (float)Math.Sqrt((double)(num551 * num551 + num552 * num552));
                    if (num553 < 100f)
                    {
                        num550 = 10f; //14
                    }
                    num553 = num550 / num553;
                    num551 *= num553;
                    num552 *= num553;
                    Projectile.velocity.X = (Projectile.velocity.X * 14f + num551) / 15f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 14f + num552) / 15f;
                }
                else
                {
                    if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
                    {
                        Projectile.velocity *= 1.05f;
                    }
                }
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                    return;
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
                                Projectile.ai[1] = -1f;
                                Projectile.netUpdate = true;
                            }
                        }
                    }
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}