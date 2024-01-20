using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.CeaselessVoid
{
	public class DarkEnergy2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Dark Energy");
			Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = -1;
			NPC.damage = 190;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 68;
			NPC.lifeMax = 28000;
			NPC.knockBackResist = 0.1f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.value = Item.buyPrice(0, 0, 0, 0);
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
			bool expertMode = Main.expertMode;
			bool bossBuff = CalamityWorld1Point2.demonMode;
			bool superBossBuff = CalamityWorld1Point2.onionMode;
			Player player = Main.player[NPC.target];
			if (player.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 3f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 3f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == NPC.aiStyle)
						{
							Main.npc[num957].active = false;
						}
					}
				}
			}
			if (NPC.ai[1] == 0f)
			{
				NPC.scale -= 0.02f;
				NPC.alpha += 30;
				if (NPC.alpha >= 250)
				{
					NPC.alpha = 255;
					NPC.ai[1] = 1f;
				}
			}
			else if (NPC.ai[1] == 1f)
			{
				NPC.scale += 0.02f;
				NPC.alpha -= 30;
				if (NPC.alpha <= 0)
				{
					NPC.alpha = 0;
					NPC.ai[1] = 0f;
				}
			}
			NPC.TargetClosest(true);
			Vector2 vector145 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num1258 = Main.player[NPC.target].Center.X - vector145.X;
			float num1259 = Main.player[NPC.target].Center.Y - vector145.Y;
			float num1260 = (float)Math.Sqrt((double)(num1258 * num1258 + num1259 * num1259));
			float num1261 = expertMode ? 18f : 15f;
			num1260 = num1261 / num1260;
			num1258 *= num1260;
			num1259 *= num1260;
			NPC.velocity.X = (NPC.velocity.X * 100f + num1258) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num1259) / 101f;
			int num1262 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num1262].velocity *= 0.1f;
			Main.dust[num1262].scale = 1.3f;
			Main.dust[num1262].noGravity = true;
			return;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}