using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.CeaselessVoid
{
	public class DarkEnergy : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Dark Energy");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = -1;
			NPC.damage = 190;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 68;
			NPC.lifeMax = 28000;
			NPC.knockBackResist = 0.25f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit53;
			NPC.DeathSound = SoundID.NPCDeath44;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                new FlavorTextBestiaryInfoElement("The void says nothing.")

            });
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
			bool bossBuff = CalamityWorld.demonMode;
			bool superBossBuff = CalamityWorld.onionMode;
			Vector2 vectorCenter = NPC.Center;
			Player player = Main.player[NPC.target];
			NPC.TargetClosest(false);
			if (player.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 3f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 3f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == NPC.aiStyle)
						{
							Main.npc[num957].active = false;
						}
					}
				}
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
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, DustID.ShadowbeamStaff, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (CalamityGlobalNPC.voidBoss < 0) 
			{
				NPC.active = false;
				NPC.netUpdate = true;
				return;
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
				if (Main.netMode != NetmodeID.MultiplayerClient && ((expertMode && Main.rand.NextBool(50)) || Main.rand.NextBool(100))) 
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
				if (num789 > 700f) 
				{
					NPC.ai[0] = 0f;
					return;
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}