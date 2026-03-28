using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Scavenger
{
	public class FlamePillar : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flame Pillar");
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
	            Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 1f;
			NPC.width = 40; //324
			NPC.height = 150; //216
			NPC.defense = 0;
			NPC.lifeMax = 100;
            NPC.alpha = 255;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
            NPC.dontTakeDamage = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
		}

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            bool provy = CalamityWorldPreTrailer.downedProvidence;
            if (!Main.npc[CalamityGlobalNPC.scavenger].active)
            {
                NPC.dontTakeDamage = false;
                NPC.life = 0;
                NPC.HitEffect(NPC.direction, 9999);
                NPC.netUpdate = true;
                return;
            }
            if (NPC.timeLeft < 3000)
            {
                NPC.timeLeft = 3000;
            }
            if (NPC.alpha > 0)
            {
                NPC.alpha -= 3;
                if (NPC.alpha < 0)
                {
                    if (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive)
                    {
                        NPC.damage = 400;
                    }
                    else
                    {
                        NPC.damage = Main.expertMode ? 250 : 180;
                    }
                    NPC.alpha = 0;
                }
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.noTileCollide = false;
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 180f)
                {
                    NPC.ai[0] = 1f;
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                if (NPC.ai[1] >= 0f)
                {
                    NPC.ai[1] -= 1f;
                    NPC.localAI[0] += 1f;
                    float SpeedX = (float)Main.rand.Next(-6, 7);
                    float SpeedY = (float)Main.rand.Next(-12, -8);
                    if (NPC.localAI[0] >= 60f)
                    {
                        if (Main.netMode != 1)
                        {
                            Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                            int damage = Main.expertMode ? 32 : 45;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("RavagerFlame").Type, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
                        }
                        NPC.localAI[0] = 0f;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    NPC.dontTakeDamage = false;
                    NPC.life = 0;
                    NPC.HitEffect(NPC.direction, 9999);
                    NPC.netUpdate = true;
                    return;
                }
            }
            if (NPC.target <= 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            int distanceFromTarget = 8000;
            if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget)
            {
                NPC.TargetClosest(true);
                if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                    return;
                }
            }
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 180;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 30; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 135, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 30; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 1, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}