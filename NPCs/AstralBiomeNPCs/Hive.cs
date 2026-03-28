using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Dusts;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
	public class Hive : ModNPC
	{
		private static Texture2D glowmask;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hive");
			if (!Main.dedServ)
				glowmask = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstralBiomeNPCs/HiveGlow").Value;
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("An amalgamation of creatures, these hives will split off small chunks of themselves to attack enemies.")
			});
		}

		public override void SetDefaults()
		{
			NPC.width = 38;
			NPC.height = 60;
			NPC.aiStyle = -1;
			NPC.damage = 90;
			NPC.defense = 25;
			NPC.lifeMax = 700;
			NPC.DeathSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCKilled/AstralEnemyDeath");
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 15, 0);
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("HiveBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

		public override void AI()
		{
			NPC.ai[0]++;
			if (NPC.ai[0] > 180)
			{
				if (Main.rand.Next(100) == 0)
				{
					NPC.ai[0] = 0;

					//spawn hiveling, it's ai[0] is the hive npc index.
					int n = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("Hiveling").Type, 0, NPC.whoAmI);
					Main.npc[n].velocity.X = Main.rand.NextFloat(-0.4f, 0.4f);
					Main.npc[n].velocity.Y = Main.rand.NextFloat(-0.5f, -0.05f);
				}
			}
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter++;
			if (NPC.frameCounter > 10)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y += frameHeight;
				if (NPC.frame.Y > frameHeight * 4)
				{
					NPC.frame.Y = 0;
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			//draw glowmask
			spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition, NPC.frame, Color.White * 0.6f, NPC.rotation, new Vector2(19, 30), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.soundDelay == 0)
			{
				NPC.soundDelay = 15;
				switch (Main.rand.Next(3))
				{
					case 0:
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit"), NPC.Center);
						break;
					case 1:
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit2"), NPC.Center);
						break;
					case 2:
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit3"), NPC.Center);
						break;
				}
			}

			CalamityGlobalNPC.DoHitDust(NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3, 20);

			//if dead do gores
			if (NPC.life <= 0)
			{
				int type = Mod.Find<ModNPC>("Hiveling").Type;
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].type == type)
					{
						Main.npc[i].ai[0] = -1f;
					}
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			//only 1 hive possible.
			bool anyHives = NPC.CountNPCS(NPC.type) > 0;
			if (anyHives) return 0f;

			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral && (spawnInfo.Player.ZoneDirtLayerHeight || spawnInfo.Player.ZoneRockLayerHeight))
			{
				return 0.17f;
			}
			return 0f;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Stardust").Type, 1, 2, 4));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("Stardust").Type, 1));
			npcLoot.Add(ItemDropRule.ByCondition(new DownedAstrumDeus(), Mod.Find<ModItem>("HivePod").Type, 7));
		}
	}
}
