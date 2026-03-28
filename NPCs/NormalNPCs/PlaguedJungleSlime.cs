using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class PlaguedJungleSlime : ModNPC
	{
		public float spikeTimer = 60f;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pestilent Slime");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				new FlavorTextBestiaryInfoElement("This slime taken over by the plague nanomachines... Yet, in its slimy wisdom, it behaves the same.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 1;
			NPC.damage = 55;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 25;
			NPC.lifeMax = 350;
			NPC.knockBackResist = 0f;
			AnimationType = 81;
			NPC.value = Item.buyPrice(0, 0, 10, 0);
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
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
			BannerItem = Mod.Find<ModItem>("PestilentSlimeBanner").Type;
		}
		
		public override void AI()
		{
			if (spikeTimer > 0f)
			{
				spikeTimer -= 1f;
			}
			if (!NPC.wet)
			{
				Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num14 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector3.X;
				float num15 = Main.player[NPC.target].position.Y - vector3.Y;
				float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
				if (Main.expertMode && num16 < 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
				{
					NPC.ai[0] = -40f;
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
					}
					if (Main.netMode != 1 && spikeTimer == 0f)
					{
                        SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                        for (int n = 0; n < 5; n++)
						{
							Vector2 vector4 = new Vector2((float)(n - 2), -4f);
							vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector4.Normalize();
							vector4 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector3.X, vector3.Y, vector4.X, vector4.Y, Mod.Find<ModProjectile>("PlagueStingerGoliathV2").Type, 25, 0f, Main.myPlayer, 0f, 0f);
							spikeTimer = 30f;
						}
					}
				}
				else if (num16 < 360f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
				{
					NPC.ai[0] = -40f;
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
					}
					if (Main.netMode != 1 && spikeTimer == 0f)
					{
                        SoundEngine.PlaySound(SoundID.Item42, NPC.position);
                        num15 = Main.player[NPC.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
						num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
						num16 = 6.5f / num16;
						num14 *= num16;
						num15 *= num16;
						spikeTimer = 50f;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector3.X, vector3.Y, num14, num15, Mod.Find<ModProjectile>("PlagueStingerGoliathV2").Type, 22, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !NPC.downedGolemBoss || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
            {
                return 0f;
            }
            return SpawnCondition.HardmodeJungle.Chance * 0.09f;
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
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("PlagueCellCluster").Type, 1, 1, 3));
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 300, true);
        }
	}
}