using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.NPCs.Crabulon
{
	public class CrabShroom : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crab Shroom");
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.1f;
			NPC.aiStyle = -1;
			NPC.damage = 35;
			NPC.width = 25; //324
			NPC.height = 25; //216
			NPC.lifeMax = 80;
			AIType = -1;
			NPC.knockBackResist = 0.75f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
                new FlavorTextBestiaryInfoElement("Fungus.")

            });
        }
        public override void AI()
		{
			bool revenge = CalamityWorld1Point2.revenge;
			float speed = revenge ? 1.5f : 1f;
			int sporeDust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueFairy, NPC.velocity.X, NPC.velocity.Y, 255, new Color(0, 80, 255, 80), NPC.scale * 0.6f);
			Main.dust[sporeDust].noGravity = true;
			Main.dust[sporeDust].velocity *= 0.5f;
			Player player = Main.player[NPC.target];
			NPC.velocity.Y = NPC.velocity.Y + 0.02f;
			if (NPC.velocity.Y > speed) 
			{
				NPC.velocity.Y = speed;
			}
			NPC.TargetClosest(true);
			if (NPC.position.X + (float)NPC.width < player.position.X) 
			{
				if (NPC.velocity.X < 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.98f;
				}
				NPC.velocity.X = NPC.velocity.X + 0.1f;
			} 
			else if (NPC.position.X > player.position.X + (float)player.width) 
			{
				if (NPC.velocity.X > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.98f;
				}
				NPC.velocity.X = NPC.velocity.X - 0.1f;
			}
			if (NPC.velocity.X > 5f || NPC.velocity.X < -5f) 
			{
				NPC.velocity.X = NPC.velocity.X * 0.97f;
			}
			NPC.rotation = NPC.velocity.X * 0.1f;
			return;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueFairy, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueFairy, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}