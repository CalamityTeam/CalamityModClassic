using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
	public class Hiveling : ModNPC
	{
		private static Texture2D glowmask;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hiveling");
			if (!Main.dedServ)
				glowmask = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstralBiomeNPCs/HivelingGlow").Value;
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("One sample of a piece of Hive, these can be very threatening in large numbers.")
			});
		}

		public override void SetDefaults()
		{
			NPC.width = 50;
			NPC.height = 40;
			NPC.aiStyle = -1;
			NPC.damage = 50;
			NPC.defense = 8;
			NPC.lifeMax = 220;
			NPC.DeathSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCKilled/AstralEnemyDeath");
			NPC.knockBackResist = 0.4f;
			NPC.noGravity = true;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

		public override void AI()
		{
			if (NPC.ai[1] == 0f)
			{
				NPC.velocity *= 0.97f;

				NPC.TargetClosest(false);
				if (Main.player[NPC.target].dead)
				{
					NPC.TargetClosest(false);
				}
				Player targ = Main.player[NPC.target];

				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, targ.position, targ.width, targ.height) || Vector2.Distance(NPC.Center, targ.MountedCenter) < 320f)
				{
					NPC.ai[1] = 1f;
				}
			}
			else
			{
				CalamityGlobalNPC.DoFlyingAI(NPC, 3f, 0.05f, 200f);
			}
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.05f + NPC.velocity.Length() * 0.667f;
			if (NPC.frameCounter >= 8)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y += frameHeight;
				if (NPC.frame.Y > NPC.height * 2)
				{
					NPC.frame.Y = 0;
				}
			}

			//DO DUST
			Dust d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 30, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(16, 8, 6, 6), Vector2.Zero, 0.3f, true);
			if (d != null)
			{
				d.customData = 0.04f;
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			//draw glowmask
			spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition + new Vector2(0, 12), NPC.frame, Color.White * 0.6f, NPC.rotation, new Vector2(15, 10), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
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
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
	}
}
