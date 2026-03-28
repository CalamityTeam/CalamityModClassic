using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassicPreTrailer.NPCs.CeaselessVoid
{
	public class DarkEnergy : ModNPC
	{
        public int invinceTime = 120;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark Energy");
			Main.npcFrameCount[NPC.type] = 6;
		}
        
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				new FlavorTextBestiaryInfoElement("A rift in time and space created by Ceaseless Void.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.dontTakeDamage = true;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 68;
			NPC.lifeMax = 6000;
            if (CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0)
            {
                NPC.lifeMax = 24000;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 44000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0.25f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit53;
			NPC.DeathSound = SoundID.NPCDeath44;
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
			bool expertMode = Main.expertMode;
            if (invinceTime > 0)
            {
                invinceTime--;
            }
            else
            {
                NPC.damage = expertMode ? 240 : 120;
                NPC.dontTakeDamage = false;
            }
            Vector2 vectorCenter = NPC.Center;
			Player player = Main.player[NPC.target];
			NPC.TargetClosest(true);
            if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
            {
                NPC.knockBackResist = 0f;
            }
			if (NPC.ai[1] == 0f)
			{
				NPC.scale -= 0.02f;
				NPC.alpha += 30;
				if (NPC.alpha >= 250)
				{
					NPC.alpha = 255;
					NPC.ai[1] = 1f;
				}
			}
			else if (NPC.ai[1] == 1f)
			{
				NPC.scale += 0.02f;
				NPC.alpha -= 30;
				if (NPC.alpha <= 0)
				{
					NPC.alpha = 0;
					NPC.ai[1] = 0f;
				}
			}
			int num1009 = (NPC.ai[0] == 0f) ? 1 : 2;
			int num1010 = (NPC.ai[0] == 0f) ? 60 : 80;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < num1009) 
				{
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, 173, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, -10f);
                    if (NPC.timeLeft > 150)
                    {
                        NPC.timeLeft = 150;
                    }
                    return;
                }
            }
            else if (NPC.timeLeft < 2400)
            {
                NPC.timeLeft = 2400;
            }
            if (NPC.ai[0] == 0f) 
			{
				Vector2 vector96 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num784 = Main.npc[CalamityGlobalNPC.voidBoss].Center.X - vector96.X;
				float num785 = Main.npc[CalamityGlobalNPC.voidBoss].Center.Y - vector96.Y;
				float num786 = (float)Math.Sqrt((double)(num784 * num784 + num785 * num785));
				if (num786 > 90f) 
				{
					num786 = 8f / num786; //8f
					num784 *= num786;
					num785 *= num786;
					NPC.velocity.X = (NPC.velocity.X * 15f + num784) / 16f;
					NPC.velocity.Y = (NPC.velocity.Y * 15f + num785) / 16f;
					return;
				}
				if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < 8f) //8f
				{
					NPC.velocity.Y = NPC.velocity.Y * 1.05f; //1.05f
					NPC.velocity.X = NPC.velocity.X * 1.05f; //1.05f
				}
				if (Main.netMode != 1 && ((expertMode && Main.rand.Next(50) == 0) || Main.rand.Next(100) == 0)) 
				{
					NPC.TargetClosest(true);
					vector96 = new Vector2(NPC.Center.X, NPC.Center.Y);
					num784 = player.Center.X - vector96.X;
					num785 = player.Center.Y - vector96.Y;
					num786 = (float)Math.Sqrt((double)(num784 * num784 + num785 * num785));
					num786 = 8f / num786; //8f
					NPC.velocity.X = num784 * num786;
					NPC.velocity.Y = num785 * num786;
					NPC.ai[0] = 1f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else 
			{
				Vector2 value4 = player.Center - NPC.Center;
				value4.Normalize();
				value4 *= 9f; //9f
				NPC.velocity = (NPC.velocity * 99f + value4) / 100f;
				Vector2 vector97 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num787 = Main.npc[CalamityGlobalNPC.voidBoss].Center.X - vector97.X;
				float num788 = Main.npc[CalamityGlobalNPC.voidBoss].Center.Y - vector97.Y;
				float num789 = (float)Math.Sqrt((double)(num787 * num787 + num788 * num788));
				if (num789 > 1400f) 
				{
					NPC.ai[0] = 0f;
					return;
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}