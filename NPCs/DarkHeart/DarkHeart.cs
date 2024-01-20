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

namespace CalamityModClassic1Point2.NPCs.DarkHeart
{
	public class DarkHeart : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Dark Heart");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.damage = 30;
			NPC.width = 32; //324
			NPC.height = 32; //216
			NPC.defense = 7;
			NPC.lifeMax = 100;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0.4f;
			NPC.noGravity = true;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath21;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("A microbial cluster filled with corrosive liquid.")

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
			bool revenge = CalamityWorld1Point2.revenge;
			NPC.TargetClosest(true);
			float num1164 = revenge ? 4.5f : 4f;
			float num1165 = revenge ? 0.8f : 0.75f;
			Vector2 vector133 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num1166 = Main.player[NPC.target].Center.X - vector133.X;
			float num1167 = Main.player[NPC.target].Center.Y - vector133.Y - 200f;
			float num1168 = (float)Math.Sqrt((double)(num1166 * num1166 + num1167 * num1167));
			if (num1168 < 20f)
			{
				num1166 = NPC.velocity.X;
				num1167 = NPC.velocity.Y;
			}
			else
			{
				num1168 = num1164 / num1168;
				num1166 *= num1168;
				num1167 *= num1168;
			}
			if (NPC.velocity.X < num1166)
			{
				NPC.velocity.X = NPC.velocity.X + num1165;
				if (NPC.velocity.X < 0f && num1166 > 0f)
				{
					NPC.velocity.X = NPC.velocity.X + num1165 * 2f;
				}
			}
			else if (NPC.velocity.X > num1166)
			{
				NPC.velocity.X = NPC.velocity.X - num1165;
				if (NPC.velocity.X > 0f && num1166 < 0f)
				{
					NPC.velocity.X = NPC.velocity.X - num1165 * 2f;
				}
			}
			if (NPC.velocity.Y < num1167)
			{
				NPC.velocity.Y = NPC.velocity.Y + num1165;
				if (NPC.velocity.Y < 0f && num1167 > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y + num1165 * 2f;
				}
			}
			else if (NPC.velocity.Y > num1167)
			{
				NPC.velocity.Y = NPC.velocity.Y - num1165;
				if (NPC.velocity.Y > 0f && num1167 < 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y - num1165 * 2f;
				}
			}
			if (NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X && NPC.position.X < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.ai[0] += 4f;
				if (NPC.ai[0] > 16f)
				{
					NPC.ai[0] = 0f;
					int num1169 = (int)(NPC.position.X + 10f + (float)Main.rand.Next(NPC.width - 20));
					int num1170 = (int)(NPC.position.Y + (float)NPC.height + 4f);
					int num184 = 15;
					if (Main.expertMode)
					{
						num184 = 12;
					}
					Projectile.NewProjectile(NPC.GetSource_FromThis(), (float)num1169, (float)num1170, 0f, 5f, Mod.Find<ModProjectile>("ShaderainHostile").Type, num184, 0f, Main.myPlayer, 0f, 0f);
					return;
				}
			}
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
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}