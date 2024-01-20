using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.DesertScourge
{
	public class DesertScourgeBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Desert Scourge");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 20; //70
			NPC.npcSlots = 5f;
			NPC.width = 32; //324
			NPC.height = 36; //216
			NPC.defense = 8;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 5200 : 4000;
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.alpha = 255;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.boss = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.netAlways = true;
			NPC.dontCountMe = true;
			if (Main.expertMode)
			{
				NPC.scale = 1.15f;
			}
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
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
			int wormCount = 5;
			if (NPC.life <= (NPC.lifeMax * 0.75f) && NPC.CountNPCS(Mod.Find<ModNPC>("DriedSeekerHead").Type) < wormCount)
			{
				if (Main.rand.NextBool(10)&& Main.netMode != NetmodeID.MultiplayerClient)
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					int seeker = NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("DriedSeekerHead").Type);
					if (Main.netMode == NetmodeID.Server && seeker < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, seeker, 0f, 0f, 0f, 0, 0, 0);
					}
					NPC.netUpdate = true;
				}
			}
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				float randomSpread = (float)(Main.rand.Next(-100, 100) / 100);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("ScourgeBody").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("ScourgeBody2").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("ScourgeBody3").Type, 1f);
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Bleeding, 200, true);
			}
		}
	}
}