using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point0.NPCs.DesertScourge
{
	public class DesertScourgeHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Scale = 0.8f,
                PortraitScale = 0.8f,
                CustomTexturePath = "CalamityModClassic1Point0/NPCs/DesertScourge/Bestiary",
                PortraitPositionXOverride = 40,
                PortraitPositionYOverride = 20
            };
            value.Position.X += 40;
            value.Position.Y += 25;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
        public override void SetDefaults()
		{
			NPC.damage = 25;
			NPC.npcSlots = 5f;
			NPC.width = 38; //324
			NPC.height = 38; //216
			NPC.defense = 0;
			NPC.lifeMax = 1625;
			NPC.scale = 1.25f;
			NPC.knockBackResist = 0f;
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 1, 0, 0);
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.Boss1;
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
			if (Main.netMode != 1)
			{
				if (NPC.ai[0] == 0f)
				{
					NPC.ai[3] = (float)NPC.whoAmI;
					NPC.realLife = NPC.whoAmI;
					int num936 = NPC.whoAmI;
					int num937 = 20;
					for (int num938 = 0; num938 <= num937; num938++)
					{
						int num939 = Mod.Find<ModNPC>("DesertScourgeBody").Type;
						if (num938 == num937)
						{
							num939 = Mod.Find<ModNPC>("DesertScourgeTail").Type;
						}
						int num940 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), num939, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
						Main.npc[num940].ai[3] = (float)NPC.whoAmI;
						Main.npc[num940].realLife = NPC.whoAmI;
						Main.npc[num940].ai[1] = (float)num936;
						Main.npc[num936].ai[0] = (float)num940;
						NetMessage.SendData(23, -1, -1);
						num936 = num940;
					}
				}
			}
			if (Main.expertMode)
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += (float)Main.rand.Next(4);
					if (NPC.localAI[0] >= (float)Main.rand.Next(1400, 5000))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float num941 = 8f;
							Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
							float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
							float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
							float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
							num944 = num941 / num944;
							num942 *= num944;
							num943 *= num944;
							num942 += (float)Main.rand.Next(-20, 21) * 0.05f;
							num943 += (float)Main.rand.Next(-20, 21) * 0.05f;
							int num945 = 22;
							if (Main.expertMode)
							{
								num945 = 18;
							}
							int num946 = 572;
							vector104.X += num942 * 5f;
							vector104.Y += num943 * 5f;
							int num947 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num947].timeLeft = 300;
							NPC.netUpdate = true;
						}
					}	
				}
			}
			int num948 = (int)(NPC.position.X / 16f) - 1;
			int num949 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
			int num950 = (int)(NPC.position.Y / 16f) - 1;
			int num951 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
			if (num948 < 0)
			{
				num948 = 0;
			}
			if (num949 > Main.maxTilesX)
			{
				num949 = Main.maxTilesX;
			}
			if (num950 < 0)
			{
				num950 = 0;
			}
			if (num951 > Main.maxTilesY)
			{
				num951 = Main.maxTilesY;
			}
			bool flag94 = false;
			if (!flag94)
			{
				for (int num952 = num948; num952 < num949; num952++)
				{
					for (int num953 = num950; num953 < num951; num953++)
					{
						if (Main.tile[num952, num953] != null && ((Main.tile[num952, num953].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num952, num953].TileType] || (Main.tileSolidTop[(int)Main.tile[num952, num953].TileType] && Main.tile[num952, num953].TileFrameY == 0))) || Main.tile[num952, num953].LiquidAmount > 64))
						{
							Vector2 vector105;
							vector105.X = (float)(num952 * 16);
							vector105.Y = (float)(num953 * 16);
							if (NPC.position.X + (float)NPC.width > vector105.X && NPC.position.X < vector105.X + 16f && NPC.position.Y + (float)NPC.height > vector105.Y && NPC.position.Y < vector105.Y + 16f)
							{
								flag94 = true;
								break;
							}
						}
					}
				}
			}
			if (!flag94)
			{
				NPC.localAI[1] = 1f;
				{
					Rectangle rectangle12 = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
					int num954 = 1000;
					bool flag95 = true;
					if (NPC.position.Y > Main.player[NPC.target].position.Y)
					{
						for (int num955 = 0; num955 < 255; num955++)
						{
							if (Main.player[num955].active)
							{
								Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - num954, (int)Main.player[num955].position.Y - num954, num954 * 2, num954 * 2);
								if (rectangle12.Intersects(rectangle13))
								{
									flag95 = false;
									break;
								}
							}
						}
						if (flag95)
						{
							flag94 = true;
						}
					}
				}
			}
			else
			{
				NPC.localAI[1] = 0f;
			}
			float num956 = 16f;
			if (Main.player[NPC.target].dead)
			{
				flag94 = false;
				NPC.velocity.Y = NPC.velocity.Y + 1f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 1f;
					num956 = 32f;
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
			float num958 = 0.2f;
			float num959 = 0.125f;
			Vector2 vector106 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num960 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
			float num961 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
			num960 = (float)((int)(num960 / 16f) * 16);
			num961 = (float)((int)(num961 / 16f) * 16);
			vector106.X = (float)((int)(vector106.X / 16f) * 16);
			vector106.Y = (float)((int)(vector106.Y / 16f) * 16);
			num960 -= vector106.X;
			num961 -= vector106.Y;
			float num962 = (float)Math.Sqrt((double)(num960 * num960 + num961 * num961));
			if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
			{
				try
				{
					vector106 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num960 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector106.X;
					num961 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector106.Y;
				}
				catch
				{
				}
				NPC.rotation = (float)Math.Atan2((double)num961, (double)num960) + 1.57f;
				num962 = (float)Math.Sqrt((double)(num960 * num960 + num961 * num961));
				int num963 = (int)(44f * NPC.scale);
				num962 = (num962 - (float)num963) / num962;
				num960 *= num962;
				num961 *= num962;
				NPC.velocity = Vector2.Zero;
				NPC.position.X = NPC.position.X + num960;
				NPC.position.Y = NPC.position.Y + num961;
				return;
			}
			if (!flag94)
			{
				NPC.TargetClosest(true);
				NPC.velocity.Y = NPC.velocity.Y + 0.15f;
				if (NPC.velocity.Y > num956)
				{
					NPC.velocity.Y = num956;
				}
				if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num956 * 0.4)
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num958 * 1.1f;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X + num958 * 1.1f;
					}
				}
				else if (NPC.velocity.Y == num956)
				{
					if (NPC.velocity.X < num960)
					{
						NPC.velocity.X = NPC.velocity.X + num958;
					}
					else if (NPC.velocity.X > num960)
					{
						NPC.velocity.X = NPC.velocity.X - num958;
					}
				}
				else if (NPC.velocity.Y > 4f)
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num958 * 0.9f;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X - num958 * 0.9f;
					}
				}
			}
			else
			{
				if (NPC.soundDelay == 0)
				{
					float num964 = num962 / 40f;
					if (num964 < 10f)
					{
						num964 = 10f;
					}
					if (num964 > 20f)
					{
						num964 = 20f;
					}
					NPC.soundDelay = (int)num964;
					SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
				}
				num962 = (float)Math.Sqrt((double)(num960 * num960 + num961 * num961));
				float num965 = Math.Abs(num960);
				float num966 = Math.Abs(num961);
				float num967 = num956 / num962;
				num960 *= num967;
				num961 *= num967;
				if (((NPC.velocity.X > 0f && num960 > 0f) || (NPC.velocity.X < 0f && num960 < 0f)) && ((NPC.velocity.Y > 0f && num961 > 0f) || (NPC.velocity.Y < 0f && num961 < 0f)))
				{
					if (NPC.velocity.X < num960)
					{
						NPC.velocity.X = NPC.velocity.X + num959;
					}
					else if (NPC.velocity.X > num960)
					{
						NPC.velocity.X = NPC.velocity.X - num959;
					}
					if (NPC.velocity.Y < num961)
					{
						NPC.velocity.Y = NPC.velocity.Y + num959;
					}
					else if (NPC.velocity.Y > num961)
					{
						NPC.velocity.Y = NPC.velocity.Y - num959;
					}
				}
				if ((NPC.velocity.X > 0f && num960 > 0f) || (NPC.velocity.X < 0f && num960 < 0f) || (NPC.velocity.Y > 0f && num961 > 0f) || (NPC.velocity.Y < 0f && num961 < 0f))
				{
					if (NPC.velocity.X < num960)
					{
						NPC.velocity.X = NPC.velocity.X + num958;
					}
					else if (NPC.velocity.X > num960)
					{
						NPC.velocity.X = NPC.velocity.X - num958;
					}
					if (NPC.velocity.Y < num961)
					{
						NPC.velocity.Y = NPC.velocity.Y + num958;
					}
					else if (NPC.velocity.Y > num961)
					{
						NPC.velocity.Y = NPC.velocity.Y - num958;
					}
					if ((double)Math.Abs(num961) < (double)num956 * 0.2 && ((NPC.velocity.X > 0f && num960 < 0f) || (NPC.velocity.X < 0f && num960 > 0f)))
					{
						if (NPC.velocity.Y > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num958 * 2f;
						}
						else
						{
							NPC.velocity.Y = NPC.velocity.Y - num958 * 2f;
						}
					}
					if ((double)Math.Abs(num960) < (double)num956 * 0.2 && ((NPC.velocity.Y > 0f && num961 < 0f) || (NPC.velocity.Y < 0f && num961 > 0f)))
					{
						if (NPC.velocity.X > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num958 * 2f;
						}
						else
						{
							NPC.velocity.X = NPC.velocity.X - num958 * 2f;
						}
					}
				}
				else if (num965 > num966)
				{
					if (NPC.velocity.X < num960)
					{
						NPC.velocity.X = NPC.velocity.X + num958 * 1.1f;
					}
					else if (NPC.velocity.X > num960)
					{
						NPC.velocity.X = NPC.velocity.X - num958 * 1.1f;
					}
					if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num956 * 0.5)
					{
						if (NPC.velocity.Y > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num958;
						}
						else
						{
							NPC.velocity.Y = NPC.velocity.Y - num958;
						}
					}
				}
				else
				{
					if (NPC.velocity.Y < num961)
					{
						NPC.velocity.Y = NPC.velocity.Y + num958 * 1.1f;
					}
					else if (NPC.velocity.Y > num961)
					{
						NPC.velocity.Y = NPC.velocity.Y - num958 * 1.1f;
					}
					if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num956 * 0.5)
					{
						if (NPC.velocity.X > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num958;
						}
						else
						{
							NPC.velocity.X = NPC.velocity.X - num958;
						}
					}
				}
			}
			NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
			if (flag94)
			{
				if (NPC.localAI[0] != 1f)
				{
					NPC.netUpdate = true;
				}
				NPC.localAI[0] = 1f;
			}
			else
			{
				if (NPC.localAI[0] != 0f)
				{
					NPC.netUpdate = true;
				}
				NPC.localAI[0] = 0f;
			}
			if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
			{
				NPC.netUpdate = true;
				return;
			}
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                new FlavorTextBestiaryInfoElement("A sea serpent that once swam through the waters of the Ilmeran Sea, it curiously chose to stay in its home rather than find a new one deep below. Quickly taking to a diet of antlion and cactus, it was able to take the place of possibly the desert’s apex predator after its only competition went into hiding.")
           });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("VictoryShard").Type, 1, 7, 14));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Starfish, 1, 5, 9));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Coral, 1, 5, 9));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Seashell, 1, 5, 9));
			npcLoot.Add(ItemDropRule.BossBag(Mod.Find<ModItem>("DesertScourgeBag").Type));
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < hit.Damage/ NPC.lifeMax * 100.0; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.5f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode || Main.rand.Next(1) == 0)
			{
				target.AddBuff(BuffID.Bleeding, 600, true);
			}
		}
	}
}