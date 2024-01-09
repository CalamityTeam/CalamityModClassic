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
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Leviathan
{
	public class AquaticAberration : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Aquatic Aberration");
			Main.npcFrameCount[NPC.type] = 9;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 1f;
			NPC.aiStyle = -1;
			NPC.damage = 80;
			NPC.width = 70; //324
			NPC.height = 40; //216
			NPC.defense = 18;
			NPC.lifeMax = 900;
			NPC.knockBackResist = 0.1f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("idk...")

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
			bool revenge = CalamityWorld.revenge;
			NPC.TargetClosest(false);
			NPC.rotation = NPC.velocity.ToRotation();
			if (Math.Sign(NPC.velocity.X) != 0) 
			{
				NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
			}
			if (NPC.rotation < -1.57079637f) 
			{
				NPC.rotation += 3.14159274f;
			}
			if (NPC.rotation > 1.57079637f) 
			{
				NPC.rotation -= 3.14159274f;
			}
			NPC.spriteDirection = Math.Sign(NPC.velocity.X);
			float num997 = 0.3f;
			float num998 = 8f;
			float scaleFactor3 = 300f;
			float num999 = 800f;
			float num1000 = 60f;
			float num1001 = 5f;
			float scaleFactor4 = 0.8f;
			int num1002 = 0;
			float scaleFactor5 = 10f;
			float num1003 = 30f;
			float num1004 = 150f;
			float num1005 = 60f;
			float num1006 = 0.333333343f;
			float num1007 = 8f;
			bool flag63 = false;
			num1006 *= num1005;
			if (Main.expertMode) 
			{
				num997 *= Main.GameModeInfo.KnockbackToEnemiesMultiplier;
			}
			int num1009 = (NPC.ai[0] == 2f) ? 2 : 1;
			int num1010 = (NPC.ai[0] == 2f) ? 30 : 20;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < num1009) 
				{
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, DustID.Water, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (NPC.ai[0] == 0f) 
			{
				NPC.knockBackResist = num997;
				float scaleFactor6 = num998;
				Vector2 center4 = NPC.Center;
				Vector2 center5 = Main.player[NPC.target].Center;
				Vector2 vector126 = center5 - center4;
				Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
				float num1013 = vector126.Length();
				vector126 = Vector2.Normalize(vector126) * scaleFactor6;
				vector127 = Vector2.Normalize(vector127) * scaleFactor6;
				bool flag64 = Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1);
				if (NPC.ai[3] >= 120f) 
				{
					flag64 = true;
				}
				float num1014 = 8f;
				flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
				if (num1013 > num999 || !flag64) 
				{
					NPC.velocity.X = (NPC.velocity.X * (num1000 - 1f) + vector127.X) / num1000;
					NPC.velocity.Y = (NPC.velocity.Y * (num1000 - 1f) + vector127.Y) / num1000;
					if (!flag64) 
					{
						NPC.ai[3] += 1f;
						if (NPC.ai[3] == 120f) 
						{
							NPC.netUpdate = true;
						}
					} 
					else
					{
						NPC.ai[3] = 0f;
					}
				} 
				else 
				{
					NPC.ai[0] = 1f;
					NPC.ai[2] = vector126.X;
					NPC.ai[3] = vector126.Y;
					NPC.netUpdate = true;
				}
			} 
			else if (NPC.ai[0] == 1f) 
			{
				NPC.knockBackResist = 0f;
				NPC.velocity *= scaleFactor4;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= num1001) 
				{
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
					Vector2 velocity = new Vector2(NPC.ai[2], NPC.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
					velocity.Normalize();
					velocity *= scaleFactor5;
					NPC.velocity = velocity;
				}
			} 
			else if (NPC.ai[0] == 2f) 
			{
				NPC.knockBackResist = 0f;
				float num1016 = num1003;
				NPC.ai[1] += 1f;
				bool flag65 = Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > num1004 && NPC.Center.Y > Main.player[NPC.target].Center.Y;
				if ((NPC.ai[1] >= num1016 && flag65) || NPC.velocity.Length() < num1007) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.velocity /= 2f;
					NPC.netUpdate = true;
					NPC.ai[1] = 45f;
					NPC.ai[0] = 4f;
				} 
				else 
				{
					Vector2 center6 = NPC.Center;
					Vector2 center7 = Main.player[NPC.target].Center;
					Vector2 vec2 = center7 - center6;
					vec2.Normalize();
					if (vec2.HasNaNs()) 
					{
						vec2 = new Vector2((float)NPC.direction, 0f);
					}
					NPC.velocity = (NPC.velocity * (num1005 - 1f) + vec2 * (NPC.velocity.Length() + num1006)) / num1005;
				}
				if (flag63 && Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) 
				{
					NPC.ai[0] = 3f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
				}
			} 
			else if (NPC.ai[0] == 4f) 
			{
				NPC.ai[1] -= 3f;
				if (NPC.ai[1] <= 0f) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
				}
				NPC.velocity *= 0.95f;
			}
			if (flag63 && NPC.ai[0] != 3f && Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) < 64f) 
			{
				NPC.ai[0] = 3f;
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				NPC.netUpdate = true;
			}
			if (NPC.ai[0] == 3f) 
			{
				float damageMult = revenge ? 120f : 80f;
				NPC.position = NPC.Center;
				NPC.width = (NPC.height = 192);
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				NPC.velocity = Vector2.Zero;
				NPC.damage = (int)(damageMult * Main.GameModeInfo.EnemyDamageMultiplier);
				NPC.alpha = 255;
				Lighting.AddLight((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16, 0f, 0.7f, 1.1f);
				for (int num1017 = 0; num1017 < 10; num1017++) 
				{
					int num1018 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num1018].velocity *= 1.4f;
					Main.dust[num1018].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
				}
				for (int num1019 = 0; num1019 < 40; num1019++) 
				{
					int num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[num1020].noGravity = true;
					Main.dust[num1020].velocity *= 2f;
					Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
					Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
					if (Main.rand.NextBool(2)) 
					{
						num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water, 0f, 0f, 100, default(Color), 0.9f);
						Main.dust[num1020].noGravity = true;
						Main.dust[num1020].velocity *= 1.2f;
						Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
						Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
					}
					if (Main.rand.NextBool(4)) 
					{
						num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water, 0f, 0f, 100, default(Color), 0.7f);
						Main.dust[num1020].velocity *= 1.2f;
						Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
						Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
					}
				}
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= 3f) 
				{
					SoundEngine.PlaySound(SoundID.Item14, NPC.position);
					NPC.life = 0;
					NPC.HitEffect(0, 10.0);
					NPC.active = false;
					return;
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Wet, 120, true);
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}