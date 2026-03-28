using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class BobbitWormHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bobbit Worm");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/AbyssNPCs/BobbitWorm_Bestiary"
			};
			value.Position.Y += 40;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 150;
			NPC.width = 80; //324
			NPC.height = 40; //216
			NPC.defense = 50;
			NPC.lifeMax = 10000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			NPC.noGravity = true;
			NPC.value = Item.buyPrice(0, 0, 50, 0);
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("BobbitWormBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer4Biome>().Type };
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("An ambush predator that lurks within the deepest parts of the abyss, it extends out rapidly to snatch unsuspecting prey.")
			});
		}

		public override void AI()
		{
			if (NPC.ai[0] == 0f)
			{
				NPC.noTileCollide = true;
				float num659 = 14f;
				Vector2 vector79 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num660 = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.X - vector79.X;
				float num661 = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.Y - vector79.Y;
				float num662 = (float)Math.Sqrt((double)(num660 * num660 + num661 * num661));
				if (num662 < 11f + num659)
				{
					NPC.rotation = 0f;
					NPC.velocity.X = num660;
					NPC.velocity.Y = num661;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] >= 60f)
					{
						NPC.TargetClosest(true);
						if (NPC.Center.Y + ((Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().anechoicPlating ||
							Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().anechoicCoating) ? 50f : 100f) > Main.player[NPC.target].Center.Y &&
							Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.ai[1] = 0f;
							NPC.ai[0] = 1f;
							return;
						}
						NPC.ai[1] = 0f;
						return;
					}
				}
				else
				{
					num662 = num659 / num662;
					NPC.velocity.X = num660 * num662;
					NPC.velocity.Y = num661 * num662;
					NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.noTileCollide = true;
				NPC.collideX = false;
				NPC.collideY = false;
				float num663 = 11f;
				Vector2 vector80 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num664 = Main.player[NPC.target].Center.X - vector80.X;
				float num665 = Main.player[NPC.target].Center.Y - vector80.Y;
				float num666 = (float)Math.Sqrt((double)(num664 * num664 + num665 * num665));
				num666 = num663 / num666;
				NPC.velocity.X = num664 * num666;
				NPC.velocity.Y = num665 * num666;
				NPC.ai[0] = 2f;
				NPC.rotation = (float)Math.Atan2((double)(-(double)NPC.velocity.Y), (double)(-(double)NPC.velocity.X)) - 1.57f;
			}
			else if (NPC.ai[0] == 2f)
			{
				if (Math.Abs(NPC.velocity.X) > Math.Abs(NPC.velocity.Y))
				{
					if (NPC.velocity.X > 0f && NPC.Center.X > Main.player[NPC.target].Center.X)
					{
						NPC.noTileCollide = false;
					}
					if (NPC.velocity.X < 0f && NPC.Center.X < Main.player[NPC.target].Center.X)
					{
						NPC.noTileCollide = false;
					}
				}
				else
				{
					if (NPC.velocity.Y > 0f && NPC.Center.Y > Main.player[NPC.target].Center.Y)
					{
						NPC.noTileCollide = false;
					}
					if (NPC.velocity.Y < 0f && NPC.Center.Y < Main.player[NPC.target].Center.Y)
					{
						NPC.noTileCollide = false;
					}
				}
				Vector2 vector81 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num667 = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.X - vector81.X;
				float num668 = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.Y - vector81.Y;
				float num669 = (float)Math.Sqrt((double)(num667 * num667 + num668 * num668));
				if (num669 > 700f || NPC.collideX || NPC.collideY)
				{
					NPC.noTileCollide = true;
					NPC.ai[0] = 0f;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				NPC.noTileCollide = true;
				float num671 = 11f;
				float num672 = 0.25f;
				Vector2 vector82 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num673 = Main.player[NPC.target].Center.X - vector82.X;
				float num674 = Main.player[NPC.target].Center.Y - vector82.Y;
				float num675 = (float)Math.Sqrt((double)(num673 * num673 + num674 * num674));
				num675 = num671 / num675;
				num673 *= num675;
				num674 *= num675;
				if (NPC.velocity.X < num673)
				{
					NPC.velocity.X = NPC.velocity.X + num672;
					if (NPC.velocity.X < 0f && num673 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num672 * 2f;
					}
				}
				else if (NPC.velocity.X > num673)
				{
					NPC.velocity.X = NPC.velocity.X - num672;
					if (NPC.velocity.X > 0f && num673 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num672 * 2f;
					}
				}
				if (NPC.velocity.Y < num674)
				{
					NPC.velocity.Y = NPC.velocity.Y + num672;
					if (NPC.velocity.Y < 0f && num674 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num672 * 2f;
					}
				}
				else if (NPC.velocity.Y > num674)
				{
					NPC.velocity.Y = NPC.velocity.Y - num672;
					if (NPC.velocity.Y > 0f && num674 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num672 * 2f;
					}
				}
				NPC.rotation = (float)Math.Atan2((double)(-(double)NPC.velocity.Y), (double)(-(double)NPC.velocity.X));
			}
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.1f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			float drawPositionX = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.X - center.X;
			float drawPositionY = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.Y - center.Y;
			float rotation = (float)Math.Atan2((double)drawPositionY, (double)drawPositionX) - 1.57f;
			bool draw = !NPC.IsABestiaryIconDummy;
			while (draw)
			{
				float totalDrawDistance = (float)Math.Sqrt((double)(drawPositionX * drawPositionX + drawPositionY * drawPositionY));
				if (totalDrawDistance < 16f)
				{
					draw = false;
				}
				else
				{
					totalDrawDistance = 16f / totalDrawDistance;
					drawPositionX *= totalDrawDistance;
					drawPositionY *= totalDrawDistance;
					center.X += drawPositionX;
					center.Y += drawPositionY;
					drawPositionX = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.X - center.X;
					drawPositionY = Main.npc[CalamityGlobalNPC.bobbitWormBottom].Center.Y - center.Y;
					drawPositionY += 4f;
					Microsoft.Xna.Framework.Color color = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
					Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/BobbitWormSegment").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/BobbitWormSegment").Value.Width, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/BobbitWormSegment").Value.Height)), color, rotation,
						new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/BobbitWormSegment").Value.Width * 0.5f, (float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/BobbitWormSegment").Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Bleeding, 300, true);
			target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 300, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 300);
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120, true);
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule fakeCalorPlantDead = new LeadingConditionRule(new DownedCalDoppelorPlantera());
			
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1000000));
			fakeCalorPlantDead.OnSuccess(new CommonDrop(Mod.Find<ModItem>("DepthCells").Type, 2, 5, 8));
			fakeCalorPlantDead.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("DepthCells").Type, 2, 2, 4));
			npcLoot.Add(fakeCalorPlantDead);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int k = 0; k < 20; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("BobbitWorm").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("BobbitWorm2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("BobbitWorm3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("BobbitWorm4").Type, 1f);
				}
			}
		}
	}
}