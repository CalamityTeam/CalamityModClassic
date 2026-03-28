using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Polterghast
{
	public class PolterPhantom : ModNPC
	{
        private int despawnTimer = 600;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Polterghast");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 210;
			NPC.width = 90;
			NPC.height = 120;
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 150000 : 130000;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 225000;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = CalamityWorldPreTrailer.death ? 1100000 : 900000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
            NPC.alpha = 255;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
            }
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit36;
			NPC.DeathSound = SoundID.NPCDeath39;
		}

        public override void AI()
        {
            NPC.alpha -= 5;
            if (NPC.alpha < 50)
            {
                NPC.alpha = 50;
            }
            if (!Main.npc[CalamityGlobalNPC.ghostBoss].active)
            {
                NPC.active = false;
                NPC.netUpdate = true;
                return;
            }
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.5f, 0.25f, 0.75f);
            NPC.TargetClosest(true);
            Vector2 vector = NPC.Center;
            if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 6000f)
			{
				NPC.active = false;
			}
            bool speedBoost1 = false;
            bool despawnBoost = false;
            if (NPC.timeLeft < 1500)
			{
				NPC.timeLeft = 1500;
			}
            bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
            bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
            int[] array2 = new int[4];
            float num730 = 0f;
            float num731 = 0f;
            int num732 = 0;
            int num;
            for (int num733 = 0; num733 < 200; num733 = num + 1)
            {
                if (Main.npc[num733].active && Main.npc[num733].type == Mod.Find<ModNPC>("PolterghastHook").Type)
                {
                    num730 += Main.npc[num733].Center.X;
                    num731 += Main.npc[num733].Center.Y;
                    array2[num732] = num733;
                    num732++;
                    if (num732 > 3)
						break;
                }
                num = num733;
            }
            num730 /= (float)num732;
            num731 /= (float)num732;
            float num734 = 3f;
            float num735 = 0.03f;
            if (!Main.player[NPC.target].ZoneDungeon)
            {
                despawnTimer--;
                if (despawnTimer <= 0)
					despawnBoost = true;
                speedBoost1 = true;
                num734 += 8f;
                num735 = 0.15f;
            }
            else
			{
				despawnTimer = 600;
			}
            if (Main.npc[CalamityGlobalNPC.ghostBoss].ai[2] < 300f)
            {
                num734 = 18f;
                num735 = 0.12f;
            }
            if (expertMode)
            {
                num734 += revenge ? 1.5f : 1f;
                num734 *= revenge ? 1.25f : 1.1f;
                num735 += revenge ? 0.015f : 0.01f;
                num735 *= revenge ? 1.2f : 1.1f;
            }
            Vector2 vector91 = new Vector2(num730, num731);
            float num736 = Main.player[NPC.target].Center.X - vector91.X;
            float num737 = Main.player[NPC.target].Center.Y - vector91.Y;
            if (despawnBoost)
            {
                num737 *= -1f;
                num736 *= -1f;
                num734 += 8f;
            }
            float num738 = (float)Math.Sqrt((double)(num736 * num736 + num737 * num737));
            int num739 = 500;
            if (speedBoost1)
			{
				num739 += 500;
			}
            if (expertMode)
			{
				num739 += 150;
			}
            if (num738 >= (float)num739)
            {
                num738 = (float)num739 / num738;
                num736 *= num738;
                num737 *= num738;
            }
            num730 += num736;
            num731 += num737;
            vector91 = new Vector2(vector.X, vector.Y);
            num736 = num730 - vector91.X;
            num737 = num731 - vector91.Y;
            num738 = (float)Math.Sqrt((double)(num736 * num736 + num737 * num737));
            if (num738 < num734)
            {
                num736 = NPC.velocity.X;
                num737 = NPC.velocity.Y;
            }
            else
            {
                num738 = num734 / num738;
                num736 *= num738;
                num737 *= num738;
            }
            if (NPC.velocity.X < num736)
            {
                NPC.velocity.X = NPC.velocity.X + num735;
                if (NPC.velocity.X < 0f && num736 > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + num735 * 2f;
                }
            }
            else if (NPC.velocity.X > num736)
            {
                NPC.velocity.X = NPC.velocity.X - num735;
                if (NPC.velocity.X > 0f && num736 < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - num735 * 2f;
                }
            }
            if (NPC.velocity.Y < num737)
            {
                NPC.velocity.Y = NPC.velocity.Y + num735;
                if (NPC.velocity.Y < 0f && num737 > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + num735 * 2f;
                }
            }
            else if (NPC.velocity.Y > num737)
            {
                NPC.velocity.Y = NPC.velocity.Y - num735;
                if (NPC.velocity.Y > 0f && num737 < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - num735 * 2f;
                }
            }
            Vector2 vector92 = new Vector2(vector.X, vector.Y);
            float num740 = Main.player[NPC.target].Center.X - vector92.X;
            float num741 = Main.player[NPC.target].Center.Y - vector92.Y;
            NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) + 1.57f;
            if (speedBoost1)
            {
                NPC.defense = 400;
				NPC.damage = 1200;
            }
            else
            {
                NPC.damage = expertMode ? 336 : 210;
                NPC.defense = 0;
			}
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return new Color(200, 150, 255, NPC.alpha);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 6.0)
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = NPC.frame.Y + frameHeight;
            }
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
            if (CalamityWorldPreTrailer.revenge)
			    target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			Dust.NewDust(NPC.position, NPC.width, NPC.height, 180, hit.HitDirection, -1f, 0, default(Color), 1f);
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 90;
				NPC.height = 90;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 60; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}