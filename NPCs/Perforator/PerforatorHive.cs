using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.HiveMind;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Weapons.HiveMind;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Perforator;
using CalamityModClassic1Point2.Items.Weapons.Perforators;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Perforator
{
	[AutoloadBossHead]
	public class PerforatorHive : ModNPC
	{
		public int spawn = 10;
		public int small = 0;
		public int medium = 0;
		public int large = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Perforator Hive");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 15f;
			NPC.damage = 35;
			NPC.width = 110; //324
			NPC.height = 100; //216
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorld.revenge ? 4000 : 2800;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 5, 0, 0);
			NPC.buffImmune[44] = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath19;
			Music = MusicID.Boss2;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("PerforatorBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
                new FlavorTextBestiaryInfoElement("The corpse of the Brain of Cthulhu, risen by worms.")

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
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 1.5f, 0f, 0f);
			Player player = Main.player[NPC.target];
			bool isCrimson = player.ZoneCrimson;
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, 10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			NPC.TargetClosest(true);
			int npcType = Mod.Find<ModNPC>("PerforatorHeadLarge").Type;
			bool wormAlive = false;
			if (NPC.CountNPCS(npcType) > 0)
			{
				wormAlive = true;
			}
			if (wormAlive)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = isCrimson ? false : true;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				int shoot = revenge ? 6 : 4;
				NPC.localAI[0] += (float)Main.rand.Next(shoot);
				if (NPC.localAI[0] >= (float)Main.rand.Next(300, 500))
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						float num179 = 0.1f;
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
						float num181 = Math.Abs(num180) * 0.1f;
						float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
						float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
						NPC.netUpdate = true;
						num183 = num179 / num183;
						num180 *= num183;
						num182 *= num183;
						int num184 = expertMode ? 12 : 15;
						int num185 = Mod.Find<ModProjectile>("BloodGeyser").Type;
						value9.X += num180;
						value9.Y += num182;
						for (int num186 = 0; num186 < 20; num186++)
						{
							num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
							num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = 12f / num183;
							num180 += (float)Main.rand.Next(-180, 181);
							num182 += (float)Main.rand.Next(-180, 181);
							num180 *= num183;
							num182 *= num183;
							int blood = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[blood].velocity.Y = -5f;
						}
					}
				}
	       	}
			if (NPC.ai[0] == 0f)
			{
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				float num1065 = 6f; //changed from 6 to 7.5 modifies speed while firing projectiles
				float num1066 = 0.075f; //changed from 0.075 to 0.09375 modifies speed while firing projectiles
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector122 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1067 = player.position.X + (float)(player.width / 2) - vector122.X;
				float num1068 = player.position.Y + (float)(player.height / 2) - 300f - vector122.Y;
				float num1069 = (float)Math.Sqrt((double)(num1067 * num1067 + num1068 * num1068));
				if (!Collision.CanHit(new Vector2(vector121.X, vector121.Y - 30f), 1, 1, player.position, player.width, player.height))
				{
					num1065 = 14f; //changed from 14 not a prob
					num1066 = 0.1f; //changed from 0.1 not a prob
					vector122 = vector121;
					num1067 = player.position.X + (float)(player.width / 2) - vector122.X;
					num1068 = player.position.Y + (float)(player.height / 2) - vector122.Y;
					num1069 = (float)Math.Sqrt((double)(num1067 * num1067 + num1068 * num1068));
					num1069 = num1065 / num1069;
					if (NPC.velocity.X < num1067)
					{
						NPC.velocity.X = NPC.velocity.X + num1066;
						if (NPC.velocity.X < 0f && num1067 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num1066;
						}
					}
					else if (NPC.velocity.X > num1067)
					{
						NPC.velocity.X = NPC.velocity.X - num1066;
						if (NPC.velocity.X > 0f && num1067 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num1066;
						}
					}
					if (NPC.velocity.Y < num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1066;
						if (NPC.velocity.Y < 0f && num1068 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num1066;
						}
					}
					else if (NPC.velocity.Y > num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1066;
						if (NPC.velocity.Y > 0f && num1068 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num1066;
						}
					}
				}
				else if (num1069 > 100f)
				{
					NPC.TargetClosest(true);
					NPC.spriteDirection = NPC.direction;
					num1069 = num1065 / num1069;
					if (NPC.velocity.X < num1067)
					{
						NPC.velocity.X = NPC.velocity.X + num1066;
						if (NPC.velocity.X < 0f && num1067 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num1066 * 2f;
						}
					}
					else if (NPC.velocity.X > num1067)
					{
						NPC.velocity.X = NPC.velocity.X - num1066;
						if (NPC.velocity.X > 0f && num1067 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num1066 * 2f;
						}
					}
					if (NPC.velocity.Y < num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1066;
						if (NPC.velocity.Y < 0f && num1068 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num1066 * 2f;
						}
					}
					else if (NPC.velocity.Y > num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1066;
						if (NPC.velocity.Y > 0f && num1068 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num1066 * 2f;
						}
					}
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<PerforatorTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<PerforatorBag>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PerforatorMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BloodSample>(), 1, 7, 14));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.CrimtaneBar, 1, 2, 5));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Vertebrae, 1, 3, 9));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Aorta>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BloodBath>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VeinBurster>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BloodyRupture>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Perforators.SausageMaker>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ToothBall>(), 4, 25, 50));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Eviscerator>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BloodClotStaff>(), 4));
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.CountNPCS(Mod.Find<ModNPC>("PerforatorHeadMini").Type) < 5)
			{
				spawn--;
				if (NPC.life > 0 && Main.netMode != NetmodeID.MultiplayerClient && spawn <= 0)
				{
					spawn = 10;
					int num660 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), Mod.Find<ModNPC>("PerforatorHeadMini").Type, 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == NetmodeID.Server && num660 < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
					}
					NPC.netUpdate = true;
				}
			}
			if (NPC.life <= NPC.lifeMax * 0.7f && Main.netMode != NetmodeID.MultiplayerClient && small == 0)
			{
				small = 1;
				int num660 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), Mod.Find<ModNPC>("PerforatorHeadSmall").Type, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == NetmodeID.Server && num660 < 200)
				{
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
				}
				NPC.netUpdate = true;
			}
			if (NPC.life <= NPC.lifeMax * 0.4f && Main.netMode != NetmodeID.MultiplayerClient && medium == 0)
			{
				medium = 1;
				int num660 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), Mod.Find<ModNPC>("PerforatorHeadMedium").Type, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == NetmodeID.Server && num660 < 200)
				{
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
				}
				NPC.netUpdate = true;
			}
			if (NPC.life <= NPC.lifeMax * 0.1f && Main.netMode != NetmodeID.MultiplayerClient && large == 0)
			{
				large = 1;
				int num660 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), Mod.Find<ModNPC>("PerforatorHeadLarge").Type, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == NetmodeID.Server && num660 < 200)
				{
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
				}
				NPC.netUpdate = true;
			}
			for (int k = 0; k < hit.Damage / NPC.lifeMax * 100.0; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}