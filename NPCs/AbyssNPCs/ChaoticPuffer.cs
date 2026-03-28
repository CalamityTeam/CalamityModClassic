using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class ChaoticPuffer : ModNPC
	{
		public bool puffedUp = false;
		public bool puffing = false;
		public bool unpuffing = false;
		public int puffTimer = 0;
		public int puffingTimer = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaotic Puffer");
			Main.npcFrameCount[NPC.type] = 11;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("While it behaves like a regular pufferfish for the most part, once destroyed its water reserves will be expelled at extreme temperatures, severely burning anything in its proximity.")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.width = 78;
			NPC.height = 70;
			NPC.defense = 25;
			NPC.lifeMax = 7500;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.knockBackResist = 0f;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 30, 0);
			NPC.HitSound = SoundID.NPCHit23;
			NPC.DeathSound = SoundID.NPCDeath28;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("ChaoticPufferBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer2Biome>().Type, ModContent.GetInstance<AbyssLayer3Biome>().Type };
		}

		public override void AI()
		{
			NPC.TargetClosest(true);
			NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.03f;
			NPC.velocity.Y = NPC.velocity.Y + (float)NPC.directionY * 0.03f;

			NPC.damage = puffedUp ? (Main.expertMode ? 230 : 115) : 0;


			if (!puffing || !unpuffing)
			{
				++puffTimer;
			}

			if (puffTimer >= 300)
			{

				if (!puffedUp)
				{
					puffing = true;
				}
				else
				{
					unpuffing = true;
				}

				puffTimer = 0;

			}
			else if (puffing || unpuffing)
			{

				++puffingTimer;

				if (puffingTimer > 16 && puffing)
				{

					puffing = false;
					puffedUp = true;
					puffingTimer = 0;

				}
				else if (puffingTimer > 16 && unpuffing)
				{

					unpuffing = false;
					puffedUp = false;
					puffingTimer = 0;

				}

			}

			if (NPC.velocity.X >= 1 || NPC.velocity.X <= -1)
			{

				NPC.velocity.X = NPC.velocity.X * 0.97f;

			}

			if (NPC.velocity.Y >= 1 || NPC.velocity.Y <= -1)
			{

				NPC.velocity.Y = NPC.velocity.Y * 0.97f;

			}

			NPC.rotation += NPC.velocity.X * 0.05f;

		}

		public void Boom()
		{
			SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
			if (Main.netMode != 1 && puffedUp)
			{
				int damageBoom = 100;
				int projectileType = Mod.Find<ModProjectile>("PufferExplosion").Type;
				int boom = Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0, 0, projectileType, damageBoom, 0f, Main.myPlayer, 0f, 0f);
			}
			NPC.netUpdate = true;
		}


		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
			Vector2 vector = center - Main.screenPosition;
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/ChaoticPufferGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/ChaoticPufferGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Yellow);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/ChaoticPufferGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frameCounter = 0.0;
				if (!unpuffing)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				else
				{
					NPC.frame.Y = NPC.frame.Y - frameHeight;
				}
			}
			if (puffing)
			{
				if (NPC.frame.Y < frameHeight * 3)
				{
					NPC.frame.Y = frameHeight * 3;
				}
				if (NPC.frame.Y > frameHeight * 6)
				{
					NPC.frame.Y = frameHeight * 3;
				}
			}
			else if (unpuffing)
			{
				if (NPC.frame.Y > frameHeight * 6)
				{
					NPC.frame.Y = frameHeight * 6;
				}
				if (NPC.frame.Y < frameHeight * 3)
				{
					NPC.frame.Y = frameHeight * 6;
				}
			}
			else if (!puffedUp)
			{
				if (NPC.frame.Y > frameHeight * 3)
				{
					NPC.frame.Y = 0;
				}
			}
			else
			{
				if (NPC.frame.Y < frameHeight * 7)
				{
					NPC.frame.Y = frameHeight * 7;
				}
				if (NPC.frame.Y > frameHeight * 10)
				{
					NPC.frame.Y = frameHeight * 7;
				}
			}

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer4 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1000000));
			npcLoot.Add(ItemDropRule.ByCondition(new DownedGolem(), Mod.Find<ModItem>("ChaoticOre").Type, 1, 10, 27));
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);

			if (puffedUp)
			{
				Boom();
				NPC.active = false;
			}
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			NPC.velocity.X = projectile.velocity.X;
			NPC.velocity.Y = projectile.velocity.Y;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				Boom();

				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}