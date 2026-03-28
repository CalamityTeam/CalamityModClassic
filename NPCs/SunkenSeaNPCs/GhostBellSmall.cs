using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.SunkenSeaNPCs
{
	public class GhostBellSmall : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Baby Ghost Bell");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Like their adult counterparts, they drift gently. Be careful not to hurt them!")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 0.1f;
			NPC.noGravity = true;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 28;
			NPC.height = 36;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			NPC.knockBackResist = 1f;
			NPC.alpha = 100;
			NPC.HitSound = SoundID.NPCHit25;
			NPC.DeathSound = SoundID.NPCDeath28;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("GhostBellSmallBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<SunkenSea>().Type };
		}

		public override void AI()
		{
			Lighting.AddLight(NPC.Center, 0f, ((255 - NPC.alpha) * 1f) / 255f, ((255 - NPC.alpha) * 1f) / 255f);
			if (NPC.localAI[0] == 0f)
			{
				NPC.localAI[0] = 1f;
				NPC.velocity.Y = -3f;
				NPC.netUpdate = true;
			}
			if (NPC.wet)
			{
				NPC.noGravity = true;
				if (NPC.velocity.Y < 0f)
				{
					NPC.velocity.Y += 0.1f;
				}
				if (NPC.velocity.Y > 0f)
				{
					NPC.velocity.Y = 0f;
				}
			}
			else
			{
				NPC.noGravity = false;
			}
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.minion)
			{
				return hasBeenHit;
			}
			return null;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea && spawnInfo.Water && !spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().clamity)
			{
				return SpawnCondition.CaveJellyfish.Chance * 1.5f;
			}
			return 0f;
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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/GhostBellSmallGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/GhostBellSmallGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.LightBlue);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/SunkenSeaNPCs/GhostBellSmallGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 2; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}