using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Permafrost
{
	public class IceSentry : ModProjectile
	{
		private bool setDamage = true;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Sentry");
            Main.projFrames[Projectile.type] = 18;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

		public override void SetDefaults()
		{
			Projectile.width = 102;
            Projectile.height = 94;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
		}

		public override void AI()
        {
			if (setDamage)
			{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				setDamage = false;
			}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}

			Projectile.velocity = Vector2.Zero;

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }

            if (Projectile.ai[1] < 300f)
            {
                Projectile.localAI[1] = 1f;
                if (Projectile.frame >= 9)
                    Projectile.frame = 0;
            }
            else
            {
                if (Projectile.frame >= 18)
                    Projectile.frame = 9;

                Projectile.localAI[1]++;
                if (Projectile.localAI[1] > 2)
                {
                    Projectile.localAI[1] = 0;

                    if (Projectile.owner == Main.myPlayer)
                    {
                        Vector2 speed = new Vector2(Main.rand.Next(-1000, 1001), Main.rand.Next(-1000, 1001));
                        speed.Normalize();
                        speed *= 15f;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.Center + speed, speed, Mod.Find<ModProjectile>("FrostShardFriendly").Type, Projectile.damage / 2, Projectile.knockBack / 2, Main.myPlayer);
                    }
                }
            }

            NPC minionAttackTargetNpc = Projectile.OwnerMinionAttackTargetNPC;
            if (minionAttackTargetNpc != null && Projectile.ai[0] != minionAttackTargetNpc.whoAmI && minionAttackTargetNpc.CanBeChasedBy(Projectile) && Collision.CanHit(Projectile.Center, 0, 0, minionAttackTargetNpc.position, minionAttackTargetNpc.width, minionAttackTargetNpc.height))
            {
                //Main.NewText("targeting special target");
                Projectile.ai[0] = minionAttackTargetNpc.whoAmI;
                Projectile.ai[1] = 0f;
                Projectile.localAI[0] = 0f;
                Projectile.netUpdate = true;
            }

            if (Projectile.ai[0] >= 0 && Projectile.ai[0] < 200)
            {
                NPC npc = Main.npc[(int)Projectile.ai[0]];

                bool rememberTarget = npc.CanBeChasedBy(Projectile);
                if (rememberTarget)
                {
                    Projectile.localAI[0]++;

                    if (Projectile.ai[1] < 300f)
                        Projectile.ai[1]++;

                    float delay = 60f - Projectile.ai[1] / 60f * 10f;
                    if (Projectile.localAI[0] > delay)
                    {
                        //Main.NewText("time to attack, delay " + delay.ToString());
                        Projectile.localAI[0] = 0f;

                        rememberTarget = Collision.CanHit(Projectile.Center, 0, 0, npc.position, npc.width, npc.height);
                        if (rememberTarget && Projectile.owner == Main.myPlayer)
                        {
                            //Main.NewText("i attacked");
                            Vector2 speed = npc.Center - Projectile.Center;
                            speed.Normalize();
                            speed *= 8f;
                            if (Projectile.ai[1] >= 300f)
                                speed = speed.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-5, 6))) * 1.5f + npc.velocity / 2f;
                            int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.Center, speed + npc.velocity / 2f, Mod.Find<ModProjectile>("FrostBoltProjectile").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                            if (p != 1000)
                            {
                                Main.projectile[p].minion = true;
                            }
                        }

                        //if (!rememberTarget) Main.NewText("couldn't hit target");
                    }
                }
                
                if (!rememberTarget)
                {
                    //Main.NewText("forgetting target");
                    Projectile.ai[0] = -1f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.localAI[0] = 0f;

                float maxDistance = 1000f;
                int possibleTarget = -1;

                for (int i = 0; i < 200; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(Projectile))
                    {
                        float npcDistance = Projectile.Distance(npc.Center);
                        if (npcDistance < maxDistance)
                        {
                            maxDistance = npcDistance;
                            possibleTarget = i;
                        }
                    }
                }

                if (possibleTarget > 0)
                {
                    //Main.NewText("new target acquired");
                    Projectile.ai[0] = possibleTarget;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}
