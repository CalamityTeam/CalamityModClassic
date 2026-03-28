using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Patreon
{
    public class MiniGuardianDefense : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Defensive Guardian");
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
    	
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
			Projectile.tileCollide = false;
            Projectile.width = 62;
            Projectile.height = 80;
			Projectile.minionSlots = 0f;
			Projectile.minion = true;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
            Projectile.timeLeft *= 5;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 6;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (player.dead)
            {
                modPlayer.gDefense = false;
            }
            if (modPlayer.gDefense)
            {
                Projectile.timeLeft = 2;
            }
            if (!modPlayer.pArtifact || player.maxMinions < 8)
            {
                Projectile.active = false;
                return;
            }
            float num535 = Projectile.position.X;
            float num536 = Projectile.position.Y;
            float num537 = 3000f;
            bool flag19 = false;
            NPC ownerMinionAttackTargetNPC2 = Projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(Projectile, false))
            {
                float num539 = ownerMinionAttackTargetNPC2.position.X + (float)(ownerMinionAttackTargetNPC2.width / 2);
                float num540 = ownerMinionAttackTargetNPC2.position.Y + (float)(ownerMinionAttackTargetNPC2.height / 2);
                float num541 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num539) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num540);
                if (num541 < num537)
                {
                    num537 = num541;
                    num535 = num539;
                    num536 = num540;
                    flag19 = true;
                }
            }
            if (!flag19)
            {
                int num3;
                for (int num542 = 0; num542 < 200; num542 = num3 + 1)
                {
                    if (Main.npc[num542].CanBeChasedBy(Projectile, false))
                    {
                        float num543 = Main.npc[num542].position.X + (float)(Main.npc[num542].width / 2);
                        float num544 = Main.npc[num542].position.Y + (float)(Main.npc[num542].height / 2);
                        float num545 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num543) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num544);
                        if (num545 < num537)
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
            if (!flag19)
            {
                float num16 = 0.5f;
                Projectile.tileCollide = false;
                int num17 = 100;
                Vector2 vector3 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num18 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector3.X;
                float num19 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector3.Y;
                num19 += (float)Main.rand.Next(-10, 21);
                num18 += (float)Main.rand.Next(-10, 21);
                num18 += (float)(60 * -(float)Main.player[Projectile.owner].direction);
                num19 -= 60f;
                float num20 = (float)Math.Sqrt((double)(num18 * num18 + num19 * num19));
                float num21 = 18f;

                if (num20 < (float)num17 && Main.player[Projectile.owner].velocity.Y == 0f &&
                    Projectile.position.Y + (float)Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height &&
                    !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    if (Projectile.velocity.Y < -6f)
                    {
                        Projectile.velocity.Y = -6f;
                    }
                }
                if (num20 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
                    Projectile.netUpdate = true;
                }
                if (num20 < 50f)
                {
                    if (Math.Abs(Projectile.velocity.X) > 2f || Math.Abs(Projectile.velocity.Y) > 2f)
                    {
                        Projectile.velocity *= 0.90f;
                    }
                    num16 = 0.01f;
                }
                else
                {
                    if (num20 < 100f)
                    {
                        num16 = 0.1f;
                    }
                    if (num20 > 300f)
                    {
                        num16 = 1f;
                    }
                    num20 = num21 / num20;
                    num18 *= num20;
                    num19 *= num20;
                }

                if (Projectile.velocity.X < num18)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num16;
                    if (num16 > 0.05f && Projectile.velocity.X < 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num16;
                    }
                }
                if (Projectile.velocity.X > num18)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num16;
                    if (num16 > 0.05f && Projectile.velocity.X > 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num16;
                    }
                }
                if (Projectile.velocity.Y < num19)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num16;
                    if (num16 > 0.05f && Projectile.velocity.Y < 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num16 * 2f;
                    }
                }
                if (Projectile.velocity.Y > num19)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num16;
                    if (num16 > 0.05f && Projectile.velocity.Y > 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num16 * 2f;
                    }
                }
            }
            else
            {
                if (Projectile.ai[1] == -1f)
                {
                    Projectile.ai[1] = 17f;
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 1f;
                }
                if (Projectile.ai[1] == 0f)
                {
                    float num550 = 24f; //12
                    Vector2 vector43 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num551 = num535 - vector43.X;
                    float num552 = num536 - vector43.Y;
                    float num553 = (float)Math.Sqrt((double)(num551 * num551 + num552 * num552));
                    if (num553 < 100f)
                    {
                        num550 = 28f; //14
                    }
                    if (modPlayer.gOffense)
                    {
                        num550 *= 0.95f;
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
            }
            if ((double)Projectile.velocity.X > 0.25)
            {
                Projectile.direction = -1;
            }
            else if ((double)Projectile.velocity.X < -0.25)
            {
                Projectile.direction = 1;
            }

            if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
            {
                Projectile.spriteDirection = -Projectile.direction;
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
        }
    }
}