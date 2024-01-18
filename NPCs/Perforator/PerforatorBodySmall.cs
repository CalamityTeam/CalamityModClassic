﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;

namespace CalamityModClassic1Point1.NPCs.Perforator
{
	public class PerforatorBodySmall : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
        public override void SetDefaults()
		{
			//NPC.name = "PerforatorBodySmall");
			//Tooltip.SetDefault("The Perforator");
			NPC.damage = 33; //70
			NPC.npcSlots = 5f;
			NPC.width = 32; //324
			NPC.height = 32; //216
			NPC.defense = 8;
			NPC.lifeMax = 2500; //250000
			NPC.aiStyle = 6; //new
			Main.npcFrameCount[NPC.type] = 1; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 0.9f;
			NPC.alpha = 255;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.netAlways = true;
			NPC.dontCountMe = true;
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
			bool expertMode = Main.expertMode;
			if (Main.netMode != 1)
			{
				NPC.localAI[0] += (float)Main.rand.Next(4);
				if (NPC.localAI[0] >= (float)Main.rand.Next(1500, 6000))
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num941 = 8f;
						Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
						float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
						float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
						float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
						num944 = num941 / num944;
						num942 *= num944;
						num943 *= num944;
						num942 += (float)Main.rand.Next(-50, 51) * 0.05f;
						num943 += (float)Main.rand.Next(-50, 51) * 0.05f;
						int num945 = expertMode ? 9 : 16;
						int num946 = Mod.Find<ModProjectile>("BloodClot").Type;
						vector104.X += num942 * 5f;
						vector104.Y += num943 * 5f;
						int num947 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num947].timeLeft = 160;
						NPC.netUpdate = true;
					}
				}
			}
			if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
			if (Main.npc[(int)NPC.ai[1]].alpha < 128)
			{
				NPC.alpha -= 42;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			float targetX = NPC.Center.X;
			float targetY = NPC.Center.Y;
			if (Main.rand.Next(10) == 0 && NPC.life <= 0)
			{
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("PerforatorHeadMini").Type, 0, NPC.whoAmI, targetX, targetY);
			}
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
	}
}