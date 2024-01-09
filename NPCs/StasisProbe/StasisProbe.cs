using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Projectiles;
using Terraria.GameContent.Bestiary;
using CalamityModClassic1Point0.NPCs.TheDevourerofGods;

namespace CalamityModClassic1Point0.NPCs.StasisProbe
{
	public class StasisProbe : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Stasis Probe");
			//DisplayName.SetDefault("Stasis Probe");
			NPC.npcSlots = 1f;
			NPC.damage = 50;
			NPC.width = 30; //324
			NPC.height = 30; //216
			NPC.defense = 40;
			NPC.lifeMax = 500;
			NPC.knockBackResist = 0.8f;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound= SoundID.NPCHit4;
			NPC.DeathSound= SoundID.NPCDeath14;
			NPC.buffImmune[24] = true;
		}
		
		public override void AI()
		{
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
				{
					NPC.TargetClosest(true);
				}
				float num288 = 6f;
				float num289 = 0.05f;
				Vector2 vector35 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num291 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
				float num292 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
				num291 = (float)((int)(num291 / 8f) * 8);
				num292 = (float)((int)(num292 / 8f) * 8);
				vector35.X = (float)((int)(vector35.X / 8f) * 8);
				vector35.Y = (float)((int)(vector35.Y / 8f) * 8);
				num291 -= vector35.X;
				num292 -= vector35.Y;
				float num293 = (float)Math.Sqrt((double)(num291 * num291 + num292 * num292));
				float num294 = num293;
				bool flag32 = false;
				if (num293 > 600f)
				{
					flag32 = true;
				}
				if (num293 == 0f)
				{
					num291 = NPC.velocity.X;
					num292 = NPC.velocity.Y;
				}
				else
				{
					num293 = num288 / num293;
					num291 *= num293;
					num292 *= num293;
				}
				if (num294 > 100f)
				{
					NPC.ai[0] += 1f;
					if (NPC.ai[0] > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.023f;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.023f;
					}
					if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
					{
						NPC.velocity.X = NPC.velocity.X + 0.023f;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X - 0.023f;
					}
					if (NPC.ai[0] > 200f)
					{
						NPC.ai[0] = -200f;
					}
				}
				if (Main.player[NPC.target].dead)
				{
					num291 = (float)NPC.direction * num288 / 2f;
					num292 = -num288 / 2f;
				}
				if (NPC.velocity.X < num291)
				{
					NPC.velocity.X = NPC.velocity.X + num289;
				}
				else if (NPC.velocity.X > num291)
				{
					NPC.velocity.X = NPC.velocity.X - num289;
				}
				if (NPC.velocity.Y < num292)
				{
					NPC.velocity.Y = NPC.velocity.Y + num289;
				}
				else if (NPC.velocity.Y > num292)
				{
					NPC.velocity.Y = NPC.velocity.Y - num289;
				}
				NPC.localAI[0] += 1f;
				if (NPC.justHit)
				{
					NPC.localAI[0] = 0f;
				}
				if (Main.netMode != 1 && NPC.localAI[0] >= 120f)
				{
					NPC.localAI[0] = 0f;
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						int num295 = 25;
						if (Main.expertMode)
						{
							num295 = 22;
						}
						if (Main.dayTime)
						{
							num295 = 15;
						}
						int num296 = 84;
						Projectile.NewProjectile(NPC.GetSource_FromAI(), vector35.X, vector35.Y, num291, num292, num296, num295, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				int num297 = (int)NPC.position.X + NPC.width / 2;
				int num298 = (int)NPC.position.Y + NPC.height / 2;
				num297 /= 16;
				num298 /= 16;
				if (!WorldGen.SolidTile(num297, num298))
				{
					Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.25f, 0.01f, 0.25f);
				}
				if (num291 > 0f)
				{
					NPC.spriteDirection = 1;
					NPC.rotation = (float)Math.Atan2((double)num292, (double)num291);
				}
				if (num291 < 0f)
				{
					NPC.spriteDirection = -1;
					NPC.rotation = (float)Math.Atan2((double)num292, (double)num291) + 3.14f;
				}
				else
				{
					NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
				}
				float num299 = 0.7f;
				if (NPC.collideX)
				{
					NPC.netUpdate = true;
					NPC.velocity.X = NPC.oldVelocity.X * -num299;
					if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
					{
						NPC.velocity.X = 2f;
					}
					if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
					{
						NPC.velocity.X = -2f;
					}
				}
				if (NPC.collideY)
				{
					NPC.netUpdate = true;
					NPC.velocity.Y = NPC.oldVelocity.Y * -num299;
					if (NPC.velocity.Y > 0f && (double)NPC.velocity.Y < 1.5)
					{
						NPC.velocity.Y = 2f;
					}
					if (NPC.velocity.Y < 0f && (double)NPC.velocity.Y > -1.5)
					{
						NPC.velocity.Y = -2f;
					}
				}
				else if (Main.rand.Next(40) == 0)
				{
					int num303 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), 5, NPC.velocity.X, 2f, 0, default(Color), 1f);
					Dust expr_138B5_cp_0 = Main.dust[num303];
					expr_138B5_cp_0.velocity.X = expr_138B5_cp_0.velocity.X * 0.5f;
					Dust expr_138D5_cp_0 = Main.dust[num303];
					expr_138D5_cp_0.velocity.Y = expr_138D5_cp_0.velocity.Y * 0.1f;
				}
				if (flag32)
				{
					if ((NPC.velocity.X > 0f && num291 > 0f) || (NPC.velocity.X < 0f && num291 < 0f))
					{
						if (Math.Abs(NPC.velocity.X) < 12f)
						{
							NPC.velocity.X = NPC.velocity.X * 1.05f;
						}
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
					}
				}
				if (Main.player[NPC.target].dead)
				{
					NPC.velocity.Y = NPC.velocity.Y - num289 * 2f;
					if (NPC.timeLeft > 10)
					{
						NPC.timeLeft = 10;
					}
				}
				if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
				{
					NPC.netUpdate = true;
					return;
				}
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            int associatedNPCType = ModContent.NPCType<DevourerofGodsHead>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Probes detatched from the cosmic serpent. They rapidly fire lasers at targets and stunlock them with freezing if they aren't prepared.")
           });
        }

        public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.5f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Frozen, 120, true);
			}
			else
			{
				target.AddBuff(BuffID.Frozen, 60, true);
			}
		}
	}
}