using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassicPreTrailer.NPCs.CosmicWraith
{
	public class CosmicLantern : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Lantern");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("These lanterns rarely see use in combat, and instead allow their user to see through any form of darkness.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 110;
			NPC.width = 25; //324
			NPC.height = 25; //216
			NPC.defense = 85;
			NPC.lifeMax = 25;
            NPC.alpha = 255;
			NPC.knockBackResist = 0.85f;
			NPC.noGravity = true;
            NPC.dontTakeDamage = true;
            NPC.chaseable = false;
			NPC.canGhostHeal = false;
			NPC.noTileCollide = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit53;
			NPC.DeathSound = SoundID.NPCDeath44;
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
            NPC.alpha -= 3;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
                int num1262 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 204, 0f, 0f, 0, default(Color), 0.25f);
                Main.dust[num1262].velocity *= 0.1f;
                Main.dust[num1262].noGravity = true;
            }
            NPC.rotation = NPC.velocity.X * 0.08f;
            NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
            NPC.TargetClosest(true);
            Vector2 vector145 = new Vector2(NPC.Center.X, NPC.Center.Y);
            float num1258 = Main.player[NPC.target].Center.X - vector145.X;
            float num1259 = Main.player[NPC.target].Center.Y - vector145.Y;
            float num1260 = (float)Math.Sqrt((double)(num1258 * num1258 + num1259 * num1259));
            bool revenge = CalamityWorldPreTrailer.revenge;
            float num1261 = revenge ? 27f : 25f;
            if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
            {
                num1261 = 30f;
            }
            if (NPC.localAI[0] < 85f)
            {
                num1261 = 0.1f;
                num1260 = num1261 / num1260;
                num1258 *= num1260;
                num1259 *= num1260;
                NPC.velocity.X = (NPC.velocity.X * 100f + num1258) / 101f;
                NPC.velocity.Y = (NPC.velocity.Y * 100f + num1259) / 101f;
                NPC.localAI[0] += 1f;
                return;
            }
            NPC.dontTakeDamage = false;
            NPC.chaseable = true;
			num1260 = num1261 / num1260;
			num1258 *= num1260;
			num1259 *= num1260;
			NPC.velocity.X = (NPC.velocity.X * 100f + num1258) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num1259) / 101f;
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (NPC.IsABestiaryIconDummy)
			{
				SpriteEffects effects = SpriteEffects.None;
				if (NPC.spriteDirection == 1)
				{
					effects = SpriteEffects.FlipHorizontally;
				}
				Texture2D value = TextureAssets.Npc[NPC.type].Value;
				Vector2 vector = new Vector2((TextureAssets.Npc[NPC.type].Value.Width / 2), (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[base.NPC.type] / 2));
				Vector2 vector2 = NPC.Center - screenPos;
				vector2 -= new Vector2(value.Width, (value.Height / Main.npcFrameCount[base.NPC.type])) * base.NPC.scale / 2f;
				float scale = 1f;
				vector2 += vector * NPC.scale + new Vector2(0f, 4f + NPC.gfxOffY);
				Main.EntitySpriteDraw(value, vector2, (NPC.frame), Color.White, NPC.rotation, vector, scale, effects, 0f);
				return false;
			}
			return true;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 180);
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 0;
			return NPC.alpha == 0;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 204, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}