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
    public class Sandnado : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sandnado");
            Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 40;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] == 0f)
            {
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num226 = 36;
                for (int num227 = 0; num227 < num226; num227++)
                {
                    Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
                    vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
                    Vector2 vector7 = vector6 - Projectile.Center;
                    int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 85, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
                    Main.dust[num228].noGravity = true;
                    Main.dust[num228].noLight = true;
                    Main.dust[num228].velocity = vector7;
                }
                Projectile.localAI[1] += 1f;
            }
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("Sandnado").Type;
            Player player = Main.player[Projectile.owner];
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            player.AddBuff(Mod.Find<ModBuff>("Sandnado").Type, 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.sandnado = false;
                }
                if (modPlayer.sandnado)
                {
                    Projectile.timeLeft = 2;
                }
            }
            float num8 = 0.1f;
            float num9 = ((float)Projectile.width * 2f);
            for (int j = 0; j < 1000; j++)
            {
                if (j != Projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == Projectile.owner && Main.projectile[j].type == Projectile.type &&
                    Math.Abs(Projectile.position.X - Main.projectile[j].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[j].position.Y) < num9)
                {
                    if (Projectile.position.X < Main.projectile[j].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num8;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num8;
                    }
                    if (Projectile.position.Y < Main.projectile[j].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num8;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num8;
                    }
                }
            }
            Vector2 vector = Projectile.position;
            float num10 = 400f;
            bool flag = false;
            int num11 = -1;
            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.alpha += 20;
                if (Projectile.alpha > 150)
                {
                    Projectile.alpha = 150;
                }
            }
            else
            {
                Projectile.alpha -= 50;
                if (Projectile.alpha < 60)
                {
                    Projectile.alpha = 60;
                }
            }
            Vector2 center = Main.player[Projectile.owner].Center;
            Vector2 value = new Vector2(0.5f);
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(Projectile, false))
				{
					Vector2 vector2 = npc.position + npc.Size * value;
					float num12 = Vector2.Distance(vector2, center);
					if (((Vector2.Distance(center, vector) > num12 && num12 < num10) || !flag) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
					{
						num10 = num12;
						vector = vector2;
						flag = true;
						num11 = npc.whoAmI;
					}
				}
			}
			else
			{
				for (int k = 0; k < 200; k++)
                {
                    NPC nPC = Main.npc[k];
                    if (nPC.CanBeChasedBy(Projectile, false))
                    {
                        Vector2 vector3 = nPC.position + nPC.Size * value;
                        float num13 = Vector2.Distance(vector3, center);
                        if (((Vector2.Distance(center, vector) > num13 && num13 < num10) || !flag) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
                        {
                            num10 = num13;
                            vector = vector3;
                            flag = true;
                            num11 = k;
                        }
                    }
                }
            }
            int num16 = 500;
            if (flag)
            {
                num16 = 1000;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > (float)num16)
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
            }
            if (flag && Projectile.ai[0] == 0f)
            {
                Vector2 vector4 = vector - Projectile.Center;
                float num17 = vector4.Length();
                vector4.Normalize();
                if (num17 > 400f)
                {
                    float scaleFactor = 2f;
                    vector4 *= scaleFactor;
                    Projectile.velocity = (Projectile.velocity * 20f + vector4) / 21f;
                }
                else
                {
                    Projectile.velocity *= 0.96f;
                }
                if (num17 > 200f)
                {
                    float scaleFactor2 = 6f;
                    vector4 *= scaleFactor2;
                    Projectile.velocity.X = (Projectile.velocity.X * 40f + vector4.X) / 41f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 40f + vector4.Y) / 41f;
                }
                if (Projectile.velocity.Y > -1f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
                }
            }
            else
            {
                if (!Collision.CanHitLine(Projectile.Center, 1, 1, Main.player[Projectile.owner].Center, 1, 1))
                {
                    Projectile.ai[0] = 1f;
                }
                float num21 = 9f;
                Vector2 center2 = Projectile.Center;
                Vector2 vector6 = (player.Center - center2 + new Vector2(0f, -60f)) + new Vector2(0f, 40f);
                float num23 = vector6.Length();
                if (num23 > 200f && num21 < 9f)
                {
                    num21 = 9f;
                }
                if (num23 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (num23 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.width / 2);
                }
                if (Math.Abs(vector6.X) > 40f || Math.Abs(vector6.Y) > 10f)
                {
                    vector6.Normalize();
                    vector6 *= num21;
                    vector6 *= new Vector2(1.25f, 0.65f);
                    Projectile.velocity = (Projectile.velocity * 20f + vector6) / 21f;
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
            }
            Projectile.rotation = Projectile.velocity.X * 0.05f;
            Projectile.frameCounter++;
            int num25 = 2;
            if (Projectile.frameCounter >= 6 * num25)
            {
                Projectile.frameCounter = 0;
            }
            Projectile.frame = Projectile.frameCounter / num25;
            if (Main.rand.Next(5) == 0)
            {
                int num26 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 85, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num26].velocity *= 0.3f;
                Main.dust[num26].noGravity = true;
                Main.dust[num26].noLight = true;
            }
            if (Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = (Projectile.direction = -1);
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = (Projectile.direction = 1);
            }
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += 1f;
                if (Main.rand.Next(3) != 0)
                {
                    Projectile.ai[1] += 1f;
                }
            }
            if (Projectile.ai[1] > 60f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 0f)
            {
                float scaleFactor4 = 14f;
                int num28 = Mod.Find<ModProjectile>("MiniSandShark").Type;
                if (flag)
                {
                    if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                    {
                        if (Projectile.ai[1] == 0f)
                        {
                            Projectile.ai[1] += 1f;
                            if (Main.myPlayer == Projectile.owner)
                            {
                                Vector2 vector9 = vector - Projectile.Center;
                                vector9.Normalize();
                                vector9 *= scaleFactor4;
                                int num32 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector9.X, vector9.Y, num28, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                                Main.projectile[num32].timeLeft = 300;
                                Main.projectile[num32].netUpdate = true;
                                Projectile.netUpdate = true;
                            }
                        }
                    }
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y6 = num214 * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}