using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.PlaguebringerGoliath
{
	public class PlagueBeeG : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plague Charger");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 60;
			NPC.width = 36; //324
			NPC.height = 30; //216
			NPC.defense = 20;
			NPC.scale = 0.75f;
			NPC.lifeMax = 300;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 50000;
            }
            NPC.aiStyle = -1;
			AIType = -1;
			NPC.knockBackResist = 0.9f;
			AnimationType = 210;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
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
		}
		
		public override void AI()
		{
			bool revenge = CalamityWorldPreTrailer.revenge;
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.015f, 0.1f, 0f);
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			float num = revenge ? 8f : 7f;
			float num2 = revenge ? 0.2f : 0.15f;
			NPC.localAI[0] += 1f;
			float num3 = (NPC.localAI[0] - 60f) / 60f;
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			else
			{
				if (NPC.velocity.X > 6f)
				{
					NPC.velocity.X = 6f;
				}
				if (NPC.velocity.X < -6f)
				{
					NPC.velocity.X = -6f;
				}
				if (NPC.velocity.Y > 6f)
				{
					NPC.velocity.Y = 6f;
				}
				if (NPC.velocity.Y < -6f)
				{
					NPC.velocity.Y = -6f;
				}
			}
			num2 *= num3;
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
			NPC.ai[0] += 1f;
			if (NPC.ai[0] > 0f)
			{
				NPC.velocity.Y = NPC.velocity.Y + 0.023f;
			}
			else
			{
				NPC.velocity.Y = NPC.velocity.Y - 0.023f;
			}
			if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
			{
				NPC.velocity.X = NPC.velocity.X + 0.023f;
			}
			else
			{
				NPC.velocity.X = NPC.velocity.X - 0.023f;
			}
			if (NPC.ai[0] > 200f)
			{
				NPC.ai[0] = -200f;
			}
			if (Main.player[NPC.target].dead)
			{
				num4 = (float)NPC.direction * num / 2f;
				num5 = -num / 2f;
			}
			if (NPC.velocity.X < num4)
			{
				NPC.velocity.X = NPC.velocity.X + num2;
				if (NPC.velocity.X < 0f && num4 > 0f)
				{
					NPC.velocity.X = NPC.velocity.X + num2;
				}
			}
			else if (NPC.velocity.X > num4)
			{
				NPC.velocity.X = NPC.velocity.X - num2;
				if (NPC.velocity.X > 0f && num4 < 0f)
				{
					NPC.velocity.X = NPC.velocity.X - num2;
				}
			}
			if (NPC.velocity.Y < num5)
			{
				NPC.velocity.Y = NPC.velocity.Y + num2;
				if (NPC.velocity.Y < 0f && num5 > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y + num2;
				}
			}
			else if (NPC.velocity.Y > num5)
			{
				NPC.velocity.Y = NPC.velocity.Y - num2;
				if (NPC.velocity.Y > 0f && num5 < 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y - num2;
				}
			}
			NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
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
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}