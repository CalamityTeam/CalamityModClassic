using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.SlimeGod;
using CalamityModClassic1Point2.Items.Weapons.SlimeGod;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.SlimeGod
{
	[AutoloadBossHead]
	public class SlimeGodCore : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Slime God");
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 30;
			NPC.npcSlots = 5f;
			NPC.width = 44; //324
			NPC.height = 44; //216
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorld.revenge ? 1600 : 1000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 8, 0, 0);
			NPC.alpha = 80;
			AnimationType = 10;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.Boss1;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("SlimeGodBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
                new FlavorTextBestiaryInfoElement("The god of all slimes.")

            });
        }

        public override void AI()
		{
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			Player player = Main.player[NPC.target];
			int randomDust = Main.rand.Next(2);
			if (randomDust == 0)
			{
				randomDust = 173;
			}
			else
			{
				randomDust = 260;
			}
			int num658 = Dust.NewDust(NPC.position, NPC.width, NPC.height, randomDust, NPC.velocity.X, NPC.velocity.Y, 255, new Color(0, 80, 255, 80), NPC.scale * 1.5f);
			Main.dust[num658].noGravity = true;
			Main.dust[num658].velocity *= 0.5f;
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if ((Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("SlimeGod").Type)) || (Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("SlimeGodSplit").Type)) ||
				    (Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("SlimeGodRun").Type)) || (Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("SlimeGodRunSplit").Type)))
				{
					flag100 = true;
				}
			}
			if (flag100)
			{
				NPC.dontTakeDamage = true;
			}
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
			else if (NPC.timeLeft > 2400)
			{
				NPC.timeLeft = 2400;
			}
			if (!flag100)
			{
				NPC.dontTakeDamage = false;
				NPC.damage = 80;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[0] += (float)Main.rand.Next(10);
					if (expertMode)
					{
						if (NPC.localAI[0] >= (float)Main.rand.Next(300, 500))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								float num179 = revenge ? 9f : 11f;
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								float num181 = Math.Abs(num180) * 0.1f;
								float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
								float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								NPC.netUpdate = true;
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								int num184 = 20;
								int num185 = Main.rand.Next(2);
								if (num185 == 0)
								{
									num185 = Mod.Find<ModProjectile>("AbyssMine").Type;
								}
								else
								{
									num185 = Mod.Find<ModProjectile>("AbyssMine2").Type;
								}
								value9.X += num180;
								value9.Y += num182;
								num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = 12f / num183;
								num180 += (float)Main.rand.Next(-360, 361);
								num182 += (float)Main.rand.Next(-360, 361);
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
					if (NPC.localAI[0] >= (float)Main.rand.Next(300, 500))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							float num179 = revenge ? 12f : 11f;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							NPC.netUpdate = true;
							num183 = num179 / num183;
							num180 *= num183;
							num182 *= num183;
							int num184 = expertMode ? 20 : 30;
							int num185 = Main.rand.Next(2);
							if (num185 == 0)
							{
								num185 = Mod.Find<ModProjectile>("AbyssBallVolley").Type;
							}
							else
							{
								num185 = Mod.Find<ModProjectile>("AbyssBallVolley2").Type;
							}
							value9.X += num180;
							value9.Y += num182;
							for (int num186 = 0; num186 < 3; num186++)
							{
								num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = 12f / num183;
								num180 += (float)Main.rand.Next(-60, 61);
								num182 += (float)Main.rand.Next(-60, 61);
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
			}
			NPC.TargetClosest(true);
			float num1372 = 7f;
			if (revenge)
			{
				num1372 = 20f;
			}
			else if (!flag100)
			{
				num1372 = 15f;
			}
			Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
			float num1373 = player.position.X + (float)player.width * 0.5f - vector167.X;
			float num1374 = player.Center.Y - vector167.Y;
			float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
			float num1376 = num1372 / num1375;
			num1373 *= num1376;
			num1374 *= num1376;
			NPC.ai[0] -= 1f;
			if (num1375 < 200f || NPC.ai[0] > 0f)
			{
				if (num1375 < 200f)
				{
					NPC.ai[0] = 20f;
				}
				if (NPC.velocity.X < 0f)
				{
					NPC.direction = -1;
				}
				else
				{
					NPC.direction = 1;
				}
				return;
			}
			NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
			NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
			if (num1375 < 350f)
			{
				NPC.velocity.X = (NPC.velocity.X * 10f + num1373) / 11f;
				NPC.velocity.Y = (NPC.velocity.Y * 10f + num1374) / 11f;
			}
			if (num1375 < 300f)
			{
				NPC.velocity.X = (NPC.velocity.X * 7f + num1373) / 8f;
				NPC.velocity.Y = (NPC.velocity.Y * 7f + num1374) / 8f;
			}
			return;
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<SlimeGodTrophy>(), 10));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SlimeGodBag>()));
			LeadingConditionRule notExp = new LeadingConditionRule(new Conditions.NotExpert());
            notExp.OnSuccess(ItemDropRule.OneFromOptions(7, ModContent.ItemType<SlimeGodMask>(), ModContent.ItemType<SlimeGodMask2>()));
			npcLoot.Add(notExp);
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Gel, 1, 180, 250));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PurifiedGel>(), 1, 25, 40));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<GelDart>(), 1, 80, 100));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<OverloadedBlaster>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AbyssalTome>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EldritchTome>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CrimslimeStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CorroslimeStaff>(), 4));
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 40;
				NPC.height = 40;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.VortexDebuff, 200, true);
			if (CalamityWorld.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
	}
}