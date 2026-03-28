using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Scavenger
{
	public class RockPillar : ModNPC
	{
        int tileCollideCounter = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rock Pillar");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 1f;
			NPC.width = 60; //324
			NPC.height = 300; //216
			NPC.defense = 0;
			NPC.lifeMax = 100;
            NPC.alpha = 255;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
            NPC.dontTakeDamage = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
		}

        public override void AI()
        {
            if (!Main.npc[CalamityGlobalNPC.scavenger].active)
            {
                NPC.dontTakeDamage = false;
                NPC.life = 0;
				NPC.HitEffect(NPC.direction, 9999);
                NPC.netUpdate = true;
                return;
            }
            if (NPC.timeLeft < 3000)
            {
                NPC.timeLeft = 3000;
            }
            if (NPC.alpha > 0)
            {
                NPC.alpha -= 10;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }
            if (Math.Abs(NPC.velocity.X) > 0.5f)
            {
                if (tileCollideCounter < 210)
                {
                    tileCollideCounter++;
                    NPC.noTileCollide = true;
                }
                else
                {
                    NPC.noTileCollide = false;
                }
                if (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive)
                {
                    NPC.damage = 400;
                }
                else
                {
                    NPC.damage = Main.expertMode ? 250 : 180;
                }
            }
            else
            {
                NPC.noTileCollide = false;
                NPC.damage = 0;
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.noTileCollide = false;
                if (NPC.velocity.Y == 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.8f;
                    NPC.ai[1] += 1f;
                    if (NPC.ai[1] > 0f)
                    {
                        NPC.ai[1] += 1f;
                    }
                    if (NPC.ai[1] >= 300f)
                    {
                        NPC.ai[1] = -20f;
                    }
                    else if (NPC.ai[1] == -1f)
                    {
                        NPC.TargetClosest(true);
                        NPC.velocity.X = (float)(4 * NPC.direction);
                        NPC.velocity.Y = -24.5f;
                        NPC.ai[0] = 1f;
                        NPC.ai[1] = 0f;
                    }
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                if (NPC.velocity.Y == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                    NPC.ai[0] = 0f;
                    NPC.dontTakeDamage = false;
                    NPC.life = 0;
                    NPC.HitEffect(NPC.direction, 9999);
                    NPC.netUpdate = true;
                    return;
                }
                else
                {
                    NPC.TargetClosest(true);
                    if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                        NPC.velocity.Y = NPC.velocity.Y + 0.2f;
                    }
                    else
                    {
                        if (NPC.direction < 0)
                        {
                            NPC.velocity.X = NPC.velocity.X - 0.2f;
                        }
                        else if (NPC.direction > 0)
                        {
                            NPC.velocity.X = NPC.velocity.X + 0.2f;
                        }
                        float random = (float)Main.rand.Next(7);
                        float velocityX = 6f + random;
                        if (NPC.velocity.X < -velocityX)
                        {
                            NPC.velocity.X = -velocityX;
                        }
                        if (NPC.velocity.X > velocityX)
                        {
                            NPC.velocity.X = velocityX;
                        }
                    }
                }
            }
            if (NPC.target <= 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            int distanceFromTarget = 8000;
            if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget)
            {
                NPC.TargetClosest(true);
                if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                    return;
                }
            }
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 80;
				NPC.height = 360;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 30; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 30; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 1, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}