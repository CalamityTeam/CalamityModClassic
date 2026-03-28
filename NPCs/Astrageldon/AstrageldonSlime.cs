using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Astrageldon
{
	public class AstrageldonSlime : ModNPC
	{
		private bool boostDR = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aureus Spawn");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 90; //324
			NPC.height = 60; //216
			NPC.defense = 0;
			NPC.alpha = 255;
			NPC.lifeMax = Main.expertMode ? 1007 : 1012;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 1002;
			}
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}

		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.6f, 0.25f, 0f);
			NPC.rotation = Math.Abs(NPC.velocity.X) * (float)NPC.direction * 0.04f;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			if (NPC.alpha > 0)
			{
				NPC.alpha -= 5;
				int num;
				for (int num245 = 0; num245 < 10; num245 = num + 1)
				{
					int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), NPC.velocity.X, NPC.velocity.Y, 255, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.5f;
					num = num245;
				}
			}
			NPC.TargetClosest(true);
			if (NPC.life <= 1000 || Main.dayTime)
			{
				NPC.dontTakeDamage = true;
				Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
				Point point15 = NPC.Center.ToTileCoordinates();
				Tile tileSafely = Framing.GetTileSafely(point15);
				bool flag121 = (tileSafely.HasUnactuatedTile && Main.tileSolid[tileSafely.TileType] && !Main.tileSolidTop[tileSafely.TileType] && !TileID.Sets.Platforms[tileSafely.TileType]);
				if (vector.Length() < 60f || flag121)
				{
					NPC.dontTakeDamage = false;
					CheckDead();
					NPC.life = 0;
					return;
				}
				float num1372 = 18f;
				Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
				float num1373 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector167.X;
				float num1374 = Main.player[NPC.target].Center.Y - vector167.Y;
				float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
				float num1376 = num1372 / num1375;
				num1373 *= num1376;
				num1374 *= num1376;
				NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
				NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
				return;
			}
			float num1446 = 7f;
			int num1447 = 480;
			float num244;
			if (NPC.localAI[1] == 1f)
			{
				NPC.localAI[1] = 0f;
				if (Main.rand.Next(4) == 0)
				{
					NPC.ai[0] = (float)num1447;
				}
			}
			Vector2 value53 = NPC.Center + new Vector2((float)(NPC.direction * 20), 6f);
			Vector2 vector251 = Main.player[NPC.target].Center - value53;
			bool flag104 = Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1);
			if (vector251.Length() > 400f || !flag104)
			{
				Vector2 value54 = vector251;
				if (value54.Length() > num1446)
				{
					value54.Normalize();
					value54 *= num1446;
				}
				int num1448 = 30;
				NPC.velocity = (NPC.velocity * (float)(num1448 - 1) + value54) / (float)num1448;
			}
			else
			{
				NPC.velocity *= 0.98f;
			}
			if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
			{
				SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
				int num;
				for (int num1449 = 0; num1449 < 20; num1449 = num + 1)
				{
					int num1450 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, Color.Transparent, 0.6f);
					Dust dust = Main.dust[num1450];
					dust.velocity *= 3f;
					Main.dust[num1450].noGravity = true;
					Main.dust[num1450].scale = 2.5f;
					num = num1449;
				}
				if (NPC.localAI[0] >= 260f)
				{
					NPC.localAI[0] -= 60f;
				}
				NPC.Center = new Vector2(NPC.ai[2] * 16f, NPC.ai[3] * 16f);
				NPC.velocity = Vector2.Zero;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
				for (int num1451 = 0; num1451 < 20; num1451 = num + 1)
				{
					int num1452 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, Color.Transparent, 0.6f);
					Dust dust = Main.dust[num1452];
					dust.velocity *= 3f;
					Main.dust[num1452].noGravity = true;
					Main.dust[num1452].scale = 2.5f;
					num = num1451;
				}
			}
			float[] var_9_48E3C_cp_0 = NPC.ai;
			int var_9_48E3C_cp_1 = 0;
			num244 = var_9_48E3C_cp_0[var_9_48E3C_cp_1];
			var_9_48E3C_cp_0[var_9_48E3C_cp_1] = num244 + 1f;
			if (NPC.ai[0] >= (float)num1447 && Main.netMode != 1)
			{
				NPC.ai[0] = 0f;
				Point point12 = NPC.Center.ToTileCoordinates();
				Point point13 = Main.player[NPC.target].Center.ToTileCoordinates();
				int num1453 = 20;
				int num1454 = 3;
				int num1455 = 10;
				int num1456 = 1;
				int num1457 = 0;
				bool flag106 = false;
				if (vector251.Length() > 2000f)
				{
					flag106 = true;
				}
				while (!flag106 && num1457 < 100)
				{
					num1457++;
					int num1458 = Main.rand.Next(point13.X - num1453, point13.X + num1453 + 1);
					int num1459 = Main.rand.Next(point13.Y - num1453, point13.Y + num1453 + 1);
					if ((num1459 < point13.Y - num1455 || num1459 > point13.Y + num1455 || num1458 < point13.X - num1455 || num1458 > point13.X + num1455) && (num1459 < point12.Y - num1454 || num1459 > point12.Y + num1454 || num1458 < point12.X - num1454 || num1458 > point12.X + num1454) && !Main.tile[num1458, num1459].HasUnactuatedTile)
					{
						bool flag107 = true;
						if (flag107 && (Main.tile[num1458, num1459].LiquidType == LiquidID.Lava))
						{
							flag107 = false;
						}
						if (flag107 && Collision.SolidTiles(num1458 - num1456, num1458 + num1456, num1459 - num1456, num1459 + num1456))
						{
							flag107 = false;
						}
						if (flag107)
						{
							NPC.ai[2] = (float)num1458;
							NPC.ai[3] = (float)num1459;
							break;
						}
					}
				}
				NPC.netUpdate = true;
			}
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			modifiers.SetMaxDamage(1);
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

		public override bool CheckDead()
		{
			SoundEngine.PlaySound(SoundID.Item14, NPC.position);
			NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
			NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
			NPC.damage = CalamityWorldPreTrailer.death ? 225 : 150;
			NPC.width = (NPC.height = 216);
			NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
			NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
			for (int num621 = 0; num621 < 15; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
				Main.dust[num622].noGravity = true;
			}
			for (int num623 = 0; num623 < 30; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1f);
				Main.dust[num624].velocity *= 2f;
				Main.dust[num624].noGravity = true;
			}
			return true;
		}
	}
}