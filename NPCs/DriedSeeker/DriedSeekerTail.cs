using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Projectiles;

namespace CalamityModClassic1Point0.NPCs.DriedSeeker
{
	public class DriedSeekerTail : ModNPC
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
			//NPC.name = "Dried Seeker Tail");
			//DisplayName.SetDefault("Dried Seeker");
			NPC.npcSlots = 0f;
			NPC.damage = 1;
			NPC.width = 22; //324
			NPC.height = 22; //216
			NPC.defense = 10;
			NPC.lifeMax = 50;
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.netAlways = true;
			NPC.noTileCollide = true;
			NPC.behindTiles = true;
			NPC.dontCountMe = true;
			NPC.HitSound= SoundID.NPCHit1;
			NPC.DeathSound= SoundID.NPCDeath1;
		}
		
		public override void AI()
		{
			if (NPC.ai[3] > 0f)
			{
				NPC.realLife = (int)NPC.ai[3];
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.velocity.Length();
			int num343 = (int)(NPC.position.X / 16f) - 1;
			int num344 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
			int num345 = (int)(NPC.position.Y / 16f) - 1;
			int num346 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
			if (num343 < 0)
			{
				num343 = 0;
			}
			if (num344 > Main.maxTilesX)
			{
				num344 = Main.maxTilesX;
			}
			if (num345 < 0)
			{
				num345 = 0;
			}
			if (num346 > Main.maxTilesY)
			{
				num346 = Main.maxTilesY;
			}
			bool flag33 = false;
			if (!flag33)
			{
				for (int num347 = num343; num347 < num344; num347++)
				{
					for (int num348 = num345; num348 < num346; num348++)
					{
						if (Main.tile[num347, num348] != null && ((Main.tile[num347, num348].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num347, num348].TileType] || (Main.tileSolidTop[(int)Main.tile[num347, num348].TileType] && Main.tile[num347, num348].TileFrameY == 0))) || Main.tile[num347, num348].LiquidAmount > 64))
						{
							Vector2 vector37;
							vector37.X = (float)(num347 * 16);
							vector37.Y = (float)(num348 * 16);
							if (NPC.position.X + (float)NPC.width > vector37.X && NPC.position.X < vector37.X + 16f && NPC.position.Y + (float)NPC.height > vector37.Y && NPC.position.Y < vector37.Y + 16f)
							{
								flag33 = true;
								break;
							}
						}
					}
				}
			}
			if (!flag33)
			{
				NPC.localAI[1] = 1f;
				{
					Rectangle rectangle12 = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
					int num954 = 1000;
					if (NPC.position.Y > Main.player[NPC.target].position.Y)
					{
						for (int num955 = 0; num955 < 255; num955++)
						{
							if (Main.player[num955].active)
							{
								Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - num954, (int)Main.player[num955].position.Y - num954, num954 * 2, num954 * 2);
								if (rectangle12.Intersects(rectangle13))
								{
									break;
								}
							}
						}
					}
				}
			}
			else
			{
				NPC.localAI[1] = 0f;
			}
			float num351 = 5f;
			float num352 = 0.75f;
			Vector2 vector38 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num354 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
			float num355 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
			num354 = (float)((int)(num354 / 16f) * 16);
			num355 = (float)((int)(num355 / 16f) * 16);
			vector38.X = (float)((int)(vector38.X / 16f) * 16);
			vector38.Y = (float)((int)(vector38.Y / 16f) * 16);
			num354 -= vector38.X;
			num355 -= vector38.Y;
			float num367 = (float)Math.Sqrt((double)(num354 * num354 + num355 * num355));
			if (Main.player[NPC.target].dead)
			{
				flag33 = false;
				NPC.velocity.Y = NPC.velocity.Y + 1f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 1f;
					num351 = 32f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == NPC.aiStyle)
						{
							Main.npc[num957].active = false;
						}
					}
				}
			}
			if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
			{
				try
				{
					vector38 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num354 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector38.X;
					num355 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector38.Y;
				}
				catch
				{
				}
				NPC.rotation = (float)Math.Atan2((double)num355, (double)num354) + 1.57f;
				num367 = (float)Math.Sqrt((double)(num354 * num354 + num355 * num355));
				int num368 = NPC.width;
				num367 = (num367 - (float)num368) / num367;
				num354 *= num367;
				num355 *= num367;
				NPC.velocity = Vector2.Zero;
				NPC.position.X = NPC.position.X + num354;
				NPC.position.Y = NPC.position.Y + num355;
				return;
			}
			if (!flag33)
			{
				NPC.TargetClosest(true);
				NPC.velocity.Y = NPC.velocity.Y + 0.11f;
				if (NPC.velocity.Y > num351)
				{
					NPC.velocity.Y = num351;
				}
				if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num351 * 0.4)
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num352 * 1.1f;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X + num352 * 1.1f;
					}
				}
				else if (NPC.velocity.Y == num351)
				{
					if (NPC.velocity.X < num354)
					{
						NPC.velocity.X = NPC.velocity.X + num352;
					}
					else if (NPC.velocity.X > num354)
					{
						NPC.velocity.X = NPC.velocity.X - num352;
					}
				}
				else if (NPC.velocity.Y > 4f)
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num352 * 0.9f;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X - num352 * 0.9f;
					}
				}
			}
			else
			{
				if (NPC.soundDelay == 0)
				{
					float num369 = num367 / 40f;
					if (num369 < 10f)
					{
						num369 = 10f;
					}
					if (num369 > 20f)
					{
						num369 = 20f;
					}
					NPC.soundDelay = (int)num369;
					SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
				}
				num367 = (float)Math.Sqrt((double)(num354 * num354 + num355 * num355));
				float num370 = Math.Abs(num354);
				float num371 = Math.Abs(num355);
				float num372 = num351 / num367;
				num354 *= num372;
				num355 *= num372;
				bool flag37 = false;
				if (!flag37)
				{
					if ((NPC.velocity.X > 0f && num354 > 0f) || (NPC.velocity.X < 0f && num354 < 0f) || (NPC.velocity.Y > 0f && num355 > 0f) || (NPC.velocity.Y < 0f && num355 < 0f))
					{
						if (NPC.velocity.X < num354)
						{
							NPC.velocity.X = NPC.velocity.X + num352;
						}
						else if (NPC.velocity.X > num354)
						{
							NPC.velocity.X = NPC.velocity.X - num352;
						}
						if (NPC.velocity.Y < num355)
						{
							NPC.velocity.Y = NPC.velocity.Y + num352;
						}
						else if (NPC.velocity.Y > num355)
						{
							NPC.velocity.Y = NPC.velocity.Y - num352;
						}
						if ((double)Math.Abs(num355) < (double)num351 * 0.2 && ((NPC.velocity.X > 0f && num354 < 0f) || (NPC.velocity.X < 0f && num354 > 0f)))
						{
							if (NPC.velocity.Y > 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y + num352 * 2f;
							}
							else
							{
								NPC.velocity.Y = NPC.velocity.Y - num352 * 2f;
							}
						}
						if ((double)Math.Abs(num354) < (double)num351 * 0.2 && ((NPC.velocity.Y > 0f && num355 < 0f) || (NPC.velocity.Y < 0f && num355 > 0f)))
						{
							if (NPC.velocity.X > 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num352 * 2f;
							}
							else
							{
								NPC.velocity.X = NPC.velocity.X - num352 * 2f;
							}
						}
					}
					else if (num370 > num371)
					{
						if (NPC.velocity.X < num354)
						{
							NPC.velocity.X = NPC.velocity.X + num352 * 1.1f;
						}
						else if (NPC.velocity.X > num354)
						{
							NPC.velocity.X = NPC.velocity.X - num352 * 1.1f;
						}
						if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num351 * 0.5)
						{
							if (NPC.velocity.Y > 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y + num352;
							}
							else
							{
								NPC.velocity.Y = NPC.velocity.Y - num352;
							}
						}
					}
					else
					{
						if (NPC.velocity.Y < num355)
						{
							NPC.velocity.Y = NPC.velocity.Y + num352 * 1.1f;
						}
						else if (NPC.velocity.Y > num355)
						{
							NPC.velocity.Y = NPC.velocity.Y - num352 * 1.1f;
						}
						if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num351 * 0.5)
						{
							if (NPC.velocity.X > 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num352;
							}
							else
							{
								NPC.velocity.X = NPC.velocity.X - num352;
							}
						}
					}
				}
			}
			NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
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
	}
}