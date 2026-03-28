using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class PlaguedDerpling : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Viruling");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				new FlavorTextBestiaryInfoElement("A derpling overtaken by plague nanomachines, it now serves the plague completely.")
			});
		}
		
		public override void SetDefaults()
		{
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            AIType = -1;
			NPC.damage = 60;
			NPC.width = 58; //324
			NPC.height = 44; //216
			NPC.defense = 38;
			NPC.lifeMax = 400;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 10, 0);
			NPC.HitSound = SoundID.NPCHit22;
			NPC.DeathSound = SoundID.NPCDeath25;
            NPC.buffImmune[189] = true;
            NPC.buffImmune[153] = true;
            NPC.buffImmune[70] = true;
            NPC.buffImmune[69] = true;
            NPC.buffImmune[44] = true;
            NPC.buffImmune[39] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("VirulingBanner").Type;
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
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            float num = 6f; //2
            float num2 = 0.05f;
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
            int num13 = Dust.NewDust(new Vector2(NPC.position.X - NPC.velocity.X, NPC.position.Y - NPC.velocity.Y), NPC.width, NPC.height, 46, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[num13].noGravity = true;
            Dust expr_F26_cp_0 = Main.dust[num13];
            expr_F26_cp_0.velocity.X = expr_F26_cp_0.velocity.X * 0.3f;
            Dust expr_F44_cp_0 = Main.dust[num13];
            expr_F44_cp_0.velocity.Y = expr_F44_cp_0.velocity.Y * 0.3f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            if (spawnInfo.PlayerSafe || !NPC.downedGolemBoss || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
            {
                return 0f;
            }
            return SpawnCondition.HardmodeJungle.Chance * 0.09f;
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 300, true);
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
				}

				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Viruling").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Viruling2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Viruling3").Type, 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("PlagueCellCluster").Type, 1, 1, 3));
		}
	}
}