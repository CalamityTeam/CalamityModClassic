using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.HiveMind
{
	public class HiveBlob2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hive Blob");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.1f;
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.width = 25; //324
			NPC.height = 25; //216
			NPC.lifeMax = 75;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 13000;
            }
            AIType = -1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.chaseable = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}
		
		public override void AI()
		{
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorldPreTrailer.revenge;
			if (!Main.npc[CalamityGlobalNPC.hiveMind2].active) 
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
					NPC.StrikeInstantKill();
				NPC.netUpdate = true;
				return;
			}
			int num750 = CalamityGlobalNPC.hiveMind2;
			if (NPC.ai[3] > 0f) 
			{
				num750 = (int)NPC.ai[3] - 1;
			}
			if (Main.netMode != 1) 
			{
				NPC.localAI[0] -= 1f;
				if (NPC.localAI[0] <= 0f) 
				{
					NPC.localAI[0] = (float)Main.rand.Next(120, 480);
					NPC.ai[0] = (float)Main.rand.Next(-100, 101);
					NPC.ai[1] = (float)Main.rand.Next(-100, 101);
					NPC.netUpdate = true;
				}
			}
			NPC.TargetClosest(true);
			float num751 = 0.01f;
			float num752 = 300f;
			if ((double)Main.npc[CalamityGlobalNPC.hiveMind2].life < (double)Main.npc[CalamityGlobalNPC.hiveMind2].lifeMax * 0.25) 
			{
				num752 += 30f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.hiveMind2].life < (double)Main.npc[CalamityGlobalNPC.hiveMind2].lifeMax * 0.1) 
			{
				num752 += 60f;
			}
			if (expertMode) 
			{
				float num753 = 1f - (float)NPC.life / (float)NPC.lifeMax;
				num752 += num753 * 100f;
				num751 += 0.02f;
			}
			if (revenge)
			{
				num751 += 0.1f;
			}
			if (!Main.npc[num750].active || CalamityGlobalNPC.hiveMind2 < 0) 
			{
				NPC.active = false;
				return;
			}
			Vector2 vector22 = new Vector2(NPC.ai[0] * 16f + 8f, NPC.ai[1] * 16f + 8f);
			float num189 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - (float)(NPC.width / 2) - vector22.X;
			float num190 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - (float)(NPC.height / 2) - vector22.Y;
			float num191 = (float)Math.Sqrt((double)(num189 * num189 + num190 * num190));
			float num754 = Main.npc[num750].position.X + (float)(Main.npc[num750].width / 2);
			float num755 = Main.npc[num750].position.Y + (float)(Main.npc[num750].height / 2);
			Vector2 vector93 = new Vector2(num754, num755);
			float num756 = num754 + NPC.ai[0];
			float num757 = num755 + NPC.ai[1];
			float num758 = num756 - vector93.X;
			float num759 = num757 - vector93.Y;
			float num760 = (float)Math.Sqrt((double)(num758 * num758 + num759 * num759));
			num760 = num752 / num760;
			num758 *= num760;
			num759 *= num760;
			if (NPC.position.X < num754 + num758) 
			{
				NPC.velocity.X = NPC.velocity.X + num751;
				if (NPC.velocity.X < 0f && num758 > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
				}
			} 
			else if (NPC.position.X > num754 + num758) 
			{
				NPC.velocity.X = NPC.velocity.X - num751;
				if (NPC.velocity.X > 0f && num758 < 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
				}
			}
			if (NPC.position.Y < num755 + num759) 
			{
				NPC.velocity.Y = NPC.velocity.Y + num751;
				if (NPC.velocity.Y < 0f && num759 > 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.9f;
				}
			} 
			else if (NPC.position.Y > num755 + num759) 
			{
				NPC.velocity.Y = NPC.velocity.Y - num751;
				if (NPC.velocity.Y > 0f && num759 < 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.9f;
				}
			}
			if (NPC.velocity.X > 4f) 
			{
				NPC.velocity.X = 4f;
			}
			if (NPC.velocity.X < -4f) 
			{
				NPC.velocity.X = -4f;
			}
			if (NPC.velocity.Y > 4f) 
			{
				NPC.velocity.Y = 4f;
			}
			if (NPC.velocity.Y < -4f) 
			{
				NPC.velocity.Y = -4f;
			}
			if (Main.netMode != 1)
			{
				if (!Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					NPC.localAI[1] = 180f;
				}
				NPC.localAI[1] += 1f;
				if (NPC.localAI[1] >= 600f)
				{
					NPC.localAI[1] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num941 = revenge ? 6f : 5f; //speed
						if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
						{
							num941 = 7f;
						}
						Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
						float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X;
						float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y;
						float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
						num944 = num941 / num944;
						num942 *= num944;
						num943 *= num944;
						int num945 = expertMode ? 12 : 15;
						int num946 = Mod.Find<ModProjectile>("VileClot").Type;
						vector104.X += num942 * 5f;
						vector104.Y += num943 * 5f;
						int num947 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
						NPC.netUpdate = true;
					}
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.1f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 14, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 14, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}