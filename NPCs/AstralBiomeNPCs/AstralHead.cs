using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.AstralBiomeNPCs
{
	public class AstralHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Astral Head");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.damage = 150;
			NPC.width = 22; //324
			NPC.height = 22; //216
			NPC.defense = 5;
			NPC.lifeMax = 500;
			AnimationType = 23;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.knockBackResist = 0.4f;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit3;
			NPC.DeathSound = SoundID.NPCDeath3;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[39] = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.AstralMeteorBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Get your head out of the stars.")

            });
        }

        public override void AI()
		{
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			float num = 2f;
			float num2 = 0.04f;
			Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num4 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
			float num5 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
			num4 = (float)((int)(num4 / 8f) * 8);
			num5 = (float)((int)(num5 / 8f) * 8);
			vector.X = (float)((int)(vector.X / 8f) * 8);
			vector.Y = (float)((int)(vector.Y / 8f) * 8);
			num4 -= vector.X;
			num5 -= vector.Y;
			float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
			float num7 = num6;
			if (num6 == 0f)
			{
				num4 = NPC.velocity.X;
				num5 = NPC.velocity.Y;
			}
			else
			{
				num6 = num / num6;
				num4 *= num6;
				num5 *= num6;
			}
			if (Main.player[NPC.target].dead)
			{
				num4 = (float)NPC.direction * num / 2f;
				num5 = -num / 2f;
			}
			if (NPC.velocity.X < num4)
			{
				NPC.velocity.X = NPC.velocity.X + num2;
			}
			else if (NPC.velocity.X > num4)
			{
				NPC.velocity.X = NPC.velocity.X - num2;
			}
			if (NPC.velocity.Y < num5)
			{
				NPC.velocity.Y = NPC.velocity.Y + num2;
			}
			else if (NPC.velocity.Y > num5)
			{
				NPC.velocity.Y = NPC.velocity.Y - num2;
			}
			if (num4 > 0f)
			{
				NPC.spriteDirection = 1;
				NPC.rotation = (float)Math.Atan2((double)num5, (double)num4);
			}
			else if (num4 < 0f)
			{
				NPC.spriteDirection = -1;
				NPC.rotation = (float)Math.Atan2((double)num5, (double)num4) + 3.14f;
			}
			float num12 = 0.7f;
			if (NPC.collideX)
			{
				NPC.netUpdate = true;
				NPC.velocity.X = NPC.oldVelocity.X * -num12;
				if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
				{
					NPC.velocity.X = 2f;
				}
				if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
				{
					NPC.velocity.X = -2f;
				}
			}
			if (NPC.collideY)
			{
				NPC.netUpdate = true;
				NPC.velocity.Y = NPC.oldVelocity.Y * -num12;
				if (NPC.velocity.Y > 0f && (double)NPC.velocity.Y < 1.5)
				{
					NPC.velocity.Y = 2f;
				}
				if (NPC.velocity.Y < 0f && (double)NPC.velocity.Y > -1.5)
				{
					NPC.velocity.Y = -2f;
				}
			}
			if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
			{
				NPC.netUpdate = true;
			}
			int num13 = Dust.NewDust(new Vector2(NPC.position.X - NPC.velocity.X, NPC.position.Y - NPC.velocity.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
			Main.dust[num13].noGravity = true;
			Dust expr_F26_cp_0 = Main.dust[num13];
			expr_F26_cp_0.velocity.X = expr_F26_cp_0.velocity.X * 0.3f;
			Dust expr_F44_cp_0 = Main.dust[num13];
			expr_F44_cp_0.velocity.Y = expr_F44_cp_0.velocity.Y * 0.3f;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 30;
				NPC.height = 30;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 5; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 10; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<Stardust>(), 2));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<Stardust>(), 2));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			return (spawnInfo.Player.GetModPlayer<CalamityPlayer>().ZoneAstral && !spawnInfo.Player.ZoneTowerStardust && !spawnInfo.Player.ZoneTowerSolar && !spawnInfo.Player.ZoneTowerVortex && !spawnInfo.Player.ZoneTowerNebula) ? 1f : 0f;
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld.downedStarGod)
			{
				target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 150, true);
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
	}
}