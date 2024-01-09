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

namespace CalamityModClassic1Point0.NPCs.TheDevourerofGods
{
	public class DevourerofGodsTail : ModNPC
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
			//DisplayName.SetDefault("The Devourer of Gods");
			NPC.damage = 60;
			NPC.npcSlots = 5f;
			NPC.width = 38; //324
			NPC.height = 38; //216
			NPC.defense = 85;
			NPC.lifeMax = 500000;
			NPC.knockBackResist = 0f;
			NPC.scale = 1.25f;
			NPC.alpha = 255;
			NPC.boss = true;
			NPC.behindTiles = true;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound= SoundID.NPCHit4;
			NPC.DeathSound= SoundID.NPCDeath14;
			NPC.netAlways = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.dontCountMe = true;
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
			if (Main.npc[(int)NPC.ai[1]].alpha < 128)
			{
				if (NPC.alpha != 0)
				{
					for (int num934 = 0; num934 < 2; num934++)
					{
						int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num935].noGravity = true;
						Main.dust[num935].noLight = true;
					}
				}
				NPC.alpha -= 42;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
			}
			bool flag93 = false;
			if (NPC.ai[1] <= 0f)
			{
				flag93 = true;
			}
			else if (Main.npc[(int)NPC.ai[1]].life <= 0)
			{
				flag93 = true;
			}
			if (flag93)
			{
				NPC.life = 0;
				NPC.HitEffect(0, 10.0);
				NPC.checkDead();
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
				Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.35f, 0.05f, 0.35f);
				NPC.localAI[1] = 1f;
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
			}
			float num958 = 0.1f;
			float num959 = 0.15f;
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
			if (Main.expertMode)
			{
				num958 *= 1.2f;
				num959 *= 1.2f;
			}
			if (Main.dayTime)
			{
				num958 *= 2f;
				num959 *= 2f;
			}
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
		}
		
		public void PlayerInteraction(int player)
		{
			if (NPC.playerInteraction[player])
			{
				return;
			}
			for (int j = 0; j < 200; j++)
			{
				if (j != NPC.whoAmI && Main.npc[j].active)
				{
					Main.npc[j].ApplyInteraction(player);
				}
			}
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
			NPC.damage = (int)(NPC.damage * 0.85f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Chilled, 180, true);
			}
			else
			{
				target.AddBuff(BuffID.Chilled, 90, true);
			}
		}
	}
}