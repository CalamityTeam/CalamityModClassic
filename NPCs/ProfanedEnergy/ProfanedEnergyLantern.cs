using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Shaders;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Social;
using CalamityModClassic1Point1.Projectiles;
using Terraria.WorldBuilding;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.Providence;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.ProfanedEnergy
{
	public class ProfanedEnergyLantern : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Profaned Energy");
			//Tooltip.SetDefault("Profaned Energy");
			NPC.npcSlots = 1f;
			NPC.aiStyle = -1;
			NPC.damage = 40;
			NPC.width = 30; //324
			NPC.height = 30; //216
			NPC.defense = 58;
			NPC.lifeMax = 28000;
			NPC.knockBackResist = 0.5f;
			AIType = -1;
			Main.npcFrameCount[NPC.type] = 5;
			NPC.value = Item.buyPrice(0, 0, 10, 0);
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath36;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("Does this enemy even spawn properly in this version?")

            });
        }
        public override void AI()
		{
			if (NPC.ai[0] < 0f || NPC.ai[0] >= (float)Main.maxTilesX || NPC.ai[1] < 0f || NPC.ai[1] >= (float)Main.maxTilesX)
			{
				return;
			}
			if (Main.tile[(int)NPC.ai[0], (int)NPC.ai[1]] == null || !Main.tile[(int)NPC.ai[0], (int)NPC.ai[1]].HasTile)
			{
				NPC.life = -1;
				NPC.HitEffect(0, 10.0);
				NPC.active = false;
				return;
			}
			FixExploitManEaters.ProtectSpot((int)NPC.ai[0], (int)NPC.ai[1]);
			NPC.TargetClosest(true);
			float num187 = 0.15f;
			float num188 = 350f;
			NPC.ai[2] += 1f;
			if (NPC.ai[2] > 300f)
			{
				num188 = (float)((int)((double)num188 * 1.3));
				if (NPC.ai[2] > 450f)
				{
					NPC.ai[2] = 0f;
				}
			}
			Vector2 vector22 = new Vector2(NPC.ai[0] * 16f + 8f, NPC.ai[1] * 16f + 8f);
			float num189 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - (float)(NPC.width / 2) - vector22.X;
			float num190 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - (float)(NPC.height / 2) - vector22.Y;
			float num191 = (float)Math.Sqrt((double)(num189 * num189 + num190 * num190));
			if (num191 > num188)
			{
				num191 = num188 / num191;
				num189 *= num191;
				num190 *= num191;
			}
			if (NPC.position.X < NPC.ai[0] * 16f + 8f + num189)
			{
				NPC.velocity.X = NPC.velocity.X + num187;
				if (NPC.velocity.X < 0f && num189 > 0f)
				{
					NPC.velocity.X = NPC.velocity.X + num187 * 1.5f;
				}
			}
			else if (NPC.position.X > NPC.ai[0] * 16f + 8f + num189)
			{
				NPC.velocity.X = NPC.velocity.X - num187;
				if (NPC.velocity.X > 0f && num189 < 0f)
				{
					NPC.velocity.X = NPC.velocity.X - num187 * 1.5f;
				}
			}
			if (NPC.position.Y < NPC.ai[1] * 16f + 8f + num190)
			{
				NPC.velocity.Y = NPC.velocity.Y + num187;
				if (NPC.velocity.Y < 0f && num190 > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y + num187 * 1.5f;
				}
			}
			else if (NPC.position.Y > NPC.ai[1] * 16f + 8f + num190)
			{
				NPC.velocity.Y = NPC.velocity.Y - num187;
				if (NPC.velocity.Y > 0f && num190 < 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y - num187 * 1.5f;
				}
			}
			if (NPC.velocity.X > 2f)
			{
				NPC.velocity.X = 2f;
			}
			if (NPC.velocity.X < -2f)
			{
				NPC.velocity.X = -2f;
			}
			if (NPC.velocity.Y > 2f)
			{
				NPC.velocity.Y = 2f;
			}
			if (NPC.velocity.Y < -2f)
			{
				NPC.velocity.Y = -2f;
			}
			NPC.rotation = (float)Math.Atan2((double)num190, (double)num189) + 1.57f;
			if (NPC.collideX)
			{
				NPC.netUpdate = true;
				NPC.velocity.X = NPC.oldVelocity.X * -0.7f;
				if (NPC.velocity.X > 0f && NPC.velocity.X < 2f)
				{
					NPC.velocity.X = 2f;
				}
				if (NPC.velocity.X < 0f && NPC.velocity.X > -2f)
				{
					NPC.velocity.X = -2f;
				}
			}
			if (NPC.collideY)
			{
				NPC.netUpdate = true;
				NPC.velocity.Y = NPC.oldVelocity.Y * -0.7f;
				if (NPC.velocity.Y > 0f && NPC.velocity.Y < 2f)
				{
					NPC.velocity.Y = 2f;
				}
				if (NPC.velocity.Y < 0f && NPC.velocity.Y > -2f)
				{
					NPC.velocity.Y = -2f;
				}
			}
			if (Main.netMode != 1)
			{
				if (!Main.player[NPC.target].dead)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= 240f)
					{
						if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
						{
							float num196 = 11f;
							vector22 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num189 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector22.X + (float)Main.rand.Next(-10, 11);
							float num197 = Math.Abs(num189 * 0.1f);
							if (num190 > 0f)
							{
								num197 = 0f;
							}
							num190 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector22.Y + (float)Main.rand.Next(-10, 11) - num197;
							num191 = (float)Math.Sqrt((double)(num189 * num189 + num190 * num190));
							num191 = num196 / num191;
							num189 *= num191;
							num190 *= num191;
							int num9 = Mod.Find<ModProjectile>("HolyBomb").Type;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, num189, num190, num9, 70, 0f, Main.myPlayer, 0f, 0f);
							NPC.localAI[0] = 0f;
							return;
						}
						NPC.localAI[0] = 250f;
						return;
					}
				}
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			int type = NPC.type;
			//npc.LoadNPC(type);
			if (NPC.setFrameSize)
			{
				NPC.frame = new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Npc[type].Value.Width, TextureAssets.Npc[type].Value.Height / Main.npcFrameCount[type]);
				NPC.setFrameSize = false;
			}
			if (NPC.realLife == -1 && NPC.life >= NPC.lifeMax && !NPC.boss)
			{
				bool flag = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0)).ToVector3().Length() > 0.4325f;
				bool flag2 = false;
				if (LockOnHelper.AimedTarget == NPC)
				{
					flag2 = true;
				}
				else
				{
					float num = NPC.Distance(Main.player[Main.myPlayer].Center);
					if (num < 400f && flag)
					{
						flag2 = true;
					}
				}
				if (flag2 && NPC.lifeMax < 5)
				{
					flag2 = false;
				}
				if (flag2 && NPC.aiStyle == 25 && NPC.ai[0] == 0f)
				{
					flag2 = false;
				}
				if (flag2)
				{
					//NPC.nameOver = MathHelper.Clamp(//NPC.nameOver + 0.025f, 0f, 1f);
				}
				else
				{
					//NPC.nameOver = MathHelper.Clamp(//NPC.nameOver - 0.025f, 0f, 1f);
				}
			}
			else
			{
				//NPC.nameOver = MathHelper.Clamp(//NPC.nameOver - 0.025f, 0f, 1f);
			}
			Vector2 vector2 = new Vector2(NPC.position.X + (float)(NPC.width / 2), NPC.position.Y + (float)(NPC.height / 2));
			float num6 = NPC.ai[0] * 16f + 8f - vector2.X;
			float num7 = NPC.ai[1] * 16f + 8f - vector2.Y;
			float rotation2 = (float)Math.Atan2((double)num7, (double)num6) - 1.57f;
			bool flag5 = true;
			Mod mod = ModLoader.GetMod("CalamityModClassic1Point1");
			while (flag5)
			{
				int num8 = 20;
				int num9 = 12;
				float num10 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
				if (num10 < (float)num9)
				{
					num8 = (int)num10 - num9 + num8;
					flag5 = false;
				}
				num10 = (float)num8 / num10;
				num6 *= num10;
				num7 *= num10;
				vector2.X += num6;
				vector2.Y += num7;
				num6 = NPC.ai[0] * 16f + 8f - vector2.X;
				num7 = NPC.ai[1] * 16f + 8f - vector2.Y;
				Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
				Texture2D texture = ModContent.Request<Texture2D>("NPCs/ProfanedEnergy/ProfanedEnergySegment").Value;
				Main.spriteBatch.Draw(texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, texture.Width, num8)), color2, rotation2, new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
			return (spawnInfo.Player.ZoneHallow || spawnInfo.Player.ZoneUnderworldHeight) && !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord ? 0.05f : 0f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<UnholyEssence>(), 1, 2, 4));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EnergyStaff>(), 10));
        }
	}
}