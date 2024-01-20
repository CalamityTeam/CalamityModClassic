using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.CalamityBiomeNPCs
{
	public class SoulSlurper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Soul Slurper");
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.npcSlots = 1f;
			NPC.damage = 65;
			NPC.width = 60; //324
			NPC.height = 40; //216
			NPC.defense = 40;
			NPC.lifeMax = 300;
			NPC.knockBackResist = 0.65f;
			NPC.value = Item.buyPrice(0, 0, 50, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lavaImmune = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			if (CalamityWorld1Point2.downedProvidence)
			{
				NPC.damage = 170;
				NPC.defense = 200;
				NPC.lifeMax = 3000;
				NPC.value = Item.buyPrice(0, 3, 0, 0);
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.BrimstoneCragsBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("A mechanical construct that absorbs souls.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			return spawnInfo.Player.GetModPlayer<CalamityPlayer1Point2>().ZoneCalamity ? 0.25f : 0f;
        }
		
		public override void AI()
		{
			bool provy = CalamityWorld1Point2.downedProvidence;
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			float num = 5f;
			float num2 = 0.07f;
			Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num4 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
			float num5 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
			num4 = (float)((int)(num4 / 8f) * 8);
			num5 = (float)((int)(num5 / 8f) * 8);
			vector.X = (float)((int)(vector.X / 8f) * 8);
			vector.Y = (float)((int)(vector.Y / 8f) * 8);
			num4 -= vector.X;
			num5 -= vector.Y;
			float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
			float num7 = num6;
			bool flag = false;
			if (num6 > 600f)
			{
				flag = true;
			}
			if (num6 == 0f)
			{
				num4 = NPC.velocity.X;
				num5 = NPC.velocity.Y;
			}
			else
			{
				num6 = num / num6;
				num4 *= num6;
				num5 *= num6;
			}
			if (num7 > 100f)
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
				num4 = (float)NPC.direction * num / 2f;
				num5 = -num / 2f;
			}
			if (NPC.velocity.X < num4)
			{
				NPC.velocity.X = NPC.velocity.X + num2;
			}
			else if (NPC.velocity.X > num4)
			{
				NPC.velocity.X = NPC.velocity.X - num2;
			}
			if (NPC.velocity.Y < num5)
			{
				NPC.velocity.Y = NPC.velocity.Y + num2;
			}
			else if (NPC.velocity.Y > num5)
			{
				NPC.velocity.Y = NPC.velocity.Y - num2;
			}
			NPC.localAI[0] += 1f;
			if (NPC.justHit)
			{
				NPC.localAI[0] = 0f;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] >= 120f)
			{
				NPC.localAI[0] = 0f;
				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					int num8 = 40;
					if (Main.expertMode)
					{
						num8 = 32;
					}
					int num9 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vector.X, vector.Y, num4, num5, num9, num8 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
				}
			}
			int num10 = (int)NPC.position.X + NPC.width / 2;
			int num11 = (int)NPC.position.Y + NPC.height / 2;
			num10 /= 16;
			num11 /= 16;
			if (!WorldGen.SolidTile(num10, num11))
			{
				Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.75f, 0f, 0f);
			}
			if (num4 > 0f)
			{
				NPC.spriteDirection = 1;
				NPC.rotation = (float)Math.Atan2((double)num5, (double)num4);
			}
			if (num4 < 0f)
			{
				NPC.spriteDirection = -1;
				NPC.rotation = (float)Math.Atan2((double)num5, (double)num4) + 3.14f;
			}
			float num12 = 0.7f;
			if (NPC.collideX)
			{
				NPC.netUpdate = true;
				NPC.velocity.X = NPC.oldVelocity.X * -num12;
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
				NPC.velocity.Y = NPC.oldVelocity.Y * -num12;
				if (NPC.velocity.Y > 0f && (double)NPC.velocity.Y < 1.5)
				{
					NPC.velocity.Y = 2f;
				}
				if (NPC.velocity.Y < 0f && (double)NPC.velocity.Y > -1.5)
				{
					NPC.velocity.Y = -2f;
				}
			}
			if (flag)
			{
				if ((NPC.velocity.X > 0f && num4 > 0f) || (NPC.velocity.X < 0f && num4 < 0f))
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
			if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
			{
				NPC.netUpdate = true;
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<EssenceofChaos>(), 2, 1, 2));
            npcLoot.Add(ItemDropRule.ByCondition(new ProvidenceDowned(), ModContent.ItemType<Bloodstone>(), 2));
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.LifeDrain, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 20; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.65f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
	}
}