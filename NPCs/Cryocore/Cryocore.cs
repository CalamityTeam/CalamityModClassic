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

namespace CalamityModClassic1Point2.NPCs.Cryocore
{
	public class Cryocore : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cryocore");
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.damage = 55;
			NPC.width = 58; //324
			NPC.height = 58; //216
			NPC.defense = 5;
			NPC.lifeMax = 200;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0.75f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath15;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("A snowflake in the wind.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld.revenge;
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.01f, 0.25f, 0.25f);
			NPC.TargetClosest(true);
			float num1372 = revenge ? 12f : 11f;
			Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
			float num1373 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector167.X;
			float num1374 = Main.player[NPC.target].Center.Y - vector167.Y;
			float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
			float num1376 = num1372 / num1375;
			num1373 *= num1376;
			num1374 *= num1376;
			NPC.ai[0] -= 1f;
			if (num1375 < 200f || NPC.ai[0] > 0f)
			{
				if (num1375 < 200f)
				{
					NPC.ai[0] = 20f;
				}
				if (NPC.velocity.X < 0f)
				{
					NPC.direction = -1;
				}
				else
				{
					NPC.direction = 1;
				}
				NPC.rotation += (float)NPC.direction * 0.3f;
				return;
			}
			NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
			NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
			if (num1375 < 350f)
			{
				NPC.velocity.X = (NPC.velocity.X * 10f + num1373) / 11f;
				NPC.velocity.Y = (NPC.velocity.Y * 10f + num1374) / 11f;
			}
			if (num1375 < 300f)
			{
				NPC.velocity.X = (NPC.velocity.X * 7f + num1373) / 8f;
				NPC.velocity.Y = (NPC.velocity.Y * 7f + num1374) / 8f;
			}
			NPC.rotation = NPC.velocity.X * 0.15f;
			return;
		}
		
		public override bool PreKill()
		{
			return false;
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceRod, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceRod, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}