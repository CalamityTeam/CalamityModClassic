using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2;
using Terraria.WorldBuilding;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.DevourerMunsters;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Weapons;

namespace CalamityModClassic1Point2.NPCs.StormWeaver
{
	[AutoloadBossHead]
	public class StormWeaverHeadNaked : ModNPC
	{
		public bool flies = true;
		public const float speed = 13f;
		public const float turnSpeed = 0.35f;
		public bool tail = false;
		public const int minLength = 60;
		public const int maxLength = 61;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Storm Weaver");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 200; //150
			NPC.npcSlots = 5f;
			NPC.width = 42; //324
			NPC.height = 42; //216
			NPC.defense = 0;
			NPC.lifeMax = 300000; //250000
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath13;
			NPC.netAlways = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			Music = MusicID.Boss3;
		}
		
		public override void AI()
		{
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if (NPC.ai[3] > 0f)
			{
				NPC.realLife = (int)NPC.ai[3];
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.velocity.Length();
			if (NPC.alpha != 0)
			{
				for (int num934 = 0; num934 < 2; num934++)
				{
					int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TheDestroyer, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num935].noGravity = true;
					Main.dust[num935].noLight = true;
				}
			}
			NPC.alpha -= 12;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
            {
	            if (!tail && NPC.ai[0] == 0f)
				{
	            	int Previous = NPC.whoAmI;
					for (int num36 = 0; num36 < maxLength; num36++)
	                {
	                    int lol = 0;
	                    if (num36 >= 0 && num36 < minLength)
	                    {
	                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("StormWeaverBodyNaked").Type, NPC.whoAmI);
	                    }
	                    else
	                    {
	                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("StormWeaverTailNaked").Type, NPC.whoAmI);
	                    }
	                    Main.npc[lol].realLife = NPC.whoAmI;
	                    Main.npc[lol].ai[2] = (float)NPC.whoAmI;
	                    Main.npc[lol].ai[1] = (float)Previous;
	                    Main.npc[Previous].ai[0] = (float)lol;
						NPC.netUpdate = true;
						Previous = lol;
					}
					tail = true;
	            }
                if (!NPC.active && Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
				}
            }
			int num180 = (int)(NPC.position.X / 16f) - 1;
			int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
			int num182 = (int)(NPC.position.Y / 16f) - 1;
			int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
			if (num180 < 0)
			{
				num180 = 0;
			}
			if (num181 > Main.maxTilesX)
			{
				num181 = Main.maxTilesX;
			}
			if (num182 < 0)
			{
				num182 = 0;
			}
			if (num183 > Main.maxTilesY)
			{
				num183 = Main.maxTilesY;
			}
			NPC.localAI[1] = 0f;
			bool canFly = flies;
			if (Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(false);
				canFly = false;
				NPC.velocity.Y = NPC.velocity.Y + 10f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 10f;
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
			float num188 = speed;
			float num189 = turnSpeed;
			Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
			float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
			int num42 = -1;
			int num43 = (int)(Main.player[NPC.target].Center.X / 16f);
			int num44 = (int)(Main.player[NPC.target].Center.Y / 16f);
			for (int num45 = num43 - 2; num45 <= num43 + 2; num45++)
			{
				for (int num46 = num44; num46 <= num44 + 15; num46++)
				{
					if (WorldGen.SolidTile2(num45, num46))
					{
						num42 = num46;
						break;
					}
				}
				if (num42 > 0)
				{
					break;
				}
			}
			if (num42 > 0)
			{
				num42 *= 16;
				float num47 = (float)(num42 - 800);
				if (Main.player[NPC.target].position.Y > num47)
				{
					num192 = num47;
					if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 500f)
					{
						if (NPC.velocity.X > 0f)
						{
							num191 = Main.player[NPC.target].Center.X + 600f;
						}
						else
						{
							num191 = Main.player[NPC.target].Center.X - 600f;
						}
					}
				}
			}
			else
			{
				num188 = revenge ? 19f : 18f;
				num189 = revenge ? 0.7f : 0.65f;
			}
			if (!Main.player[NPC.target].ZoneSkyHeight)
			{
				num188 = 24f;
				num189 = 0.85f;
			}
			float num48 = num188 * 1.3f;
			float num49 = num188 * 0.7f;
			float num50 = NPC.velocity.Length();
			if (num50 > 0f)
			{
				if (num50 > num48)
				{
					NPC.velocity.Normalize();
					NPC.velocity *= num48;
				}
				else if (num50 < num49)
				{
					NPC.velocity.Normalize();
					NPC.velocity *= num49;
				}
			}
			if (num42 > 0)
			{
				for (int num51 = 0; num51 < 200; num51++)
				{
					if (Main.npc[num51].active && Main.npc[num51].type == NPC.type && num51 != NPC.whoAmI)
					{
						Vector2 vector3 = Main.npc[num51].Center - NPC.Center;
						if (vector3.Length() < 400f)
						{
							vector3.Normalize();
							vector3 *= 1000f;
							num191 -= vector3.X;
							num192 -= vector3.Y;
						}
					}
				}
			}
			else
			{
				for (int num52 = 0; num52 < 200; num52++)
				{
					if (Main.npc[num52].active && Main.npc[num52].type == NPC.type && num52 != NPC.whoAmI)
					{
						Vector2 vector4 = Main.npc[num52].Center - NPC.Center;
						if (vector4.Length() < 60f)
						{
							vector4.Normalize();
							vector4 *= 200f;
							num191 -= vector4.X;
							num192 -= vector4.Y;
						}
					}
				}
			}
			num191 = (float)((int)(num191 / 16f) * 16);
			num192 = (float)((int)(num192 / 16f) * 16);
			vector18.X = (float)((int)(vector18.X / 16f) * 16);
			vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
			num191 -= vector18.X;
			num192 -= vector18.Y;
			float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
			if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
			{
				try
				{
					vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num191 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
					num192 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
				}
				catch
				{
				}
				NPC.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
				num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				int num194 = NPC.width;
				num193 = (num193 - (float)num194) / num193;
				num191 *= num193;
				num192 *= num193;
				NPC.velocity = Vector2.Zero;
				NPC.position.X = NPC.position.X + num191;
				NPC.position.Y = NPC.position.Y + num192;
			}
			else
			{
				num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				float num196 = System.Math.Abs(num191);
				float num197 = System.Math.Abs(num192);
				float num198 = num188 / num193;
				num191 *= num198;
				num192 *= num198;
				if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
				{
					if (NPC.velocity.X < num191)
					{
						NPC.velocity.X = NPC.velocity.X + num189;
					}
					else
					{
						if (NPC.velocity.X > num191)
						{
							NPC.velocity.X = NPC.velocity.X - num189;
						}
					}
					if (NPC.velocity.Y < num192)
					{
						NPC.velocity.Y = NPC.velocity.Y + num189;
					}
					else
					{
						if (NPC.velocity.Y > num192)
						{
							NPC.velocity.Y = NPC.velocity.Y - num189;
						}
					}
					if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
					{
						if (NPC.velocity.Y > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
						}
						else
						{
							NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
						}
					}
					if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
					{
						if (NPC.velocity.X > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num189 * 2f; //changed from 2
						}
						else
						{
							NPC.velocity.X = NPC.velocity.X - num189 * 2f; //changed from 2
						}
					}
				}
				else
				{
					if (num196 > num197)
					{
						if (NPC.velocity.X < num191)
						{
							NPC.velocity.X = NPC.velocity.X + num189 * 1.1f; //changed from 1.1
						}
						else if (NPC.velocity.X > num191)
						{
							NPC.velocity.X = NPC.velocity.X - num189 * 1.1f; //changed from 1.1
						}
						if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
						{
							if (NPC.velocity.Y > 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y + num189;
							}
							else
							{
								NPC.velocity.Y = NPC.velocity.Y - num189;
							}
						}
					}
					else
					{
						if (NPC.velocity.Y < num192)
						{
							NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
						}
						else if (NPC.velocity.Y > num192)
						{
							NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
						}
						if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
						{
							if (NPC.velocity.X > 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num189;
							}
							else
							{
								NPC.velocity.X = NPC.velocity.X - num189;
							}
						}
					}
				}
			}
			NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 30;
				NPC.height = 30;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 40; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<ArmoredShell>(), 1, 5, 8));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<WeaverTrophy>(), 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<TheStorm>(), 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<StormDragoon>(), 3));
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage *= 2;
		}
	}
}