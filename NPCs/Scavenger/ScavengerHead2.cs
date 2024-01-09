using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.Scavenger
{
	public class ScavengerHead2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ravager");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.width = 80; //324
			NPC.height = 64; //216
			NPC.defense = 99999;
			NPC.lifeMax = 5000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			if (CalamityWorld.downedProvidence)
			{
				NPC.damage = 0;
				NPC.defense = 99999;
				NPC.lifeMax = 50000;
			}
		}
		
		public override void AI()
		{
			bool provy = CalamityWorld.downedProvidence;
			bool defenseBoost = false;
			int defenseAdd = 0;
			for (int nPC = 0; nPC < 200; nPC++)
			{
				if (Main.npc[nPC].active && Main.npc[nPC].type == (Mod.Find<ModNPC>("ScavengerBody").Type))
				{
					defenseBoost = true;
					defenseAdd++;
				}
			}
			NPC.defense += defenseAdd * 25;
			if (!defenseBoost)
			{
				NPC.defense = -99999;
			}
			if (CalamityGlobalNPC.scavenger < 0)
            {
                NPC.SimpleStrikeNPC(9999, 0, false, noPlayerInteraction: true);
                return;
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			float num = 9f;
			float num2 = 0.225f;
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
			if ((double)Main.npc[CalamityGlobalNPC.scavenger].life < (double)Main.npc[CalamityGlobalNPC.scavenger].lifeMax * 0.3)
			{
				NPC.localAI[0] += 1f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.scavenger].life < (double)Main.npc[CalamityGlobalNPC.scavenger].lifeMax * 0.1)
			{
				NPC.localAI[0] += 1f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.scavenger].life < (double)Main.npc[CalamityGlobalNPC.scavenger].lifeMax * 0.5)
			{
				NPC.localAI[1] += 1f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.scavenger].life < (double)Main.npc[CalamityGlobalNPC.scavenger].lifeMax * 0.25)
			{
				NPC.localAI[1] += 1f;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] >= 180f)
			{
				NPC.localAI[0] = 0f;
				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					SoundEngine.PlaySound(SoundID.Item33, NPC.position);
					int num8 = 58;
					if (Main.expertMode)
					{
						num8 = 50;
					}
					int num9 = Mod.Find<ModProjectile>("ScavengerLaser").Type;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vector.X, vector.Y, num4, num5, num9, num8 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[1] >= 60f)
			{
				NPC.localAI[1] = 0f;
				if (!Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					int num8 = 68;
					if (Main.expertMode)
					{
						num8 = 60;
					}
					int num9 = 259;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vector.X, vector.Y, num4, num5, num9, num8 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
				}
			}
			int num10 = (int)NPC.position.X + NPC.width / 2;
			int num11 = (int)NPC.position.Y + NPC.height / 2;
			num10 /= 16;
			num11 /= 16;
			if (!WorldGen.SolidTile(num10, num11))
			{
				Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.3f, 0f, 0.25f);
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
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
			NPC.damage = 0;
		}
	}
}