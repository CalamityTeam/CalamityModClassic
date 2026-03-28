using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class MirageJelly : ModNPC
	{
		public bool teleporting = false;
		public bool rephasing = false;
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mirage Jelly");
			Main.npcFrameCount[NPC.type] = 7;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("These jellyfish create illusions of themselves to confuse both predator and prey.")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.damage = 100;
			NPC.width = 70;
			NPC.height = 162;
			NPC.defense = 10;
			NPC.lifeMax = 6000;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.knockBackResist = 0f;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 25, 0);
			NPC.HitSound = SoundID.NPCHit25;
			NPC.DeathSound = SoundID.NPCDeath28;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MirageJellyBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer3Biome>().Type };
		}

		public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			NPC.velocity *= 0.985f;
			if (NPC.velocity.Y > -0.3f)
			{
				NPC.velocity.Y = -3f;
			}
			if (NPC.justHit)
			{
				if (Main.rand.Next(10) == 0)
				{
					teleporting = true;
				}
				hasBeenHit = true;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.chaseable = true;
				NPC.dontTakeDamage = false;
				if (Main.netMode != 1)
				{
					if (teleporting)
					{
						teleporting = false;
						NPC.TargetClosest(true);
						int num1249 = 0;
						int num1250;
						int num1251;
						while (true)
						{
							num1249++;
							num1250 = (int)player.Center.X / 16;
							num1251 = (int)player.Center.Y / 16;
							num1250 += Main.rand.Next(-50, 51);
							num1251 += Main.rand.Next(-50, 51);
							if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2((float)(num1250 * 16), (float)(num1251 * 16)), 1, 1, player.position, player.width, player.height) &&
								Main.tile[num1250, num1251].LiquidAmount > 204)
							{
								break;
							}
							if (num1249 > 100)
							{
								goto Block;
							}
						}
						NPC.ai[0] = 1f;
						NPC.ai[1] = (float)num1250;
						NPC.ai[2] = (float)num1251;
						NPC.netUpdate = true;
					Block:;
					}
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.damage = 0;
				NPC.chaseable = false;
				NPC.dontTakeDamage = true;
				NPC.alpha += 5;
				if (NPC.alpha >= 255)
				{
					NPC.alpha = 255;
					NPC.position.X = NPC.ai[1] * 16f - (float)(NPC.width / 2);
					NPC.position.Y = NPC.ai[2] * 16f - (float)(NPC.height / 2);
					NPC.ai[0] = 2f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.alpha -= 5;
				if (NPC.alpha <= 0)
				{
					NPC.damage = Main.expertMode ? 160 : 80;
					NPC.chaseable = true;
					NPC.dontTakeDamage = false;
					NPC.alpha = 0;
					NPC.ai[0] = 0f;
					NPC.netUpdate = true;
				}
			}
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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/MirageJellyGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/MirageJellyGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Purple);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/MirageJellyGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += (hasBeenHit ? 0.15f : 0.1f);
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer3 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule fakeCalorPlantDead = new LeadingConditionRule(new DownedCalDoppelorPlantera());
			
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1000000));
			fakeCalorPlantDead.OnSuccess(new CommonDrop(Mod.Find<ModItem>("DepthCells").Type, 2, 5, 8));
			fakeCalorPlantDead.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("DepthCells").Type, 2, 5, 8));
			npcLoot.Add(fakeCalorPlantDead);
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("VitalJelly").Type, 5));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("LifeJelly").Type, 5));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("ManaJelly").Type, 5));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("VitalJelly").Type, 7));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("LifeJelly").Type, 7));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("ManaJelly").Type, 7));
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Venom, 240, true);
			target.AddBuff(BuffID.Electrified, 60, true);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}