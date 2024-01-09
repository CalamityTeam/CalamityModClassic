using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items.DevourerMunsters;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Accessories;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.CeaselessVoid
{
	[AutoloadBossHead]
	public class CeaselessVoid : ModNPC
	{
		public float bossLife;
		public float beamPortal = 0f;
		public float shootBoost = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ceaseless Void");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 15f;
			NPC.width = 100; //324
			NPC.height = 100; //216
			NPC.defense = 0;
			NPC.lifeMax = 150;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.boss = true;
			NPC.dontTakeDamage = true;
			NPC.chaseable = false;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicID.Boss2;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                new FlavorTextBestiaryInfoElement("The void calls out.")

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
			Player player = Main.player[NPC.target];
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			CalamityGlobalNPC.voidBoss = NPC.whoAmI;
			Vector2 vector = NPC.Center;
			if (Vector2.Distance(player.Center, vector) > 5600f)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if (Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("DarkEnergy").Type) || Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("DarkEnergy2").Type) || Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("DarkEnergy3").Type))
				{
					flag100 = true;
				}
			}
			if (flag100)
			{
				NPC.dontTakeDamage = true;
			}
			else if (!flag100)
			{
				NPC.dontTakeDamage = false;
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.3);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						shootBoost += 1f;
						int glob = revenge ? 8 : 4;
						if (bossLife <= 0.5f)
						{
							glob = revenge ? 16 : 8;
						}
						for (int num662 = 0; num662 < glob; num662++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DarkEnergySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
	       	}
	       	if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                beamPortal += expertMode ? 2f : 1f;
                beamPortal += shootBoost;
				if (beamPortal >= 1200f)
				{
					beamPortal = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						float num941 = 3f; //speed
						Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
						float num942 = player.position.X + (float)player.width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
						float num943 = player.position.Y + (float)player.height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
						float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
						num944 = num941 / num944;
						num942 *= num944;
						num943 *= num944;
						num942 += (float)Main.rand.Next(-10, 11) * 0.05f;
						num943 += (float)Main.rand.Next(-10, 11) * 0.05f;
						int num945 = expertMode ? 55 : 60;
						int num946 = Mod.Find<ModProjectile>("DoGBeamPortal").Type;
						vector104.X += num942 * 5f;
						vector104.Y += num943 * 5f;
						int num947 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num947].timeLeft = 300;
						NPC.netUpdate = true;
					}
				}
            }
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			if (Main.rand.NextBool(5))
			{
				int num805 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), DustID.ShadowbeamStaff, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_2F45E_cp_0 = Main.dust[num805];
				expr_2F45E_cp_0.velocity.X = expr_2F45E_cp_0.velocity.X * 0.5f;
				Dust expr_2F47E_cp_0 = Main.dust[num805];
				expr_2F47E_cp_0.velocity.Y = expr_2F47E_cp_0.velocity.Y * 0.1f;
			}
			if (NPC.ai[0] == 0f)
			{
				if (NPC.ai[1] == 0f)
				{
					float num823 = 10f;
					float num824 = 0.2f;
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					float num826 = player.position.Y + (float)(player.height / 2) - 300f - vector82.Y;
					float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
					num827 = num823 / num827;
					num825 *= num827;
					num826 *= num827;
					if (NPC.velocity.X < num825)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
						if (NPC.velocity.X < 0f && num825 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num824;
						}
					}
					else if (NPC.velocity.X > num825)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
						if (NPC.velocity.X > 0f && num825 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num824;
						}
					}
					if (NPC.velocity.Y < num826)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
						if (NPC.velocity.Y < 0f && num826 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num824;
						}
					}
					else if (NPC.velocity.Y > num826)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
						if (NPC.velocity.Y > 0f && num826 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num824;
						}
					}
				}
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<DarkPlasma>(), 1, 2, 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CeaselessVoidTrophy>(), 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<MirrorBlade>(), 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<ArcanumoftheVoid>(), 3));
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
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
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 400;
			NPC.damage = 0;
		}
	}
}