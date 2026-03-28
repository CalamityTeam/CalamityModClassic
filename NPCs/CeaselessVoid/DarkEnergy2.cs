using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.CeaselessVoid
{
	public class DarkEnergy2 : ModNPC
	{
        public int invinceTime = 120;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark Energy");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.dontTakeDamage = true;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 68;
            NPC.lifeMax = 6000;
            if (CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0)
            {
                NPC.lifeMax = 24000;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 44000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0.1f;
            NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			AIType = -1;
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
			bool expertMode = Main.expertMode;
            if (invinceTime > 0)
            {
                invinceTime--;
            }
            else
            {
                NPC.damage = expertMode ? 240 : 120;
                NPC.dontTakeDamage = false;
            }
            Player player = Main.player[NPC.target];
            if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
            {
                NPC.knockBackResist = 0f;
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
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, -10f);
                    if (NPC.timeLeft > 150)
                    {
                        NPC.timeLeft = 150;
                    }
                    return;
                }
            }
            else if (NPC.timeLeft < 2400)
            {
                NPC.timeLeft = 2400;
            }
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
			int num1262 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num1262].velocity *= 0.1f;
			Main.dust[num1262].scale = 1.3f;
			Main.dust[num1262].noGravity = true;
			return;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}